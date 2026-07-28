namespace PortraitFinder.Model.Enums;

[Flags]
public enum Wing
{
    Unset = 0,
    Demon = 1 << 0,
    Feathery = 1 << 1,
    Other = 1 << 2
}