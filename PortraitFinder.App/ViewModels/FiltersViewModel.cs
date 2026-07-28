using CommunityToolkit.Mvvm.ComponentModel;
using PortraitFinder.Model;
using PortraitFinder.Model.Enums;
using System.Collections.ObjectModel;

namespace PortraitFinder.App.ViewModels;

public class FiltersViewModel : ObservableObject
{
    public Filters Filters { get; }

    public ObservableCollection<FlagEnumViewModelBase> Groups { get;  }

    public FiltersViewModel()
    {
        Filters = new Filters();

        Groups =
        [
            new FlagEnumViewModel<Gender>("Gender", () => Filters.Gender, value => Filters.Gender = value),
            new FlagEnumViewModel<Race>("Race", () => Filters.Race, value => Filters.Race = value),
            new FlagEnumViewModel<HairColor>("HairColor", () => Filters.HairColor, value => Filters.HairColor = value),
            new FlagEnumViewModel<HairLength>("HairLength", () => Filters.HairLength, value => Filters.HairLength = value),
            new FlagEnumViewModel<HeadFeature>("HeadFeature", () => Filters.HeadFeature, value => Filters.HeadFeature = value),
            new FlagEnumViewModel<Wing>("Wing", () => Filters.Wing, value => Filters.Wing = value),
            new FlagEnumViewModel<Weapon>("Weapon", () => Filters.Weapon, value => Filters.Weapon = value),
            new FlagEnumViewModel<Armor>("Armor", () => Filters.Armor, value => Filters.Armor = value),
            new FlagEnumViewModel<Companion>("Companion", () => Filters.Companion, value => Filters.Companion = value),
            new FlagEnumViewModel<Surrounding>("Surrounding", () => Filters.Surrounding, value => Filters.Surrounding = value),
            new FlagEnumViewModel<PlayerClass>("PlayerClass", () => Filters.PlayerClass, value => Filters.PlayerClass = value),
            new FlagEnumViewModel<MythicPath>("MythicPath", () => Filters.MythicPath, value => Filters.MythicPath = value)
        ];
    }
}
