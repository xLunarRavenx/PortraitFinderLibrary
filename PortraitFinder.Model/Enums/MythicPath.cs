namespace PortraitFinder.Model.Enums;

[Flags]
public enum MythicPath
{
    Unset = 0,
    Aeon = 1 << 0,
    Angel = 1 << 1,
    Azata = 1 << 2,
    Demon = 1 << 3,
    Devil = 1 << 4,
    GoldDragon = 1 << 5,
    Legend = 1 << 6,
    Lich = 1 << 7,
    SwarmThatWalks = 1 << 8,
    Trickster = 1 << 9
}