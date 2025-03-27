using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Play.Catalog.Service.Repository;
using Play.Catalog.Service.Settings;

namespace Play.Catalog.Service.Extensions
{
    public static class StartupExtensions
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration Configuration)
        {
            var serviceSection = Configuration.GetSection(ServiceSettings.ConfigurationName).Get<ServiceSettings>();

            services.AddSingleton(serviceProvider =>{
                var mongoDbSettings = Configuration.GetSection(MongoDbSettings.ConfigurationName).Get<MongoDbSettings>();
                var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);

                return mongoClient.GetDatabase(serviceSection.ServiceName);

            });

            services.AddTransient<IItemsRepository, ItemsRepository>();
        }
    }
}