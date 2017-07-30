using System.IO;
using Microsoft.EntityFrameworkCore;

namespace QuickCharts.Models.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<ConstructionSite> ConstructionSites { get; set; }
        public DbSet<Building> Buildings { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }
    }
}