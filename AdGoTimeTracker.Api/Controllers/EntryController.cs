using Microsoft.AspNetCore.Mvc;
using AdGoTimeTracker.Core.Models;
using AdGoTimeTracker.Core.Interfaces;

namespace AdGoTimeTracker.Api.Controllers;

[ApiController]
[Route("/api/entries")]
public class EntryController(ITimeTrackerEntryStore store, IUserContext userContext) : ControllerBase
{
    [HttpGet]
    public Task<IEnumerable<TimeTrackerEntry>> Get()
    {
        return store.GetAllByUserIdAsync(userContext.SignedInUser.Id);
    }

    [HttpPost]
    public Task SaveEntry([FromBody] TimeTrackerEntry entry)
    {                        
        entry.UserId = userContext.SignedInUser.Id;
        return store.AddEntryAsync(entry);
    }
}
