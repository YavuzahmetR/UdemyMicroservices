using Asp.Versioning.Builder;
using UdemyMicroservice.Discount.Api.Features.Create;
using UdemyMicroservice.Discount.Api.Features.GetAll;

namespace UdemyMicroservice.Discount.Api.Features
{
    public static class DiscountEndpointExt
    {
        public static void AddDiscountGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/discounts").WithTags("discounts").WithApiVersionSet(apiVersionSet)
                .CreateDiscountGroupItemEndpoint()
                .GetDiscountByCodeGroupItemEndpoint();
        }
    }
}
