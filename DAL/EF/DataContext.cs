using Microsoft.EntityFrameworkCore;
using VkWallReader.DAL.Entities;

namespace VkWallReader.DAL.EF;

public sealed class DataContext : DbContext
{
    public DbSet<CountedWall> CountedWalls { get; set; } = null!;

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        Database.Migrate();
    }
}
