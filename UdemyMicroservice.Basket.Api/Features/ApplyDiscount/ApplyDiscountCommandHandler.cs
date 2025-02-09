using MediatR;
using System.Text.Json;
using UdemyMicroservice.Basket.Api.Data;
using UdemyMicroservices.Shared;
using UdemyMicroservices.Shared.Services;

namespace UdemyMicroservice.Basket.Api.Features.ApplyDiscount
{
    public sealed class ApplyDiscountCommandHandler(
        BasketService basketService) : IRequestHandler<ApplyDiscountCodeCommand, ServiceResult>
    {

        public async Task<ServiceResult> Handle(ApplyDiscountCodeCommand request, CancellationToken cancellationToken)
        {
            
            var basketASJson = await basketService.GetBasketFromCacheAsync(cancellationToken);

            if (string.IsNullOrEmpty(basketASJson))
            {
                return ServiceResult.ErrorAsNotFound();
            }

            var basket = JsonSerializer.Deserialize<Data.Basket>(basketASJson);

            if (!(basket!.Items.Any()))
            {
                return ServiceResult.Error("Basket is empty","Basket does not contain any item",System.Net.HttpStatusCode.NotFound);
            }

            foreach(var item in basket.Items) // Can't use discount code if discount already applied
            {
                if (item.PriceByApplyDiscountRate.HasValue)
                {
                    return ServiceResult.Error("Discount already applied", "Discount already applied to the basket", System.Net.HttpStatusCode.BadRequest);
                }
            }

            basket.ApplyNewDiscount(request.DiscountCode, request.DiscountRate);

            await basketService.CreateBasketCacheAsync(basket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
