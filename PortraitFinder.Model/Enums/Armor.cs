namespace PortraitFinder.Model.Enums;

[Flags]
public enum Armor
{
    Unset = 0,
    Chain = 1 << 0,
    Cloth = 1 << 1,
    Leather = 1 << 2,
    Plate = 1 << 3,
    Shield = 1 << 4,
    Other = 1 << 5
}