using FluentValidation;
using MediatR;
using System.Text.Json;
using UdemyMicroservices.Shared;
using UdemyMicroservices.Shared.Extensions;
using UdemyMicroservices.Shared.Filters;
using UdemyMicroservices.Shared.ProduceTypes;

namespace UdemyMicroservice.Basket.Api.Features.RemoveDiscount
{
    public sealed record RemoveDiscountCommand : IRequestByServiceResult;
    public sealed class RemoveDiscountCommandEndPoint(
        BasketService basketService) : IRequestHandler<RemoveDiscountCommand, ServiceResult>
    {

        public async Task<ServiceResult> Handle(RemoveDiscountCommand request, CancellationToken cancellationToken)
        {
            var basketASJson = await basketService.GetBasketFromCacheAsync(cancellationToken);

            if (string.IsNullOrEmpty(basketASJson))
            {
                return ServiceResult.ErrorAsNotFound();
            }

            var basket = JsonSerializer.Deserialize<Data.Basket>(basketASJson);

            basket!.RemoveDiscount();

            await basketService.CreateBasketCacheAsync(basket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();

        }


    }
    public static class ApplyDiscountCommandEndPoint
    {
        public static RouteGroupBuilder RemoveDiscountGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/remove-discount-coupon",
                    async (IMediator mediator) =>
                        (await mediator.Send(new RemoveDiscountCommand())).ToGenericResult())
                .MapToApiVersion(1, 0)
                .Produces(StatusCodes.Status204NoContent)
                .Produces<NotFoundType>(StatusCodes.Status404NotFound);
            return group;
        }
    }

}
