using BookMakerPredictionsFE.Models;
using System.Net.Http.Json;

namespace BookMakerPredictionsFE.Services
{
    public class FixtureService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FixtureService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<FixtureWithPrediction>> GetFixturesWithPredictionsAsync()
        {
            var client = _httpClientFactory.CreateClient(nameof(FixtureService));

            return await client.GetFromJsonAsync<IEnumerable<FixtureWithPrediction>>("/api/Fixtures") ?? Enumerable.Empty<FixtureWithPrediction>();
        }
    }
}
