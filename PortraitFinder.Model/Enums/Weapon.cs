namespace PortraitFinder.Model.Enums;

[Flags]
public enum Weapon
{
    Unset = 0,
    Aura = 1 << 0,
    AxeOneHand = 1 << 1,
    AxeTwoHand = 1 << 2,
    Bomb = 1 << 3,
    Book = 1 << 4,
    Bow = 1 << 5,
    ClubOneHand = 1 << 6,
    ClubTwoHand = 1 << 7,
    Crossbow = 1 << 8,
    Flail = 1 << 9,
    Glaive = 1 << 10,
    HammerOneHand = 1 << 11,
    HammerTwoHand = 1 << 12,
    Knife = 1 << 13,
    Potion = 1 << 14,
    Scroll = 1 << 15,
    Shield = 1 << 16,
    Spear = 1 << 17,
    SpellOneHand = 1 << 18,
    SpellTwoHand = 1 << 19,
    Staff = 1 << 20,
    SwordOneHand = 1 << 21,
    SwordTwoHand = 1 << 22,
    Wand = 1 << 23,
    Whip = 1 << 24,
    Other = 1 << 25
}