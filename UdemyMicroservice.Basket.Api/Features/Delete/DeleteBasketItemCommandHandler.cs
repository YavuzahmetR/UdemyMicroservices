using MediatR;
using System.Net;
using System.Text.Json;
using UdemyMicroservices.Shared;

namespace UdemyMicroservice.Basket.Api.Features.Delete
{
    public sealed class DeleteBasketItemCommandHandler(BasketService basketService) : IRequestHandler<DeleteBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {
            var basketAsJson = await basketService.GetBasketFromCacheAsync(cancellationToken);

            if(string.IsNullOrEmpty(basketAsJson))
            {
                return ServiceResult.Error("Basket not found", "Specified Basket Did Not Occure On Database", HttpStatusCode.NotFound);
            }

            var currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson);

            var basketItemToDelete =  currentBasket!.Items.FirstOrDefault(x => x.Id == request.Id);

            if (basketItemToDelete is null)
            {
                return ServiceResult.Error("Basket Item not found", "Specified Basket Item Did Not Occure On Database", HttpStatusCode.NotFound);
            }

            currentBasket.Items.Remove(basketItemToDelete);

            await basketService.CreateBasketCacheAsync(currentBasket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
