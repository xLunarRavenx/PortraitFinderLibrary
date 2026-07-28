namespace PortraitFinder.Model.Enums;

[Flags]
public enum Gender
{
    Unset = 0,
    Female = 1 << 0,
    Male = 1 << 1,
    Other = 1 << 2
}