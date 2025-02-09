using System.Text.Json.Serialization;

namespace UdemyMicroservice.Basket.Api.Data
{
    public sealed class Basket
    {
        public Guid UserId { get; set; }

        public List<BasketItem> Items { get; set; } = new();

        public float? DiscountRate { get; set; }

        public string? DiscountCode { get; set; }
        public decimal TotalPrice => Items.Sum(x => x.Price);

        [JsonIgnore]
        public bool IsApplyDiscount =>
            DiscountRate is > 0 && !string.IsNullOrEmpty(DiscountCode);

        [JsonIgnore]
        public decimal? TotalPriceWithAppliedDiscount =>
            !IsApplyDiscount ? null : Items.Sum(x => x.PriceByApplyDiscountRate);

        public Basket(Guid userId, List<BasketItem> items)
        {
            UserId = userId;
            Items = items;
        }

        public void ApplyNewDiscount(string discountCode, float discountRate)
        {
            DiscountCode = discountCode;
            DiscountRate = discountRate;


            foreach (var basket in Items)
            {
                basket.PriceByApplyDiscountRate = basket.Price * (decimal)(1 - discountRate);
            }
        }
        public void ApplyAvailableDiscount()
        {
            if (!IsApplyDiscount)
            {
                return;
            }

            foreach (var basket in Items)
            {
                basket.PriceByApplyDiscountRate = basket.Price * (decimal)(1 - DiscountRate!);
            }
        }

        public void RemoveDiscount()
        {
            if (!IsApplyDiscount)
            {
                return;
            }

            DiscountRate = null;
            DiscountCode = null;

            foreach (var item in Items)
            {
                item.PriceByApplyDiscountRate = null;
            }
        }
    }
}
