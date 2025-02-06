using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyMicroservices.Shared.Extensions
{
    public static class VersioningExt
    {
        public static IServiceCollection AddVersionServiceExt(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
                //options.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader(),
                //    new QueryStringApiVersionReader(), new UrlSegmentApiVersionReader());
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });
            return services;
        }

        public static ApiVersionSet AddVersionSetExt(this WebApplication app)
        {
            //Create a new version set - This is the place where we can define the versions, 1.3 wont work for example
            var apiVersionSet = app.NewApiVersionSet()
                .HasApiVersion(new ApiVersion(1, 0)) //Default Version can be just typed v1 or v1.0 in the URL
                .HasApiVersion(new ApiVersion(1, 2)) //New Version can be used by the client
                .ReportApiVersions()
                .Build();
            return apiVersionSet;
        }
    }
}
