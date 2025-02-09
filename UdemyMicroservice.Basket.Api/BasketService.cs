using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using UdemyMicroservice.Basket.Api.Const;
using UdemyMicroservices.Shared.Services;

namespace UdemyMicroservice.Basket.Api
{
    public sealed class BasketService(IIdentiyService identiyService, IDistributedCache distributedCache)
    {
        private string GetCacheKey() => string.Format(BasketConst.BasketCacheKey, identiyService.UserId);

        public async Task<string?> GetBasketFromCacheAsync(CancellationToken cancellationToken)
        {
            return await distributedCache.GetStringAsync(GetCacheKey(), token: cancellationToken);
        }

        public async Task CreateBasketCacheAsync(Data.Basket basket, CancellationToken cancellationToken)
        {
            var basketAsString = JsonSerializer.Serialize(basket);
            await distributedCache.SetStringAsync(GetCacheKey(), basketAsString, token: cancellationToken);
        }
    }
}
