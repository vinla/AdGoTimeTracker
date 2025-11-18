using Microsoft.AspNetCore.Mvc;
using AdGoTimeTracker.Core.Models;
using AdGoTimeTracker.Core.Interfaces;

namespace AdGoTimeTracker.Api.Controllers
{
    [ApiController]
    [Route("/api/entries")]
    public class EntryController(ITimeTrackerEntryStore store) : ControllerBase
    {
        [HttpGet]
        public Task<IEnumerable<TimeTrackerEntry>> Get()
        {
            return store.GetAllAsync();
        }

        [HttpPost]
        public Task SaveEntry([FromBody] TimeTrackerEntry entry)
        {
            // TODO: Generate ID
            Console.WriteLine("Saving entry: " + entry.Description);
            return store.AddEntryAsync(entry);
        }
    }
}
