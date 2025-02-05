using AutoMapper;
using UdemyMicroservice.Catalog.Api.Features.Categories.GetAll;
using UdemyMicroservice.Catalog.Api.Features.Categories.GetById;

namespace UdemyMicroservice.Catalog.Api.Features.Categories
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<Category, GetAllQueryResponse>().ReverseMap();
            CreateMap<Category, GetByIdQueryResponse>().ReverseMap();
        }
    }
}
