namespace PortraitFinder.Model.Enums;

[Flags]
public enum Race
{
    Unset = 0,
    Aasimar = 1 << 0,
    Dhampir = 1 << 1,
    Dwarf = 1 << 2,
    Elf = 1 << 3,
    Gnome = 1 << 4,
    Halfling = 1 << 5,
    HalfElf = 1 << 6,
    HalfOrc = 1 << 7,
    Human = 1 << 8,
    Kitsune = 1 << 9,
    Oread = 1 << 10,
    Tiefling = 1 << 11,
    Other = 1 << 12
}