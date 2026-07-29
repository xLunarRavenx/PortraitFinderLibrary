using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using PortraitFinder.Model.Enums;

namespace PortraitFinder.App.ViewModels;

public partial class FiltersViewModel : ObservableObject
{
    public event EventHandler? FiltersChanged;

    public FiltersViewModel()
    {
        Armor.PropertyChanged += OnAnyFilterChanged;
        Companion.PropertyChanged += OnAnyFilterChanged;
        Gender.PropertyChanged += OnAnyFilterChanged;
        HairColor.PropertyChanged += OnAnyFilterChanged;
        HairLength.PropertyChanged += OnAnyFilterChanged;
        HeadFeature.PropertyChanged += OnAnyFilterChanged;
        MythicPath.PropertyChanged += OnAnyFilterChanged;
        PlayerClass.PropertyChanged += OnAnyFilterChanged;
        Race.PropertyChanged += OnAnyFilterChanged;
        Surrounding.PropertyChanged += OnAnyFilterChanged;
        Weapon.PropertyChanged += OnAnyFilterChanged;
        Wing.PropertyChanged += OnAnyFilterChanged;
    }

    private void OnAnyFilterChanged(object? sender, PropertyChangedEventArgs e) => FiltersChanged?.Invoke(this, EventArgs.Empty);

    [ObservableProperty] private FlagCollection<Armor> armor = new();
    [ObservableProperty] private FlagCollection<Companion> companion = new();
    [ObservableProperty] private FlagCollection<Gender> gender = new();
    [ObservableProperty] private FlagCollection<HairColor> hairColor = new();
    [ObservableProperty] private FlagCollection<HairLength> hairLength = new();
    [ObservableProperty] private FlagCollection<HeadFeature> headFeature = new();
    [ObservableProperty] private FlagCollection<MythicPath> mythicPath = new();
    [ObservableProperty] private FlagCollection<PlayerClass> playerClass = new();
    [ObservableProperty] private FlagCollection<Race> race = new();
    [ObservableProperty] private FlagCollection<Surrounding> surrounding = new();
    [ObservableProperty] private FlagCollection<Weapon> weapon = new();
    [ObservableProperty] private FlagCollection<Wing> wing = new();
}
