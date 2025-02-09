using MediatR;
using UdemyMicroservices.Shared.Extensions;

namespace UdemyMicroservice.Basket.Api.Features.GetAll
{
    public static class GetAllBasketsEndPoint
    {
        public static RouteGroupBuilder GetBasketsGroupItemEndpoint(this RouteGroupBuilder groupBuilder)
        {
            groupBuilder.MapGet("/user",
                    async (IMediator mediator) =>
                        (await mediator.Send(new GetAllBasketsQuery())).ToGenericResult())
                .Produces<GetAllBasketsQuery>(StatusCodes.Status200OK)
                .MapToApiVersion(1, 0);
            return groupBuilder;
        }
    }
}
