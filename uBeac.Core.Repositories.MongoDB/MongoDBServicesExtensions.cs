using Microsoft.Extensions.Configuration;
using uBeac.Core.Repositories.MongoDB;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MongoDBServicesExtensions
    {
        public static IServiceCollection AddMongo<TMongoDbContext>(this IServiceCollection services, string connectionString)
           where TMongoDbContext : class, IMongoDBContext
        {            
            services.AddSingleton(provider =>
            {
                var configuration = provider.GetService<IConfiguration>();
                return new MongoDBOptions<MainDBContext>(configuration.GetConnectionString(connectionString));
            });

            services.AddSingleton<TMongoDbContext>();

            return services;

        }
    }
}
