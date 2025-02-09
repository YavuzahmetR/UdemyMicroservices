namespace UdemyMicroservice.Basket.Api.Dto
{
    public sealed record BasketItemDto(
        Guid Id,
        string Name,
        string? ImageUrl,
        decimal Price,
        decimal? PriceByApplyDiscountRate);
}
