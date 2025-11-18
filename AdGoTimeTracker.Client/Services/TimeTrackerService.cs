using AdGoTimeTracker.Core.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace AdGoTimeTracker.Client.Services
{
    public interface ITimeTrackerService
    {
        Task LoadEntries();
        IEnumerable<TimeTrackerEntry> TimeTrackerEntries { get; }
        
        Task SaveEntryAsync(TimeTrackerEntry entry);

        event Action OnChange;
    }

    public class TimeTrackerService(HttpClient httpClient) : ITimeTrackerService
    {
        private readonly List<TimeTrackerEntry> _entries = new();
        public IEnumerable<TimeTrackerEntry> TimeTrackerEntries => _entries.AsEnumerable();

        public async Task SaveEntryAsync(TimeTrackerEntry entry)
        {
            await httpClient.PostAsJsonAsync("api/entries", entry);
            _entries.Add(entry);            
            Dispatcher.CreateDefault().InvokeAsync(OnChange);
        }

        public async Task LoadEntries()
        { 
            _entries.Clear();
            _entries.AddRange(await httpClient.GetFromJsonAsync<List<TimeTrackerEntry>>("api/entries") ?? []);
        }

        public event Action OnChange;
    }
}
