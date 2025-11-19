using AdGoTimeTracker.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var storeType = builder.Configuration.GetValue<string>("STORE_TYPE");
switch(storeType)
{
    case "in-memory":
        builder.Services.AddSingleton<AdGoTimeTracker.Core.Interfaces.ITimeTrackerEntryStore, AdGoTimeTracker.MemoryStore.InMemoryTimeTrackerEntryStore>();
        break;
    case "mssql":
        var connectionString = builder.Configuration.GetValue<string>("CONNECTION_STRING");
        builder.Services.AddSqlServerTimeTrackerEntryStore(connectionString);
        break;
    default:
        throw new InvalidOperationException("Unable to start service, store type has not been configured correctly");
}

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContext, DummyUserContext>();
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
