using MudBlazor.Services;
using AdGoTimeTracker.Client.Pages;
using AdGoTimeTracker.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(sp =>
    new HttpClient
    {
        BaseAddress = new Uri(builder.Configuration["ApiUrl"] ??
            "http://localhost:5007")
    });
builder.Services.AddSingleton<AdGoTimeTracker.Client.Services.ITimeTrackerService, AdGoTimeTracker.Client.Services.TimeTrackerService>();
// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(AdGoTimeTracker.Client._Imports).Assembly);

app.Run();
