using E2E.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace E2E.Queries
{
    public static class CoreExtensions
    {
        public static IServiceCollection AddQueries(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddTransient<ConferenceQueries>();
            serviceCollection.AddPooledDbContextFactory<E2EContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            return serviceCollection;
        }
    }
}