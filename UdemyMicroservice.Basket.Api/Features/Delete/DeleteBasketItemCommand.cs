using UdemyMicroservices.Shared;

namespace UdemyMicroservice.Basket.Api.Features.Delete
{
    public sealed record DeleteBasketItemCommand(Guid Id) : IRequestByServiceResult;
}
