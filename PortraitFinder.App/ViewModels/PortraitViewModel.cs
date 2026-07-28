using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using PortraitFinder.Model;
using PortraitFinder.Model.Enums;

namespace PortraitFinder.App.ViewModels;

public class PortraitViewModel : Portrait, INotifyPropertyChanged
{
    public PortraitViewModel() {}
    public PortraitViewModel(Portrait portrait)
    {
        Id = portrait.Id;
        ThumbnailPath = portrait.ThumbnailPath;
        PortraitFolderPath = portrait.PortraitFolderPath;
        ImageLastModified = portrait.ImageLastModified;

        Gender = portrait.Gender;
        Race = portrait.Race;
        HairColor = portrait.HairColor;
        HairLength = portrait.HairLength;
        HeadFeature = portrait.HeadFeature;
        Wing = portrait.Wing;
        Weapon = portrait.Weapon;
        Armor = portrait.Armor;
        Companion = portrait.Companion;
        Surrounding = portrait.Surrounding;
        PlayerClass = portrait.PlayerClass;
        MythicPath = portrait.MythicPath;

        _originalGender = portrait.Gender;
        _originalRace = portrait.Race;
        _originalHairColor = portrait.HairColor;
        _originalHairLength = portrait.HairLength;
        _originalHeadFeature = portrait.HeadFeature;
        _originalWing = portrait.Wing;
        _originalWeapon = portrait.Weapon;
        _originalArmor = portrait.Armor;
        _originalCompanion = portrait.Companion;
        _originalSurrounding = portrait.Surrounding;
        _originalPlayerClass = portrait.PlayerClass;
        _originalMythicPath = portrait.MythicPath;
    }

    private readonly Gender _originalGender;
    private readonly Race _originalRace;
    private readonly HairColor _originalHairColor;
    private readonly HairLength _originalHairLength;
    private readonly HeadFeature _originalHeadFeature;
    private readonly Wing _originalWing;
    private readonly Weapon _originalWeapon;
    private readonly Armor _originalArmor;
    private readonly Companion _originalCompanion;
    private readonly Surrounding _originalSurrounding;
    private readonly PlayerClass _originalPlayerClass;
    private readonly MythicPath _originalMythicPath;

    private BitmapImage? _thumbnail = null;
    public BitmapImage? Thumbnail
    {
        get
        {
            if (_thumbnail != null)
                return _thumbnail;

            _thumbnail = GetImage(ThumbnailPath, 110);
            return _thumbnail;
        }
    }
    
    private BitmapImage? _smallPortrait = null;
    public BitmapImage? SmallPortrait
    {
        get
        {
            if (_smallPortrait != null)
                return _smallPortrait;

            _smallPortrait = GetImage(SmallPortraitPath);
            return _smallPortrait;
        }
    }

    private BitmapImage? _mediumPortrait = null;
    public BitmapImage? MediumPortrait
    {
        get
        {
            if (_mediumPortrait != null)
                return _mediumPortrait;

            _mediumPortrait = GetImage(MediumPortraitPath);
            return _mediumPortrait;
        }
    }

    private BitmapImage? _fullLengthPortrait = null;
    public BitmapImage? FullLengthPortrait
    {
        get
        {
            if (_fullLengthPortrait != null)
                return _fullLengthPortrait;

            _fullLengthPortrait = GetImage(FullLengthPortraitPath);
            return _fullLengthPortrait;
        }
    }


    public bool IsSelected
    {
        get;
        set { field = value; OnPropertyChanged(); }
    }
    
    public bool HasUnsavedChanges =>
        _originalGender != Gender ||
        _originalRace != Race ||
        _originalHairColor != HairColor ||
        _originalHairLength != HairLength ||
        _originalHeadFeature != HeadFeature ||
        _originalWing != Wing ||
        _originalWeapon != Weapon ||
        _originalArmor != Armor ||
        _originalCompanion != Companion ||
        _originalSurrounding != Surrounding ||
        _originalPlayerClass != PlayerClass ||
        _originalMythicPath != MythicPath;

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? name = null, object? sender = null, PropertyChangedEventArgs? e = null) =>
        PropertyChanged?.Invoke(this, e ?? new PropertyChangedEventArgs(name));

    private static BitmapImage? GetImage(string? path, int? size = null)
    {
        if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
        {
            return null; // Or return a fallback default image URI here
        }

        try
        {
            BitmapImage bitmap = new();

            bitmap.BeginInit();
            
            // This prevents the image file from being locked by the app
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            if (size.HasValue)
            {
                bitmap.DecodePixelWidth = size.Value;
            }
            bitmap.UriSource = new Uri(path);
            bitmap.EndInit();
            bitmap.Freeze();
            return bitmap;
        }
        catch
        {
            return null; // Silent catch prevents the UI row layout from crashing
        }
    }
}