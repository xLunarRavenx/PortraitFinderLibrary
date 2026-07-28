using Microsoft.EntityFrameworkCore;
using PortraitFinder.Data;
using PortraitFinder.Model;
using PortraitFinder.Model.Enums;

namespace PortraitFinder.Services;

public class PortraitDatabaseService : IPortraitDatabaseService
{
    private readonly PortraitFinderDbContext _context;

    public PortraitDatabaseService(PortraitFinderDbContext context)
    {
        _context = context;
    }

    public async Task<Portrait?> GetPortrait(int portraitId, bool asNoTracking = false)
    {
        return asNoTracking
            ? await _context.Portraits.AsNoTracking().FirstOrDefaultAsync(p => p.Id == portraitId)
            : await _context.Portraits.FirstOrDefaultAsync(p => p.Id == portraitId);
    }

    public async Task<Portrait?> GetPortrait(string thumbnailPath, bool asNoTracking = false)
    {
        return asNoTracking
            ? await _context.Portraits.AsNoTracking().FirstOrDefaultAsync(p => p.ThumbnailPath == thumbnailPath)
            : await _context.Portraits.FirstOrDefaultAsync(p => p.ThumbnailPath == thumbnailPath);
    }

    public async Task<List<Portrait>> GetAllPortraits(bool asNoTracking = false)
    {
        return asNoTracking
            ? await _context.Portraits.AsNoTracking().ToListAsync()
            : await _context.Portraits.ToListAsync();
    }
    
    public IQueryable<Portrait> GetPortraits(Portrait? portrait = null, bool asNoTracking = false)
    {
        var portraits = _context.Portraits.AsQueryable();
        if (portrait != null)
        {
            portraits = portraits.Where(p =>
                (portrait.Gender == Gender.Unset || (p.Gender & portrait.Gender) != Gender.Unset)
                && (portrait.Race == Race.Unset || (p.Race & portrait.Race) != Race.Unset)
                && (portrait.HairColor == HairColor.Unset || (p.HairColor & portrait.HairColor) != HairColor.Unset)
                && (portrait.HairLength == HairLength.Unset || (p.HairLength & portrait.HairLength) != HairLength.Unset)
                && (portrait.HeadFeature == HeadFeature.Unset || (p.HeadFeature & portrait.HeadFeature) != HeadFeature.Unset)
                && (portrait.Wing == Wing.Unset || (p.Wing & portrait.Wing) != Wing.Unset)
                && (portrait.Weapon == Weapon.Unset || (p.Weapon & portrait.Weapon) != Weapon.Unset)
                && (portrait.Armor == Armor.Unset || (p.Armor & portrait.Armor) != Armor.Unset)
                && (portrait.Companion == Companion.Unset || (p.Companion & portrait.Companion) != Companion.Unset)
                && (portrait.Surrounding == Surrounding.Unset || (p.Surrounding & portrait.Surrounding) != Surrounding.Unset)
                && (portrait.PlayerClass == PlayerClass.Unset || (p.PlayerClass & portrait.PlayerClass) != PlayerClass.Unset)
                && (portrait.MythicPath == MythicPath.Unset || (p.MythicPath & portrait.MythicPath) != MythicPath.Unset)
            );
        }
        if (asNoTracking)
        {
            portraits = portraits.AsNoTracking();
        }

        return portraits;
    }

    public async Task<Portrait> AddPortraitAsync(Portrait portrait)
    {
        _context.Portraits.Add(portrait);

        await _context.SaveChangesAsync();

        return portrait;
    }

    public async Task UpdatePortraitAsync(Portrait portrait)
    {
        _context.Portraits.Update(portrait);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePortraitAsync(int portraitId)
    {
        var portrait = await _context.Portraits.FirstOrDefaultAsync(p => p.Id == portraitId);
        if (portrait != null)
            _context.Portraits.Remove(portrait);

        await _context.SaveChangesAsync();
    }

    public async Task DeletePortraitAsync(Portrait portrait)
    {
        _context.Portraits.Remove(portrait);
        await _context.SaveChangesAsync();
    }
}
