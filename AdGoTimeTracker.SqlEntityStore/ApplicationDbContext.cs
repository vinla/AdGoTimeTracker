using AdGoTimeTracker.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AdGoTimeTracker.SqlEntityStore
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=AdGo;ConnectRetryCount=0");
        }

        public DbSet<TimeTrackerEntry> TimeTrackerEntries { get; set; }
    }
}
