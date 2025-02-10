using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using UdemyMicroservice.Discount.Api.Features.Create;
using UdemyMicroservice.Discount.Api.Repositories;
using UdemyMicroservices.Shared.Filters;

namespace UdemyMicroservice.Discount.Api.Features.GetAll
{
    public sealed record GetDiscountByCodeQuery(string DiscountCode) : IRequestByServiceResult<GetAllDiscountsQueryResponse>;

    public sealed record GetAllDiscountsQueryResponse(string DiscountCode, float DiscountRate);


    public sealed class GetAllDiscountsQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetDiscountByCodeQuery, ServiceResult<GetAllDiscountsQueryResponse>>
    {

        public async Task<ServiceResult<GetAllDiscountsQueryResponse>> Handle(GetDiscountByCodeQuery request, CancellationToken cancellationToken)
        {
            var hasDiscount = await context.Discounts.AsNoTracking().FirstOrDefaultAsync(x => x.DiscountCode == request.DiscountCode, cancellationToken: cancellationToken);

            if (hasDiscount is null)
            {
                return ServiceResult<GetAllDiscountsQueryResponse>.Error("Discount Not Found", "Discount not found", HttpStatusCode.NotFound);
            }

            if (hasDiscount.Expired < DateTime.Now)
            {
                return ServiceResult<GetAllDiscountsQueryResponse>.Error("Discount is expired", $"Discount is expired {hasDiscount.Expired} in that date.", HttpStatusCode.BadRequest);
            }

            var mappedDiscount = mapper.Map<GetAllDiscountsQueryResponse>(hasDiscount);

            return ServiceResult<GetAllDiscountsQueryResponse>.SuccessAsOk(mappedDiscount);
        }
    }

    public static class GetDiscountByCodeEndpoint
    {
        public static RouteGroupBuilder GetDiscountByCodeGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{discountCode:length(10)}",
                     async (string discountCode, IMediator mediator) =>
                         (await mediator.Send(new GetDiscountByCodeQuery(discountCode))).ToGenericResult())
                 .MapToApiVersion(1, 0)
                 .Produces<GetAllDiscountsQueryResponse>(StatusCodes.Status200OK)
                 .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
                 .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);

            return group;
        }
    }

}
