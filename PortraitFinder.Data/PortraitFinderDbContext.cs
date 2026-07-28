using Microsoft.EntityFrameworkCore;
using PortraitFinder.Model;

namespace PortraitFinder.Data;

public class PortraitFinderDbContext : DbContext
{
    public PortraitFinderDbContext(DbContextOptions<PortraitFinderDbContext> options)
        : base(options)
    { }

    public DbSet<Portrait> Portraits => Set<Portrait>();
}