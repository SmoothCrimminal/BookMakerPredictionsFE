using BookMakerPredictionsFE.Constants;
using BookMakerPredictionsFE.Extensions;
using BookMakerPredictionsFE.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Net.Http.Json;

namespace BookMakerPredictionsFE.Services
{
    public class AgentStatsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IDistributedCache _cache;

        public AgentStatsService(IHttpClientFactory httpClientFactory, IDistributedCache cache)
        {
            _httpClientFactory = httpClientFactory;
            _cache = cache;
        }

        public async Task<AgentStats?> GetAgentStatsForLeague(int? leagueId = null)
        {
            var client = _httpClientFactory.CreateClient(HttpClientConstants.BookMakerApi);

            var cacheKey = $"stats_{leagueId}";

            var url = leagueId is null ? "/api/agentEffectiveness" : $"/api/agentEffectiveness?leagueId={leagueId}";

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);
                return await client.GetFromJsonAsync<AgentStats>(url);
            });
        }
    }
}
