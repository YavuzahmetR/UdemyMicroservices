using UdemyMicroservice.Discount.Api.Features.GetAll;

namespace UdemyMicroservice.Discount.Api.Features
{
    public sealed class DiscountMapper : Profile
    {
        public DiscountMapper()
        {
            CreateMap<Discount, GetAllDiscountsQueryResponse>().ReverseMap();
        }
    }
}
