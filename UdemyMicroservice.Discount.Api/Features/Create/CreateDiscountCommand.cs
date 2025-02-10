namespace UdemyMicroservice.Discount.Api.Features.Create
{
    public sealed record CreateDiscountCommand(string DiscountCode,
        float DiscountRate,
        Guid UserId,
        DateTime Expired) : IRequestByServiceResult<Guid>;
}
