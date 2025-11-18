using AdGoTimeTracker.Core.Models;

namespace AdGoTimeTracker.Core.Interfaces
{
    public interface ITimeTrackerEntryStore
    {
        Task<IEnumerable<TimeTrackerEntry>> GetAllAsync();
        Task AddEntryAsync(TimeTrackerEntry entry);
    }
}
