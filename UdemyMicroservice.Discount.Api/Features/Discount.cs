using UdemyMicroservice.Discount.Api.Repositories;

namespace UdemyMicroservice.Discount.Api.Features
{
    public sealed class Discount : BaseEntity
    {
        public Guid UserId { get; set; }
        public float DiscountRate { get; set; }
        public string DiscountCode { get; set; } = default!;
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime Expired { get; set; }
    }
}
