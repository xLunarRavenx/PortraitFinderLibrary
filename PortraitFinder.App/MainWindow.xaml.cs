using System.IO;
using System.Windows;
using System.Windows.Input;
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

        _log.LogInformation("Finished initializing MainWindow.");
    }

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        _mainViewModel.AllPortraits = [.. (await _db.GetAllPortraits(true)).Select(x => new PortraitViewModel(x))];

        _log.LogInformation("Finished loading images from the db.");

        RecalculatePortraitRows();

        _log.LogInformation("Finished rendering images .");

    }  

    private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (_mainViewModel.AllPortraits.Count == 0)
        {
            return; 
        }
        RecalculatePortraitRows();
    }

    private void RecalculatePortraitRows()
    {
        double availableWidth = PortraitsListBox.ActualWidth - ThumbnailScrollViewScrollbarWitdh;
        if (availableWidth <= 0)
        {
            _log.LogInformation("no images found....");
            return;
        }

        var columns = (int)Math.Max(1, Math.Floor(availableWidth / ThumbnailWidth));

        _log.LogInformation("Should make {columns} columns", columns);

        _mainViewModel.PortraitRows.Clear();
        for (int i = 0; i < _mainViewModel.AllPortraits.Count; i += columns)
        {
            _mainViewModel.PortraitRows.Add(new ThumbnailRowViewModel 
            { 
                RowThumbnails = [.. _mainViewModel.AllPortraits.Skip(i).Take(columns)]
            });
        }
    }

}