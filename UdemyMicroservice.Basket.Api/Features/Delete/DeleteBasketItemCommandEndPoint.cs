using MediatR;
using UdemyMicroservices.Shared.Extensions;
using UdemyMicroservices.Shared.ProduceTypes;

namespace UdemyMicroservice.Basket.Api.Features.Delete
{
    public static class DeleteBasketItemCommandEndPoint
    {
        public static RouteGroupBuilder DeleteBasketItemGroupItemEndpoint(this RouteGroupBuilder groupBuilder)
        {
          groupBuilder.MapDelete("/item/{id:guid}", async(Guid id, IMediator mediator)=> 
          (await mediator.Send(new DeleteBasketItemCommand(id))).ToGenericResult())
                .Produces(StatusCodes.Status204NoContent)
                .Produces<NotFoundType>(StatusCodes.Status404NotFound)
                .MapToApiVersion(1, 0);
            return groupBuilder;
        }
    }
}
