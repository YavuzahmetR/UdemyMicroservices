using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;

namespace UdemyMicroservice.Discount.Api.Repositories
{
    public sealed class DiscountConfigurations : IEntityTypeConfiguration<UdemyMicroservice.Discount.Api.Features.Discount>
    {
        public void Configure(EntityTypeBuilder<UdemyMicroservice.Discount.Api.Features.Discount> builder)
        {
            builder.ToCollection("discounts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DiscountCode).HasElementName("discount_code").HasMaxLength(10);
            builder.Property(x => x.DiscountRate).HasElementName("discount_rate");
            builder.Property(x => x.UserId).HasElementName("user_id");
            builder.Property(x => x.Created).HasElementName("created");
            builder.Property(x => x.Updated).HasElementName("updated");
            builder.Property(x => x.Expired).HasElementName("expired");
        }
    }
}
