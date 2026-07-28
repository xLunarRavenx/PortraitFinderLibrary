using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections;
using System.Collections.ObjectModel;

namespace PortraitFinder.App.ViewModels;

public class FlagEnumViewModel<TEnum> : FlagEnumViewModelBase
    where TEnum : struct, Enum
{
    private readonly Func<TEnum> _getter;
    private readonly Action<TEnum> _setter;

    public ObservableCollection<FlagOptionViewModel<TEnum>> FlagOptions { get; } = new();
    public override IEnumerable Options => FlagOptions;

    public FlagEnumViewModel(string name, Func<TEnum> getter, Action<TEnum> setter)
        : base(name, true)
    {
        _getter = getter;
        _setter = setter;

        FlagOptions = new(
            Enum.GetValues<TEnum>()
                .Where(x => Convert.ToUInt64(x) != 0) // ignore Unset/None
                .Select(x => new FlagOptionViewModel<TEnum>(this, x)));
    }

    public FlagEnumViewModel(string name, bool isExpanded, Func<TEnum> getter, Action<TEnum> setter)
        : base(name, isExpanded)
    {
        _getter = getter;
        _setter = setter;

        FlagOptions = new(
            Enum.GetValues<TEnum>()
                .Where(x => Convert.ToUInt64(x) != 0) // ignore Unset/None
                .Select(x => new FlagOptionViewModel<TEnum>(this, x)));
    }

    internal bool HasFlag(TEnum flag)
    {
        ulong current = Convert.ToUInt64(_getter());
        ulong value = Convert.ToUInt64(flag);
        return (current & value) == value;
    }

    internal void SetFlag(TEnum flag, bool enabled)
    {
        ulong current = Convert.ToUInt64(_getter());
        ulong value = Convert.ToUInt64(flag);

        current = enabled
            ? current | value
            : current & ~value;

        _setter((TEnum)Enum.ToObject(typeof(TEnum), current));

        foreach (var option in FlagOptions)
            option.Refresh();
    }
}
