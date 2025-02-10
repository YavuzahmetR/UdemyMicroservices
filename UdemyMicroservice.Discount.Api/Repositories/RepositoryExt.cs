using MongoDB.Driver;
using UdemyMicroservice.Discount.Api.Options;

namespace UdemyMicroservice.Discount.Api.Repositories
{
    public static class RepositoryExt
    {
        public static IServiceCollection AddDatabaseServiceExt(this IServiceCollection services)
        {
            services.AddSingleton<IMongoClient, MongoClient>(sp =>
            {
                var options = sp.GetRequiredService<MongoOptions>();
                return new MongoClient(options.ConnectionString);
            });
            services.AddScoped<AppDbContext>(sp =>
            {
                var mongoClient = sp.GetRequiredService<IMongoClient>();
                var options = sp.GetRequiredService<MongoOptions>();
                return AppDbContext.Create(mongoClient.GetDatabase(options.DatabaseName));
            });
            return services;
        }
    }
}
