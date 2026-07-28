using CommunityToolkit.Mvvm.ComponentModel;

namespace PortraitFinder.App.ViewModels;

public partial class FlagOption<T> : ObservableObject
    where T : struct, Enum
{
    public FlagOption(T value, string name, Func<T> getFlags, Action<T> setFlags)
    {
        this.value = value;
        this.name = name;
        _getFlags = getFlags;
        _setFlags = setFlags;
    }

    protected readonly Func<T> _getFlags;
    protected readonly Action<T> _setFlags;

    [ObservableProperty]
    public T value;

    [ObservableProperty]
    private string name;

    public bool IsSelected
    {
        get => _getFlags().HasFlag(Value);
        set 
        {
            var flags = Convert.ToUInt64(_getFlags());
            var val = Convert.ToUInt64(Value);
            flags = value
                ?  flags | val
                : flags & ~val;

            var newFlag = (T)Enum.ToObject(typeof(T), flags);
            _setFlags(newFlag);
            Refresh();
        }
    }

    public void Refresh() => OnPropertyChanged(nameof(IsSelected));
}