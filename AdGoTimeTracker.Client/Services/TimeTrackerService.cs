using AdGoTimeTracker.Core.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace AdGoTimeTracker.Client.Services;

public interface ITimeTrackerService
{
    FetchStatus FetchStatus { get; }
    Task LoadEntries();
    IEnumerable<TimeTrackerEntry> TimeTrackerEntries { get; }
        
    Task SaveEntryAsync(TimeTrackerEntry entry);

    event Action OnChange;
}

public class TimeTrackerService(HttpClient httpClient) : ITimeTrackerService
{
    private FetchStatus _fetchStatus = FetchStatus.Loading;
    private readonly List<TimeTrackerEntry> _entries = new();
    public IEnumerable<TimeTrackerEntry> TimeTrackerEntries => _entries.AsEnumerable();
    public FetchStatus FetchStatus => _fetchStatus;

    public async Task SaveEntryAsync(TimeTrackerEntry entry)
    {
        await httpClient.PostAsJsonAsync("api/entries", entry);
        _entries.Add(entry);            
        Dispatcher.CreateDefault().InvokeAsync(OnChange);
    }

    public async Task LoadEntries()
    {
        _fetchStatus = FetchStatus.Loading;

        _entries.Clear();
        try
        {
            _entries.AddRange(await httpClient.GetFromJsonAsync<List<TimeTrackerEntry>>("api/entries") ?? []);
            _fetchStatus = FetchStatus.Done;
        }
        catch (HttpRequestException)
        {
            // TODO: Log something useful
            _fetchStatus = FetchStatus.Failed;
        }
    }

    public event Action OnChange;
}

public enum FetchStatus
{
    Loading,
    Done,
    Failed
}