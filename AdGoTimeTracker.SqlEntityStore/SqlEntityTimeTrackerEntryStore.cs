using AdGoTimeTracker.Core.Interfaces;
using AdGoTimeTracker.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AdGoTimeTracker.SqlEntityStore;

public class SqlEntityTimeTrackerEntryStore(ApplicationDbContext dbContext) : ITimeTrackerEntryStore
{
    public Task AddEntryAsync(TimeTrackerEntry entry)
    {
        var entity = new TimeTrackerEntryEntity
        {
            UserId = entry.UserId,
            Description = entry.Description,
            EndTime = entry.EndTime.DateTime,
            StartTime = entry.StartTime.DateTime,
        };
        dbContext.TimeTrackerEntries.Add(entity);
        return dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<TimeTrackerEntry>> GetAllByUserIdAsync(string userId)
    {
        var data = await dbContext.TimeTrackerEntries.Where(tte => tte.UserId == userId).ToListAsync();
        return data.Select(e => new TimeTrackerEntry
        {
            UserId = e.UserId,
            Description = e.Description,
            EndTime = e.EndTime,
            StartTime = e.StartTime,
        });
    }
}