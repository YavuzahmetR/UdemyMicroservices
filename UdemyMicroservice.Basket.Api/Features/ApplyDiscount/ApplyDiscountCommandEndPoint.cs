using MediatR;
using System.Net;
using UdemyMicroservices.Shared.Extensions;
using UdemyMicroservices.Shared.Filters;

namespace UdemyMicroservice.Basket.Api.Features.ApplyDiscount
{
    public static class ApplyDiscountCommandEndPoint
    {
        public static RouteGroupBuilder ApplyDiscountCommandGroupItemEndpoint(this RouteGroupBuilder groupBuilder)
        {
            groupBuilder.MapPut("/apply-discount-coupon",
                     async (ApplyDiscountCodeCommand command, IMediator mediator) =>
                         (await mediator.Send(command)).ToGenericResult())
                 .MapToApiVersion(1, 0)
                 .Produces(StatusCodes.Status204NoContent)
                 .AddEndpointFilter<ValidationFilter<ApplyDiscountCodeCommandValidator>>();
            return groupBuilder;
        }
    }
}
