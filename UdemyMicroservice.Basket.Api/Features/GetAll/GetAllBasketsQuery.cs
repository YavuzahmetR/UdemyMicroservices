using UdemyMicroservice.Basket.Api.Dto;
using UdemyMicroservices.Shared;

namespace UdemyMicroservice.Basket.Api.Features.GetAll
{
    public sealed record GetAllBasketsQuery : IRequestByServiceResult<BasketDto>;

}
