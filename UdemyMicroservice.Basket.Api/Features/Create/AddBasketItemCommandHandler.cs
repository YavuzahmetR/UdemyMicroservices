using MediatR;
using System.Text.Json;
using UdemyMicroservice.Basket.Api.Data;
using UdemyMicroservices.Shared;
using UdemyMicroservices.Shared.Services;

namespace UdemyMicroservice.Basket.Api.Features
{
    public sealed class AddBasketItemCommandHandler(IIdentiyService identiyService,
        BasketService basketService) : IRequestHandler<AddBasketItemCommand, ServiceResult>
    {

        public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {
            var basketAsJson = await basketService.GetBasketFromCacheAsync(cancellationToken);

            Data.Basket? currentBasket;

            var newBasketItem = new BasketItem(request.CourseId, request.CourseName, request.ImageUrl,
                 request.CoursePrice, null);

            if (string.IsNullOrEmpty(basketAsJson))
            {
                currentBasket = new Data.Basket(identiyService.UserId, [newBasketItem]);
                await basketService.CreateBasketCacheAsync(currentBasket, cancellationToken);
                return ServiceResult.SuccessAsNoContent();
            }

            currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson);

            var existingItem = currentBasket!.Items.FirstOrDefault(x => x.Id == request.CourseId);

            if (existingItem is not null)
            {
                currentBasket.Items.Remove(existingItem);
            }
            currentBasket.Items.Add(newBasketItem);

            currentBasket.ApplyAvailableDiscount();

            await basketService.CreateBasketCacheAsync(currentBasket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();

        }
    }
}
