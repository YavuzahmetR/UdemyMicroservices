using MediatR;
using System.Net;
using UdemyMicroservices.Shared.Extensions;
using UdemyMicroservices.Shared.Filters;

namespace UdemyMicroservice.Basket.Api.Features
{
    public static class AddBasketItemCommandEndPoint
    {
        public static RouteGroupBuilder AddBasketItemGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/item",
                    async (AddBasketItemCommand command, IMediator mediator) =>
                        (await mediator.Send(command)).ToGenericResult())
                .MapToApiVersion(1, 0)
                .Produces(StatusCodes.Status201Created)
                .AddEndpointFilter<ValidationFilter<AddBasketItemCommandValidator>>();
            return group;
        }
    }
}
