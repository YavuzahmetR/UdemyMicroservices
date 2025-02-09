using MediatR;
using UdemyMicroservices.Shared;

namespace UdemyMicroservice.Basket.Api.Features.ApplyDiscount
{
    public sealed record ApplyDiscountCodeCommand(string DiscountCode, float DiscountRate) : IRequestByServiceResult;
}
