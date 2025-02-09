using Asp.Versioning.Builder;
using UdemyMicroservice.Basket.Api.Features;
using UdemyMicroservice.Basket.Api.Features.ApplyDiscount;
using UdemyMicroservice.Basket.Api.Features.Delete;
using UdemyMicroservice.Basket.Api.Features.GetAll;
using UdemyMicroservice.Basket.Api.Features.RemoveDiscount;

namespace UdemyMicroservice.Basket.Api
{
    public static class BasketEndpointExt
    {
        public static void AddBasketGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/baskets").WithTags("Baskets")
                .WithApiVersionSet(apiVersionSet)
                .AddBasketItemGroupItemEndpoint()
                .DeleteBasketItemGroupItemEndpoint()
                .GetBasketsGroupItemEndpoint()
                .ApplyDiscountCommandGroupItemEndpoint()
                .RemoveDiscountGroupItemEndpoint();
        }
    }
}
