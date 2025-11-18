using AdGoTimeTracker.SqlEntityStore;
using AdGoTimeTracker.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddSqlServerTimeTrackerEntryStore(this IServiceCollection services, string connectionString)
        {
            services.AddDbContextFactory<ApplicationDbContext>(
                options => options.UseSqlServer(connectionString));
            services.AddScoped<ITimeTrackerEntryStore, SqlEntityTimeTrackerEntryStore>();
            return services;
        }
    }
}
