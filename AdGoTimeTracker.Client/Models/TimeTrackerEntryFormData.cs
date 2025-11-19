namespace AdGoTimeTracker.Client.Models;

public class TimeTrackerEntryFormData
{
    public string Description { get; set; } = string.Empty;
    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
}