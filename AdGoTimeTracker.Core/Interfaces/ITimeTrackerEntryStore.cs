using AdGoTimeTracker.Core.Models;

namespace AdGoTimeTracker.Core.Interfaces;

public interface ITimeTrackerEntryStore
{
    Task<IEnumerable<TimeTrackerEntry>> GetAllByUserIdAsync(string userId);
    Task AddEntryAsync(TimeTrackerEntry entry);
}