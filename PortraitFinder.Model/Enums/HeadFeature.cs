namespace PortraitFinder.Model.Enums;

[Flags]
public enum HeadFeature
{
    Unset = 0,
    Blind = 1 << 0,
    Crown = 1 << 1,
    Eyewear = 1 << 2,
    GlowingEyes = 1 << 3,
    Halo = 1 << 4,
    Hat = 1 << 5,
    Headwear = 1 << 6,
    Helm = 1 << 7,
    Horns = 1 << 8,
    Mask = 1 << 9,
    Other = 1 << 10
}