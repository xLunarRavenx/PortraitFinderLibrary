namespace PortraitFinder.Model.Enums;

[Flags]
public enum HairColor
{
    Unset = 0,
    Blonde = 1 << 0,
    Brown = 1 << 1,
    Black = 1 << 2,
    Grey = 1 << 3,
    White = 1 << 4,
    Multicolored = 1 << 5,
    Purple = 1 << 6,
    Blue = 1 << 7,
    Green = 1 << 8,
    Yellow = 1 << 9,
    Orange = 1 << 10,
    Red = 1 << 11,
    Pink = 1 << 12,
    Other = 1 << 13
}