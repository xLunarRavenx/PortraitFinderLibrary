using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PortraitFinder.Model.Enums;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
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
    
#region filter flags

    [ObservableProperty]
    private FlagCollection<Armor> armor = new();
    partial void OnArmorChanged(FlagCollection<Armor> value)
    {
        foreach(var option in armor.Options)
            option.Refresh();
    }

    [ObservableProperty]
    private FlagCollection<Companion> companion = new();
    partial void OnCompanionChanged(FlagCollection<Companion> value)
    {
        foreach(var option in companion.Options)
            option.Refresh();
    }

    [ObservableProperty]
    private FlagCollection<Gender> gender = new();
    partial void OnGenderChanged(FlagCollection<Gender> value)
    {
        foreach(var option in gender.Options)
            option.Refresh();
    }

    [ObservableProperty]
    private FlagCollection<HairColor> hairColor = new();
    partial void OnHairColorChanged(FlagCollection<HairColor> value)
    {
        foreach(var option in hairColor.Options)
            option.Refresh();
    }

    [ObservableProperty]
    private FlagCollection<HairLength> hairLength = new();
    partial void OnHairLengthChanged(FlagCollection<HairLength> value)
    {
        foreach(var option in hairLength.Options)
            option.Refresh();
    }

    [ObservableProperty]
    private FlagCollection<HeadFeature> headFeature = new();
    partial void OnHeadFeatureChanged(FlagCollection<HeadFeature> value)
    {
        foreach(var option in headFeature.Options)
            option.Refresh();
    }

    [ObservableProperty]
    private FlagCollection<MythicPath> mythicPath = new();
    partial void OnMythicPathChanged(FlagCollection<MythicPath> value)
    {
        foreach(var option in mythicPath.Options)
            option.Refresh();
    }

    [ObservableProperty]
    private FlagCollection<PlayerClass> playerClass = new();
    partial void OnPlayerClassChanged(FlagCollection<PlayerClass> value)
    {
        foreach(var option in playerClass.Options)
            option.Refresh();
    }

    [ObservableProperty]
    private FlagCollection<Race> race = new();
    partial void OnRaceChanged(FlagCollection<Race> value)
    {
        foreach(var option in race.Options)
            option.Refresh();
    }

    [ObservableProperty]
    private FlagCollection<Surrounding> surrounding = new();
    partial void OnSurroundingChanged(FlagCollection<Surrounding> value)
    {
        foreach(var option in surrounding.Options)
            option.Refresh();
    }

    [ObservableProperty]
    private FlagCollection<Weapon> weapon = new();
    partial void OnWeaponChanged(FlagCollection<Weapon> value)
    {
        foreach(var option in weapon.Options)
            option.Refresh();
    }

    [ObservableProperty]
    private FlagCollection<Wing> wing = new();
    partial void OnWingChanged(FlagCollection<Wing> value)
    {
        foreach(var option in wing.Options)
            option.Refresh();
    }

#endregion filter flags

#region selectedFlags

    [ObservableProperty]
    private FlagCollection<Armor> selectedArmor = new();
    partial void OnSelectedArmorChanged(FlagCollection<Armor> value)
    {
        foreach(var option in selectedArmor.Options)
            option.Refresh();
        foreach(var portrait in SelectedPortraits)
            portrait.Armor = SelectedArmor.Flags;
    }

    [ObservableProperty]
    private FlagCollection<Companion> selectedCompanion = new();
    partial void OnSelectedCompanionChanged(FlagCollection<Companion> value)
    {
        foreach(var option in selectedCompanion.Options)
            option.Refresh();
        foreach(var portrait in SelectedPortraits)
            portrait.Companion = SelectedCompanion.Flags;
    }

    [ObservableProperty]
    private FlagCollection<Gender> selectedGender = new();
    partial void OnSelectedGenderChanged(FlagCollection<Gender> value)
    {
        foreach(var option in selectedGender.Options)
            option.Refresh();
        foreach(var portrait in SelectedPortraits)
            portrait.Gender = SelectedGender.Flags;
    }

    [ObservableProperty]
    private FlagCollection<HairColor> selectedHairColor = new();
    partial void OnSelectedHairColorChanged(FlagCollection<HairColor> value)
    {
        foreach(var option in selectedHairColor.Options)
            option.Refresh();
        foreach(var portrait in SelectedPortraits)
            portrait.HairColor = SelectedHairColor.Flags;
    }

    [ObservableProperty]
    private FlagCollection<HairLength> selectedHairLength = new();
    partial void OnSelectedHairLengthChanged(FlagCollection<HairLength> value)
    {
        foreach(var option in selectedHairLength.Options)
            option.Refresh();
        foreach(var portrait in SelectedPortraits)
            portrait.HairLength = SelectedHairLength.Flags;
    }

    [ObservableProperty]
    private FlagCollection<HeadFeature> selectedHeadFeature = new();
    partial void OnSelectedHeadFeatureChanged(FlagCollection<HeadFeature> value)
    {
        foreach(var option in selectedHeadFeature.Options)
            option.Refresh();
        foreach(var portrait in SelectedPortraits)
            portrait.HeadFeature = SelectedHeadFeature.Flags;
    }

    [ObservableProperty]
    private FlagCollection<MythicPath> selectedMythicPath = new();
    partial void OnSelectedMythicPathChanged(FlagCollection<MythicPath> value)
    {
        foreach(var option in selectedMythicPath.Options)
            option.Refresh();
        foreach(var portrait in SelectedPortraits)
            portrait.MythicPath = SelectedMythicPath.Flags;
    }

    [ObservableProperty]
    private FlagCollection<PlayerClass> selectedPlayerClass = new();
    partial void OnSelectedPlayerClassChanged(FlagCollection<PlayerClass> value)
    {
        foreach(var option in selectedPlayerClass.Options)
            option.Refresh();
        foreach(var portrait in SelectedPortraits)
            portrait.PlayerClass = SelectedPlayerClass.Flags;
    }

    [ObservableProperty]
    private FlagCollection<Race> selectedRace = new();
    partial void OnSelectedRaceChanged(FlagCollection<Race> value)
    {
        foreach(var option in selectedRace.Options)
            option.Refresh();
        foreach(var portrait in SelectedPortraits)
            portrait.Race = SelectedRace.Flags;
    }

    [ObservableProperty]
    private FlagCollection<Surrounding> selectedSurrounding = new();
    partial void OnSelectedSurroundingChanged(FlagCollection<Surrounding> value)
    {
        foreach(var option in selectedSurrounding.Options)
            option.Refresh();
        foreach(var portrait in SelectedPortraits)
            portrait.Surrounding = SelectedSurrounding.Flags;
    }

    [ObservableProperty]
    private FlagCollection<Weapon> selectedWeapon = new();
    partial void OnSelectedWeaponChanged(FlagCollection<Weapon> value)
    {
        foreach(var option in selectedWeapon.Options)
            option.Refresh();
        foreach(var portrait in SelectedPortraits)
            portrait.Weapon = SelectedWeapon.Flags;
    }

    [ObservableProperty]
    private FlagCollection<Wing> selectedWing = new();
    partial void OnSelectedWingChanged(FlagCollection<Wing> value)
    {
        foreach(var option in selectedWing.Options)
            option.Refresh();
        foreach(var portrait in SelectedPortraits)
            portrait.Wing = SelectedWing.Flags;
    }

#endregion selectedFlags

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
#pragma warning disable CS8604 // Possible null reference argument.
            var startIndex = AllPortraits.IndexOf(_anchorSelection);
            var endIndex = AllPortraits.IndexOf(clicked);
#pragma warning restore CS8604 // Possible null reference argument.

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
#pragma warning disable CS8604 // Possible null reference argument.
            var startIndex = AllPortraits.IndexOf(_anchorSelection);
            var endIndex = AllPortraits.IndexOf(clicked);
#pragma warning restore CS8604 // Possible null reference argument.

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

#region DetailsPortrait props

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

#endregion DetailsPortrait props

}