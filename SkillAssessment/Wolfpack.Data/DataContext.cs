using Microsoft.EntityFrameworkCore;
using Wolfpack.Data.Model;

namespace Wolfpack.Data
{
    public sealed class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            Wolfs = Set<WolfEntity>();
            Packs = Set<PackEntity>();
        }

        public DbSet<WolfEntity> Wolfs { get; set; }

        public DbSet<PackEntity> Packs { get; set; }
    }
}
