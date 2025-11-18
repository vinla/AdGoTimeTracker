var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddSingleton<AdGoTimeTracker.Core.Interfaces.ITimeTrackerEntryStore, AdGoTimeTracker.MemoryStore.InMemoryTimeTrackerEntryStore>();
//builder.Services.AddLocalDbTimeTrackerEntryStore("AdGo");
builder.Services.AddSqlServerTimeTrackerEntryStore("Server=localhost;Database=AdGo;User Id=sa;Password=password123!;Encrypt=False;TrustServerCertificate=True");
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
