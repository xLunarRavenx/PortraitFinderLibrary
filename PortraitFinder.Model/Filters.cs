using PortraitFinder.Model.Enums;

namespace PortraitFinder.Model;

public class Filters
{
    public Gender Gender { get; set; }
    public Race Race { get; set; }
    public HairColor HairColor { get; set; }
    public HairLength HairLength { get; set; }
    public HeadFeature HeadFeature { get; set; }
    public Wing Wing { get; set; }
    public Weapon Weapon { get; set; }
    public Armor Armor { get; set; }
    public Companion Companion { get; set; }
    public Surrounding Surrounding { get; set; }
    public PlayerClass PlayerClass { get; set; }
    public MythicPath MythicPath { get; set; }
}
