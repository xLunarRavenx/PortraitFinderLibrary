using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PortraitFinder.Data;
using PortraitFinder.Services;
using Serilog;

namespace PortraitFinder.App;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private IHost? _host;

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var appDataFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "PortraitFinder");

        Directory.CreateDirectory(appDataFolder);

        var dbPath = Path.Combine(appDataFolder, "portraitfinder.db");

        var configuration = new ConfigurationBuilder()
            // .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        _host = Host
            .CreateDefaultBuilder()
            //.UseSerilog()
            .UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration))
            .ConfigureAppConfiguration((context, config) =>
            {
                // config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                config.AddEnvironmentVariables();
            })
            .ConfigureServices((context, services) =>
            {


                services.AddDbContext<PortraitFinderDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));

                services.AddSingleton<MainWindow>();

                services.AddScoped<IPortraitDatabaseService, PortraitDatabaseService>();
                // Register your app services here
                //      services.AddSingleton<IPortraitService, PortraitService>();
                //      services.AddSingleton<IImageRepository, ImageRepository>();
            })
            .Build();

        var log = Log.ForContext<App>();
        log.Warning("are we logging....?");

        await _host.StartAsync();

        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        if (_host is not null)
        {
            await _host.StopAsync();
            _host.Dispose();
        }

        base.OnExit(e);
    }
}