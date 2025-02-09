using AutoMapper;
using MediatR;
using System.Net;
using System.Text.Json;
using UdemyMicroservice.Basket.Api.Dto;
using UdemyMicroservices.Shared;

namespace UdemyMicroservice.Basket.Api.Features.GetAll
{
    public sealed class GetAllBasketsHandler(BasketService basketService, IMapper mapper) : IRequestHandler<GetAllBasketsQuery, ServiceResult<BasketDto>>
    {
        public async Task<ServiceResult<BasketDto>> Handle(GetAllBasketsQuery request, CancellationToken cancellationToken)
        {
            var basketAsString = await basketService.GetBasketFromCacheAsync(cancellationToken);

            if(string.IsNullOrEmpty(basketAsString))
            {
                return ServiceResult<BasketDto>.Error("Basket not found", 
                    "Specified Basket Did Not Occure On Database", HttpStatusCode.NotFound);
            }

            var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsString);

            var basketDto = mapper.Map<BasketDto>(basket);

            return ServiceResult<BasketDto>.SuccessAsOk(basketDto);
        }
    }
}
