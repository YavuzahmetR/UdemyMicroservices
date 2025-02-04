using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace UdemyMicroservice.Catalog.Api.Options
{
    public static class MongoOptionExt
    {
        public static IServiceCollection AddOptExt(this IServiceCollection services)
        {
            services.AddOptions<MongoOption>().BindConfiguration(nameof(MongoOption)) //Type Safe Binding
                .ValidateDataAnnotations().ValidateOnStart();

            //Bind to MongoOption from IOptions connectionstring and databasename
            services.AddSingleton<MongoOption>(sp => sp.GetRequiredService<IOptions<MongoOption>>().Value); 


            return services;
        }
    }
}
