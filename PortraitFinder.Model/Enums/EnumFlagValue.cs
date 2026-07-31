using System.ComponentModel.DataAnnotations;

namespace PortraitFinder.Model.Enums;

public class EnumFlagValue
{
    public Type? EnumType { get; set; }
    public string Name { get; set; } = "";
    public bool IsSelected { get; set; }
}