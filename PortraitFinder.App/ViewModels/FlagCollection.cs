using CommunityToolkit.Mvvm.ComponentModel;
using PortraitFinder.Model.Enums;

namespace PortraitFinder.App.ViewModels;

public partial class FlagCollection<T>: ObservableObject
    where T : struct, Enum
{
    private T flags;
    public T Flags
    {
        get => flags;
        set
        {
            flags = value;
            OnPropertyChanged(nameof(Flags));
            OnFlagsChanged(value);
        }
    }

    [ObservableProperty]
    private FilterMode filterMode = FilterMode.Any;
    partial void OnFilterModeChanged(FilterMode value)
    {
        OnPropertyChanged(nameof(RequireAll));
    }
    public bool RequireAll
    {
        get => FilterMode == FilterMode.All;
        set => FilterMode = value
            ? FilterMode.All
            : FilterMode.Any;
    }

    public IReadOnlyList<FlagOption<T>> Options { get; set; }

    public FlagCollection()
    {
        Options = [..
            Enum.GetValues<T>()
                .Where(x => !x.Equals(default(T)))
                .Select(x => new FlagOption<T>(
                    x,
                    x.ToString(),
                    () => Flags,
                    value => Flags = value
                ))
        ];
    }

    void OnFlagsChanged(T value)
    {
        foreach (var option in Options)
            option.Refresh();
    }

    public bool Has(T flag) => Flags.HasFlag(flag);
    public void Add(T flag) => Flags = Flags.Or(flag);
    public void Remove(T flag) => Flags = Flags.AndNot(flag);
    public void Toggle(T flag) => Flags = Flags.Xor(flag);
}
