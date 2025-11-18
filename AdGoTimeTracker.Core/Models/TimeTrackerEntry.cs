namespace AdGoTimeTracker.Core.Models
{
    public class TimeTrackerEntry
    {        
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public string Description { get; set; } = string.Empty;
        public TimeSpan Duration => EndTime - StartTime;
    }
}
