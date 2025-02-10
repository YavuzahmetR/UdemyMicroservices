﻿using Microsoft.Extensions.Options;

namespace UdemyMicroservice.Discount.Api.Options
{
    public static class OptionsExt
    {
        public static IServiceCollection AddOptionsExt(this IServiceCollection services)
        {
            services.AddOptions<MongoOptions>().BindConfiguration(nameof(MongoOptions)).ValidateDataAnnotations().ValidateOnStart();

            services.AddSingleton(sp => sp.GetRequiredService<IOptions<MongoOptions>>().Value);
            return services;
        }
    }
}
