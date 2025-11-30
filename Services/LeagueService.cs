using BookMakerPredictionsFE.Constants;
using BookMakerPredictionsFE.Extensions;
using BookMakerPredictionsFE.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Net.Http.Json;

namespace BookMakerPredictionsFE.Services
{
    public class LeagueService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IDistributedCache _cache;

        public LeagueService(IHttpClientFactory httpClientFactory, IDistributedCache cache)
        {
            _httpClientFactory = httpClientFactory;
            _cache = cache;
        }

        public async Task<IEnumerable<League>> GetLeaguesAsync()
        {
            var client = _httpClientFactory.CreateClient(HttpClientConstants.BookMakerApi);

            var cacheKey = $"leagues";

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7);
                return await client.GetFromJsonAsync<IEnumerable<League>>($"/api/Leagues");
            }) ?? [];
        }
    }
}
