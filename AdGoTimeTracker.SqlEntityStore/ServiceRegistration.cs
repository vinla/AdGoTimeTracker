using AdGoTimeTracker.SqlEntityStore;
using AdGoTimeTracker.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddLocalDbTimeTrackerEntryStore(this IServiceCollection services, string databaseName)
        {
            services.AddDbContextFactory<ApplicationDbContext>(
                options => options.UseSqlServer(
                    $"Server=(localdb)\\mssqllocaldb;Database={databaseName};ConnectRetryCount=0"));
            services.AddScoped<ITimeTrackerEntryStore, SqlEntityTimeTrackerEntryStore>();
            return services;
        }
    }
}
