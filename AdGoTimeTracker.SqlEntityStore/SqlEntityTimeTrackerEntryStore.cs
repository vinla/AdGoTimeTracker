using AdGoTimeTracker.Core.Interfaces;
using AdGoTimeTracker.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AdGoTimeTracker.SqlEntityStore
{
    public class SqlEntityTimeTrackerEntryStore(ApplicationDbContext dbContext) : ITimeTrackerEntryStore
    {
        public Task AddEntryAsync(TimeTrackerEntry entry)
        {
            dbContext.TimeTrackerEntries.Add(entry);
            return dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TimeTrackerEntry>> GetAllAsync()
        {
            var data = await dbContext.TimeTrackerEntries.ToListAsync();
            return data;
        }
    }
}
