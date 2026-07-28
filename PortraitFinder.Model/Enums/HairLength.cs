namespace PortraitFinder.Model.Enums;

[Flags]
public enum HairLength
{
    Unset = 0,
    Bald = 1 << 0,
    Shaved = 1 << 1,
    VeryShort = 1 << 2,
    Short = 1 << 3,
    AboveShoulder = 1 << 4,
    Shoulder = 1 << 5,
    MidBack = 1 << 6,
    Long = 1 << 7,
    Other = 1 << 8
}