using BookMakerPredictionsFE.Extensions;
using BookMakerPredictionsFE.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Net.Http.Json;

namespace BookMakerPredictionsFE.Services
{
    public class FixtureService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IDistributedCache _cache;

        public FixtureService(IHttpClientFactory httpClientFactory, IDistributedCache cache)
        {
            _httpClientFactory = httpClientFactory;
            _cache = cache;
        }

        public async Task<IEnumerable<FixtureWithPrediction>> GetFixturesWithPredictionsAsync()
        {
            var client = _httpClientFactory.CreateClient(nameof(FixtureService));
            var fixturesAfter = DateTime.Now.AddDays(-7);

            var cacheKey = $"fixtures_{fixturesAfter.ToString("dd.MM.yyyy")}";

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                return await client.GetFromJsonAsync<IEnumerable<FixtureWithPrediction>>($"/api/Fixtures?fixturesAfter={fixturesAfter:yyyy-MM-dd}");
            }) ?? Enumerable.Empty<FixtureWithPrediction>();
        }
    }
}
