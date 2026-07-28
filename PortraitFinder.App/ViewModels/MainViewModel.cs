using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace PortraitFinder.App.ViewModels;

public partial class MainViewModel: ObservableObject
{
    public ObservableCollection<PortraitViewModel> AllPortraits { get; set; } = [];
    public IEnumerable<PortraitViewModel> SelectedPortraits => [.. AllPortraits.Where(p => p.IsSelected)];

    public ObservableCollection<ThumbnailRowViewModel> PortraitRows { get; } = [];
    private PortraitViewModel? _anchorSelection;
    
    private int _columns = 1;
    public int Columns
    {
        get => _columns;
        set => SetProperty(ref _columns, value);
    }

    [RelayCommand]
    public void SelectPortrait(PortraitViewModel? clicked)
    {
        bool ctrl = Keyboard.Modifiers.HasFlag(ModifierKeys.Control);
        bool shift = Keyboard.Modifiers.HasFlag(ModifierKeys.Shift);

        if (!ctrl && !shift)
        {
            foreach (var portrait in AllPortraits.Where(x => x.IsSelected))
            {
                portrait.IsSelected = false;
            }
            clicked!.IsSelected = true;
            _anchorSelection = clicked;
        }
        else if (ctrl && shift)
        {
            var startIndex = AllPortraits.IndexOf(_anchorSelection);
            var endIndex = AllPortraits.IndexOf(clicked);

            _anchorSelection = clicked;

            if (startIndex > endIndex)
            {
                (startIndex, endIndex) = (endIndex, startIndex);
            }

            for(var index = startIndex; index <= endIndex; index++)
            {
                AllPortraits[index].IsSelected = true;
            }
        }
        else if (ctrl)
        {
            _anchorSelection = clicked;
        }
        else if (shift)
        {
            var startIndex = AllPortraits.IndexOf(_anchorSelection);
            var endIndex = AllPortraits.IndexOf(clicked);

            _anchorSelection = clicked;

            if (startIndex > endIndex)
            {
                (startIndex, endIndex) = (endIndex, startIndex);
            }

            foreach(var (index, portrait) in AllPortraits.Index())
            {
                portrait.IsSelected = index >= startIndex && index <= endIndex;
            }
        }
        OnPropertyChanged(nameof(SelectedSmallPortrait));
        OnPropertyChanged(nameof(SelectedMediumPortrait));
        OnPropertyChanged(nameof(SelectedFullLengthPortrait));
    }

    public BitmapImage? SelectedSmallPortrait
    {
        get
        {
            if (SelectedPortraits.Count() > 1)
            {
                return DefaultSmallPortrait;
            }

            if (SelectedPortraits.FirstOrDefault() is PortraitViewModel selectedPortrait)
            {
                return selectedPortrait.SmallPortrait;
            }

            return null;
        }
    }
    public BitmapImage? SelectedMediumPortrait
    {
        get
        {
            if (SelectedPortraits.Count() > 1)
            {
                return DefaultMediumPortrait;
            }

            if (SelectedPortraits.FirstOrDefault() is PortraitViewModel selectedPortrait)
            {
                return selectedPortrait.MediumPortrait;
            }

            return null;
        }
    }

    public BitmapImage? SelectedFullLengthPortrait
    {
        get
        {
            if (SelectedPortraits.Count() > 1)
            {
                return DefaultFullLengthPortrait;
            }

            if (SelectedPortraits.FirstOrDefault() is PortraitViewModel selectedPortrait)
            {
                return selectedPortrait.FullLengthPortrait;
            }

            return null;
        }
    }

    private void OnPortraitsCollectionChanged(object? sender = null, NotifyCollectionChangedEventArgs? e = null)
    {
        if (e?.NewItems != null)
        {
            foreach (PortraitViewModel item in e.NewItems)
                item.PropertyChanged += OnPortraitPropertyChanged;
        }
        if (e?.OldItems != null)
        {
            foreach (PortraitViewModel item in e.OldItems)
                item.PropertyChanged -= OnPortraitPropertyChanged;
        }
    }

    private void OnPortraitPropertyChanged(object? sender = null, PropertyChangedEventArgs? e = null)
    {
        // An item's property changed; handle notification here
        OnPropertyChanged(nameof(PortraitRows));
    }

    private BitmapImage? _defaultSmallPortrait = null;
    public BitmapImage? DefaultSmallPortrait
    {
        get
        {
            if (_defaultSmallPortrait != null)
                return _defaultSmallPortrait;

            _defaultSmallPortrait = GetResourceImage("PortraitFinder.App.Resources.DefaultSmall.png");
            return _defaultSmallPortrait;
        }
    }

    private BitmapImage? _defaultMediumPortrait = null;
    public BitmapImage? DefaultMediumPortrait
    {
        get
        {
            if (_defaultMediumPortrait != null)
                return _defaultMediumPortrait;

            _defaultMediumPortrait = GetResourceImage("PortraitFinder.App.Resources.DefaultMedium.png");
            return _defaultMediumPortrait;
        }
    }

    private BitmapImage? _defaultFullLengthPortrait = null;
    public BitmapImage? DefaultFullLengthPortrait
    {
        get
        {
            if (_defaultFullLengthPortrait != null)
                return _defaultFullLengthPortrait;

            _defaultFullLengthPortrait = GetResourceImage("PortraitFinder.App.Resources.DefaultFullLength.png");
            return _defaultFullLengthPortrait;
        }
    }

    private static BitmapImage GetResourceImage(string resourcePath)
    {
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream(resourcePath) ?? throw new InvalidOperationException($"Resource not found: {resourcePath}");

        var bitmap = new BitmapImage();
        bitmap.BeginInit();
        bitmap.CacheOption = BitmapCacheOption.OnLoad; 
        bitmap.StreamSource = stream;
        bitmap.EndInit();
        bitmap.Freeze();
        return bitmap;
    }
}