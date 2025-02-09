using AutoMapper;
using UdemyMicroservice.Basket.Api.Dto;
using UdemyMicroservice.Basket.Api.Data;

namespace UdemyMicroservice.Basket.Api
{
    public sealed class BasketMapper : Profile
    {
        public BasketMapper()
        {
            CreateMap<Data.Basket, BasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
        }
    }
}
