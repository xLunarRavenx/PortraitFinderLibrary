using PortraitFinder.Model;

namespace PortraitFinder.Services;

public interface IPortraitDatabaseService
{
    Task<Portrait?> GetPortrait(int portraitId, bool asNoTracking = false);
    Task<Portrait?> GetPortrait(string thumbnailPath, bool asNoTracking = false);
    IQueryable<Portrait> GetPortraits(Portrait? portrait = null, bool asNoTracking = false);
    Task<List<Portrait>> GetAllPortraits(bool asNoTracking = false);
    Task<Portrait> AddPortraitAsync(Portrait portrait);
    Task UpdatePortraitAsync(Portrait portrait);
    Task DeletePortraitAsync(int portraitId);
    Task DeletePortraitAsync(Portrait portrait);
}
