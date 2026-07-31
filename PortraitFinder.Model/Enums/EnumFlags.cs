namespace PortraitFinder.Model.Enums;

public static class EnumFlags
{
    public static T And<T>(this T left, T right)
        where T : struct, Enum
    {
        ulong leftValue = Convert.ToUInt64(left);
        ulong rightValue = Convert.ToUInt64(right);
        return (T)Enum.ToObject(typeof(T), leftValue & rightValue);
    }

    public static T Or<T>(this T left, T right)
        where T : struct, Enum
    {
        ulong leftValue = Convert.ToUInt64(left);
        ulong rightValue = Convert.ToUInt64(right);
        return (T)Enum.ToObject(typeof(T), leftValue | rightValue);
    }

    public static T AndNot<T>(this T left, T right)
        where T : struct, Enum
    {
        ulong leftValue = Convert.ToUInt64(left);
        ulong rightValue = Convert.ToUInt64(right);
        return (T)Enum.ToObject(typeof(T), leftValue & ~rightValue);
    }

    public static T Xor<T>(this T left, T right)
        where T : struct, Enum
    {
        ulong leftValue = Convert.ToUInt64(left);
        ulong rightValue = Convert.ToUInt64(right);

        return (T)Enum.ToObject(typeof(T), leftValue ^ rightValue);
    }

    private static Dictionary<Type, List<Enum>> _flagValuesDictionary = new();
    public static List<EnumFlagValue> GetFlagValues<T>(this T value)
        where T : struct, Enum
    {
        if (!_flagValuesDictionary.TryGetValue(typeof(T), out var flagValues))
        {
            flagValues = [.. Enum.GetValues<T>().Except([default])];
            _flagValuesDictionary[typeof(T)] = flagValues;
        }

        return [.. flagValues
            .Where(value.HasFlag)
            .Select(f => new EnumFlagValue
            {
                EnumType = typeof(T),
                Name = f.ToString(),
                IsSelected = value.HasFlag(f)
            })
        ];
    }
}