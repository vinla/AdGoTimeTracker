using AdGoTimeTracker.Core.Interfaces;
using AdGoTimeTracker.Core.Models;

namespace AdGoTimeTracker.MemoryStore
{
    public class InMemoryTimeTrackerEntryStore : ITimeTrackerEntryStore
    {
        private readonly List<TimeTrackerEntry> _entries = new();
        public Task AddEntryAsync(TimeTrackerEntry entry)
        {
            _entries.Add(entry);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<TimeTrackerEntry>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<TimeTrackerEntry>>(_entries);
        }
    }
}
