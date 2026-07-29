using System.Runtime.CompilerServices;
using System.Windows;
using Microsoft.Extensions.Logging;
using PortraitFinder.App.ViewModels;
using PortraitFinder.Services;

namespace PortraitFinder.App;

public partial class MainWindow : Window
{
    private readonly ILogger<MainWindow> _log;
    private readonly IPortraitDatabaseService _db;
    private readonly MainViewModel _mainViewModel;

    private const int ThumbnailWidth = 175;
    private const int ThumbnailScrollViewScrollbarWitdh = 30;

    public MainWindow(ILogger<MainWindow> log, IPortraitDatabaseService db)
    {
        _log = log;
        _db = db;
        _mainViewModel = new MainViewModel();

        InitializeComponent();

        Loaded += MainWindow_Loaded;

        DataContext = _mainViewModel;

        _mainViewModel.VisiblePortraitsChanged += portraits => RecalculatePortraitRows("trigger", portraits);

        _log.LogInformation("Finished initializing MainWindow.");
    }

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        _mainViewModel.AllPortraits = [.. (await _db.GetAllPortraits(true)).Select(x => new PortraitViewModel(x))];
        _mainViewModel.VisiblePortraits.AddRange(_mainViewModel.AllPortraits);

        _log.LogInformation("Finished loading images from the db.");

        RecalculatePortraitRows(nameof(MainWindow_Loaded), _mainViewModel.VisiblePortraits);

        _log.LogInformation("Finished rendering images .");
    }  

    private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        RecalculatePortraitRows(nameof(Window_SizeChanged), _mainViewModel.VisiblePortraits);
    }

    private void RecalculatePortraitRows([CallerMemberName] string caller = "", IEnumerable<PortraitViewModel>? visiblePortraits = null)
    {
        double availableWidth = PortraitsListBox.ActualWidth - ThumbnailScrollViewScrollbarWitdh;
        if (visiblePortraits == null || availableWidth <= 0)
        {
            _log.LogInformation("{caller} | no images found....", caller);
            return;
        }

        var columns = (int)Math.Max(1, Math.Floor(availableWidth / ThumbnailWidth));

        _log.LogInformation("{caller} | Should make {columns} columns", caller, columns);

        _mainViewModel.PortraitRows.Clear();
        for (int i = 0; i < visiblePortraits.Count(); i += columns)
        {
            _mainViewModel.PortraitRows.Add(new ThumbnailRowViewModel 
            { 
                RowThumbnails = [.. visiblePortraits.Skip(i).Take(columns)]
            });
        }
    }

}