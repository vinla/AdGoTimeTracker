using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdGoTimeTracker.SqlEntityStore;

public class TimeTrackerEntryEntity
{
    [Key]
    public int Id { get; set; }

    [Column("StartTime", TypeName = "datetime")]
    public DateTime StartTime { get; set; }

    [Column("EndTime", TypeName = "datetime")]
    public DateTime EndTime { get; set; }
    public string Description { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;
}