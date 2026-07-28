using System.ComponentModel.DataAnnotations;
using PortraitFinder.Model.Enums;

namespace PortraitFinder.Model;

public class Portrait : Filters
{
    [Key]
    public int Id { get; set; }
    public string? ThumbnailPath { get; set; }
    public string? PortraitFolderPath { get; set; }
    public DateTime? ImageLastModified { get; set; }

    public string? SmallPortraitPath => string.IsNullOrEmpty(PortraitFolderPath) ? null : Path.Combine(PortraitFolderPath, "Small.png");
    public string? MediumPortraitPath => string.IsNullOrEmpty(PortraitFolderPath) ? null : Path.Combine(PortraitFolderPath, "Medium.png");
    public string? FullLengthPortraitPath => string.IsNullOrEmpty(PortraitFolderPath) ? null : Path.Combine(PortraitFolderPath, "FullLength.png");
}
