using CommunityToolkit.Mvvm.ComponentModel;

namespace PortraitFinder.App.ViewModels;

public partial class FlagOptionViewModel<TEnum> : ObservableObject
    where TEnum : struct, Enum
{
    private readonly FlagEnumViewModel<TEnum> _parent;

    public TEnum Flag { get; }

    public string Name => Flag.ToString();

    public FlagOptionViewModel(FlagEnumViewModel<TEnum> parent, TEnum flag)
    {
        _parent = parent;
        Flag = flag;
    }
    public bool IsChecked
    {
        get => _parent.HasFlag(Flag);
        set => _parent.SetFlag(Flag, value);
    }

    internal void Refresh()
    {
        OnPropertyChanged(nameof(IsChecked));
    }
}
