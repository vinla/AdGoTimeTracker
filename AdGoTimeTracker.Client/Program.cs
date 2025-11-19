using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddSingleton(sp =>
    new HttpClient
    {
        BaseAddress = new Uri(builder.Configuration["ApiUrl"] ??
            "http://localhost:5000")
    });
builder.Services.AddSingleton<AdGoTimeTracker.Client.Services.ITimeTrackerService, AdGoTimeTracker.Client.Services.TimeTrackerService>();
builder.Services.AddMudServices();

await builder.Build().RunAsync();
