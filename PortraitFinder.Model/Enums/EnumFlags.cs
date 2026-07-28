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
}