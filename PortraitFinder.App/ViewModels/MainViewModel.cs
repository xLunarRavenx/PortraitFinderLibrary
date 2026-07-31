using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PortraitFinder.Model.Enums;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace PortraitFinder.App.ViewModels;

public partial class MainViewModel: ObservableObject
{
    public MainViewModel()
    {
        Filters.FiltersChanged += (_, _) => RefreshVisiblePortraits();
    }

    public event Action<IEnumerable<PortraitViewModel>>? VisiblePortraitsChanged;    

    public ObservableCollection<PortraitViewModel> AllPortraits { get; set; } = [];

    public IEnumerable<PortraitViewModel> SelectedPortraits => [.. AllPortraits.Where(p => p.IsSelected)];

    [ObservableProperty]
    private bool hasSelectedPortrait;
    public ObservableCollection<DisplayFlagOption> SelectedPortraitTags { get; }= []; 


    public List<PortraitViewModel> VisiblePortraits { get; set; } = [];
    public ObservableCollection<ThumbnailRowViewModel> PortraitRows { get; } = [];
    private PortraitViewModel? _anchorSelection;


    [ObservableProperty]
    private FiltersViewModel filters = new();
    public void RefreshVisiblePortraits()
    {
        VisiblePortraits.Clear();
        VisiblePortraits.AddRange([.. AllPortraits.Where(p => 
            FilterMatchesPortraitOrNotSet(Filters.Armor, p.Armor)
            && FilterMatchesPortraitOrNotSet(Filters.Companion, p.Companion)
            && FilterMatchesPortraitOrNotSet(Filters.Gender, p.Gender)
            && FilterMatchesPortraitOrNotSet(Filters.HairColor, p.HairColor)
            && FilterMatchesPortraitOrNotSet(Filters.HairLength, p.HairLength)
            && FilterMatchesPortraitOrNotSet(Filters.HeadFeature, p.HeadFeature)
            && FilterMatchesPortraitOrNotSet(Filters.MythicPath, p.MythicPath)
            && FilterMatchesPortraitOrNotSet(Filters.PlayerClass, p.PlayerClass)
            && FilterMatchesPortraitOrNotSet(Filters.Race, p.Race)
            && FilterMatchesPortraitOrNotSet(Filters.Surrounding, p.Surrounding)
            && FilterMatchesPortraitOrNotSet(Filters.Weapon, p.Weapon)
            && FilterMatchesPortraitOrNotSet(Filters.Wing, p.Wing)
        )]);

        RefreshSelectedPortraitsDisplay();

        OnPropertyChanged(nameof(VisiblePortraits));
        VisiblePortraitsChanged?.Invoke(VisiblePortraits);
    }

    public void ResetFilters()
    {
        Filters.Reset();
    }

    private static bool FilterMatchesPortraitOrNotSet<T>(FlagCollection<T> filter, T portraitValue) where T : struct, Enum
    {
        return filter.Flags.Equals(default(T))
            || (filter.RequireAll && filter.Flags.And(portraitValue).Equals(filter.Flags))
            || (!filter.RequireAll && !filter.Flags.And(portraitValue).Equals(default(T)));
    }

    private void RefreshSelectedPortraitsDisplay()
    {
        foreach (var portrait in AllPortraits.Except(VisiblePortraits))
        {
            if (portrait.IsSelected)
            {
                portrait.IsSelected = false;
            }
        }

        HasSelectedPortrait = SelectedPortraits.Any();

        UpdateDisplayTagsForSelectedPortraits();

        OnPropertyChanged(nameof(SelectedSmallPortrait));
        OnPropertyChanged(nameof(SelectedMediumPortrait));
        OnPropertyChanged(nameof(SelectedFullLengthPortrait));
    }

    private void UpdateDisplayTagsForSelectedPortraits()
    {
        SelectedPortraitTags.Clear();

        if (SelectedPortraits.Count() == 1)
        {
            
        }

        var portraitTags = SelectedPortraits
            .SelectMany(portrait => 
                portrait.Armor.GetFlagValues().Select(t => new DisplayFlagOption { Name = t.Name, FilterType = t.EnumType!.Name, TagPresence = TagPresence.SingleSelection })
                    .Union(portrait.Companion.GetFlagValues().Select(t => new DisplayFlagOption { Name = t.Name, FilterType = t.EnumType!.Name, TagPresence = TagPresence.SingleSelection }))
                    .Union(portrait.Gender.GetFlagValues().Select(t => new DisplayFlagOption { Name = t.Name, FilterType = t.EnumType!.Name, TagPresence = TagPresence.SingleSelection }))
                    .Union(portrait.HairColor.GetFlagValues().Select(t => new DisplayFlagOption { Name = t.Name, FilterType = t.EnumType!.Name, TagPresence = TagPresence.SingleSelection }))
                    .Union(portrait.HairLength.GetFlagValues().Select(t => new DisplayFlagOption { Name = t.Name, FilterType = t.EnumType!.Name, TagPresence = TagPresence.SingleSelection }))
                    .Union(portrait.HeadFeature.GetFlagValues().Select(t => new DisplayFlagOption { Name = t.Name, FilterType = t.EnumType!.Name, TagPresence = TagPresence.SingleSelection }))
                    .Union(portrait.MythicPath.GetFlagValues().Select(t => new DisplayFlagOption { Name = t.Name, FilterType = t.EnumType!.Name, TagPresence = TagPresence.SingleSelection }))
                    .Union(portrait.PlayerClass.GetFlagValues().Select(t => new DisplayFlagOption { Name = t.Name, FilterType = t.EnumType!.Name, TagPresence = TagPresence.SingleSelection }))
                    .Union(portrait.Race.GetFlagValues().Select(t => new DisplayFlagOption { Name = t.Name, FilterType = t.EnumType!.Name, TagPresence = TagPresence.SingleSelection }))
                    .Union(portrait.Surrounding.GetFlagValues().Select(t => new DisplayFlagOption { Name = t.Name, FilterType = t.EnumType!.Name, TagPresence = TagPresence.SingleSelection }))
                    .Union(portrait.Weapon.GetFlagValues().Select(t => new DisplayFlagOption { Name = t.Name, FilterType = t.EnumType!.Name, TagPresence = TagPresence.SingleSelection }))
                    .Union(portrait.Wing.GetFlagValues().Select(t => new DisplayFlagOption { Name = t.Name, FilterType = t.EnumType!.Name, TagPresence = TagPresence.SingleSelection }))
            );

        /* Consider:
         *   Get list of ALL possible tags and add to the list as TagPresence.None
         */

        var distinctTags = SelectedPortraits.Count() == 1
            ? portraitTags.OrderBy(x => x.FilterType).ThenBy(x => x.Name).ToList()
            : portraitTags
                .GroupBy(x => $"{x.Name}_{x.FilterType}")
                .Select(g => g.Count() == SelectedPortraits.Count()
                    ? new DisplayFlagOption { Name = g.First().Name, FilterType = g.First().FilterType, TagPresence = TagPresence.All }
                    : new DisplayFlagOption { Name = g.First().Name, FilterType = g.First().FilterType, TagPresence = TagPresence.Some }
                )
                .OrderBy(x => x.FilterType)
                .ThenBy(x => x.Name)
                .ToList();;

        foreach(var tag in distinctTags)
        {
            SelectedPortraitTags.Add(tag);
        }
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

        RefreshSelectedPortraitsDisplay();
    }

}