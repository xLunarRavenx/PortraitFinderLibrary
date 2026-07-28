using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PortraitFinder.Data;
using PortraitFinder.Model;
using SkiaSharp;
using System.IO;
using Serilog;


const string ImageName = "Medium.png";

RunInfo runInfo = new();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File(
        path: Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PortraitFinder", "logs", "Thumbnailer-Log.txt"),
        rollingInterval: RollingInterval.Infinite,
        fileSizeLimitBytes: 52428800,
        rollOnFileSizeLimit: true,
        retainedFileCountLimit: 5,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{Newline}{Exception}")
    .CreateLogger();

try
{
    Log.Information("PortraitFinder Thumbnailer");
    Console.WriteLine();

    Log.Information("Initializing...");

    var builder = Host.CreateApplicationBuilder(args);

    runInfo.ThumbnailFolder = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "PortraitFinder",
        "thumbnails");

    Directory.CreateDirectory(runInfo.ThumbnailFolder);

    var dbPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "PortraitFinder",
        "portraitfinder.db");

    builder.Services.AddDbContext<PortraitFinderDbContext>(options => 
        options
            .UseSqlite($"Data Source={dbPath}")
            .ConfigureWarnings(w => w.Ignore([RelationalEventId.CommandExecuted, RelationalEventId.MigrationApplying, RelationalEventId.MigrationsNotApplied, RelationalEventId.AcquiringMigrationLock]))
    );

    runInfo.Host = builder.Build();

    await using var scope = runInfo.Host.Services.CreateAsyncScope();
    var db = scope.ServiceProvider.GetRequiredService<PortraitFinderDbContext>();

    await db.Database.MigrateAsync();

    Log.Information("Initialized.");
    Console.WriteLine();

    await RunLogic(runInfo);
}
catch(Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.BackgroundColor = ConsoleColor.Yellow;
    Log.Error($"An error occurred: {ex.Message}", ex);
    Console.ResetColor();
    Console.WriteLine();
    runInfo.ExitStatus = 1;
}
finally
{
    runInfo.Host?.Dispose();

    Console.WriteLine("Press any key to exit...");
    Console.ReadKey(false);
    Environment.Exit(runInfo.ExitStatus);
}


static async Task RunLogic(RunInfo runInfo)
{
    var rootFolder = @"D:\Pictures\Owlcat PF portraits";
    // Log.Information("Enter root images folder to scan:");
    // string rootFolder = Console.ReadLine().Trim();
    // if ((rootFolder?? "") == "" || !Directory.Exists(rootFolder))
    // {
    //     Log.Warning($"{Environment.NewLine}Invalid folder path: {rootFolder}");
    //     return;
    // }

    Console.WriteLine();
    Log.Information($"Scanning folder {rootFolder}...");

    await ScanImageFolder(rootFolder, runInfo);

    Console.WriteLine();
    Log.Information($"Finished processing folder {rootFolder}. {runInfo.ImagesLoaded} portraits added to the database.");
    Console.WriteLine();
}

static async Task ScanImageFolder(string folderPath, RunInfo runInfo)
{
    var image = Directory.GetFiles(folderPath).FirstOrDefault(x => Path.GetFileName(x).Equals(ImageName, StringComparison.OrdinalIgnoreCase));
    if (image != null)
    {
        await ProcessImage(image, runInfo);
    }

    var folders = Directory.GetDirectories(folderPath);
    foreach(var folder in folders)
    {
        await ScanImageFolder(folder, runInfo);
    }
}

static async Task ProcessImage(string imagePath, RunInfo runInfo)
{
    var lastWriteTime = new FileInfo(imagePath).LastWriteTime;
    var folder = Path.GetDirectoryName(imagePath);

    await using var scope = runInfo.Host.Services.CreateAsyncScope();
    var db = scope.ServiceProvider.GetRequiredService<PortraitFinderDbContext>();

    var portrait = db.Portraits.FirstOrDefault(p => p.PortraitFolderPath == folder);
    if (portrait != null)
    {
        if (portrait.ImageLastModified == lastWriteTime)
        {
            return;
        }

        Log.Information($"  Updating thumbnail for {Path.Combine(runInfo.ThumbnailFolder, Path.GetFileName(portrait.ThumbnailPath))} from {imagePath}");
        CreateThumbnail(imagePath, Path.GetFileName(portrait.ThumbnailPath), runInfo);

        portrait.ImageLastModified = lastWriteTime;
    }
    else
    {
        portrait = new PortraitFinder.Model.Portrait
        {
            PortraitFolderPath = folder,
            ImageLastModified = lastWriteTime
        };
        await db.Portraits.AddAsync(portrait);
        await db.SaveChangesAsync();

        portrait = db.Portraits.First(p => p.PortraitFolderPath == folder);

        var thumbnailName = $"Portrait_{portrait.Id:00000}.png";

        Log.Information($"  Creating thumbnail for {imagePath} -> {Path.Combine(runInfo.ThumbnailFolder, thumbnailName)}");
        var thumbnailPath = CreateThumbnail(imagePath, thumbnailName, runInfo);

        portrait.ThumbnailPath = thumbnailPath;
    }

    await db.SaveChangesAsync();
    runInfo.ImagesLoaded++;
}

static string CreateThumbnail(string imagePath, string thumbnailName, RunInfo runInfo)
{
    // load image
    using var bitmap = SKBitmap.Decode(imagePath);

    // // scale to 200x262
    // using var scaled = bitmap.Resize(new SKImageInfo(200, 262), new SKSamplingOptions(SKCubicResampler.Mitchell));
    // scale to 110x169
    using var scaled = bitmap.Resize(new SKImageInfo(110, 169), new SKSamplingOptions(SKCubicResampler.Mitchell));

    // convert from bitmap to png
    using var image = SKImage.FromBitmap(scaled);
    using var png = image.Encode(SKEncodedImageFormat.Png, 100);

    // save to disk
    var thumbnailPath = Path.Combine(runInfo.ThumbnailFolder, thumbnailName);
    using var filestream = File.OpenWrite(thumbnailPath);
    png.SaveTo(filestream);

    return thumbnailPath;
}

public class RunInfo
{
    public int ImagesLoaded { get; set; } = 0;
    public string ThumbnailFolder { get; set; } = string.Empty;
    public IHost? Host { get; set; } = null;
    public int ExitStatus { get; set; } = 0;
}
