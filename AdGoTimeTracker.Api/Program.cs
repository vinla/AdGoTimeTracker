var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddSingleton<AdGoTimeTracker.Core.Interfaces.ITimeTrackerEntryStore, AdGoTimeTracker.MemoryStore.InMemoryTimeTrackerEntryStore>();
builder.Services.AddLocalDbTimeTrackerEntryStore("AdGo");
builder.Services.AddControllers();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();
app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

app.Run();
