using AdGoTimeTracker.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AdGoTimeTracker.SqlEntityStore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }        

    public DbSet<TimeTrackerEntryEntity> TimeTrackerEntries { get; set; }
}