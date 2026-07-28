namespace PortraitFinder.Model.Enums;

[Flags]
public enum Companion
{
    Unset = 0,
    Animal = 1 << 0,
    Companion = 1 << 1,
    Horse = 1 << 2,
    Mount = 1 << 3,
    Undead = 1 << 4,
    Other = 1 << 5
}