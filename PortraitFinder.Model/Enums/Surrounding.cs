namespace PortraitFinder.Model.Enums;

[Flags]
public enum Surrounding
{
    Unset = 0,
    Cave = 1 << 0,
    City = 1 << 1,
    Clouds = 1 << 2,
    Desert = 1 << 3,
    Dungeon = 1 << 4,
    Fire = 1 << 5,
    Forest = 1 << 6,
    Indoors = 1 << 7,
    Mountain = 1 << 8,
    Plains = 1 << 9,
    Plants = 1 << 10,
    Rain = 1 << 11,
    Sky = 1 << 12,
    Snow = 1 << 13,
    Stars = 1 << 14,
    Tree = 1 << 15,
    Water = 1 << 16,
    Other = 1 << 17
}