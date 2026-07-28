using CommunityToolkit.Mvvm.ComponentModel;
using PortraitFinder.Model.Enums;

namespace PortraitFinder.App.ViewModels;

public partial class FlagCollection<T>: ObservableObject
    where T : struct, Enum
{
    [ObservableProperty]
    private T flags;

    public IReadOnlyList<FlagOption<T>> Options { get; set; }

    public FlagCollection()
    {
        Options = [..
            Enum.GetValues<T>()
                .Where(x => !x.Equals(default(T)))
                .Select(x => new FlagOption<T>(
                    x,
                    x.ToString(),
                    () => flags,
                    value => flags = value
                ))
        ];
    }

    partial void OnFlagsChanged(T value)
    {
        foreach (var option in Options)
            option.Refresh();
    }

    public bool Has(T flag) => Flags.HasFlag(flag);
    public void Add(T flag) => Flags.Or(flag);
    public void Remove(T flag) => Flags.AndNot(flag);
    public void Toggle(T flag) => Flags.Xor(flag);
}
