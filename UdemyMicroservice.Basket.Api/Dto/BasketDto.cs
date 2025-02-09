using System.Text.Json.Serialization;

namespace UdemyMicroservice.Basket.Api.Dto
{
    public sealed record BasketDto
    {
        [JsonIgnore] public bool IsApplyDiscount => DiscountRate is > 0 && !string.IsNullOrEmpty(DiscountCode);

        public List<BasketItemDto> Items { get; set; } = new();

        public float? DiscountRate { get; set; }
        public string? DiscountCode { get; set; }


        public decimal TotalPrice => Items.Sum(x => x.Price);


        public decimal? TotalPriceWithAppliedDiscount =>
            !IsApplyDiscount ? null : Items.Sum(x => x.PriceByApplyDiscountRate);
    }
}
