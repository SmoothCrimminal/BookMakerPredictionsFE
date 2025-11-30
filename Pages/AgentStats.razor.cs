using BookMakerPredictionsFE.Services;
using BookMakerPredictionsFE.ViewModels;
using Microsoft.AspNetCore.Components;

namespace BookMakerPredictionsFE.Pages
{
    public partial class AgentStats
    {
        [Inject] LeagueService LeagueService { get; set; } = null!;
        [Inject] AgentStatsService AgentStatsService { get; set; } = null!;

        private IEnumerable<AgentStatsViewModel> _agentStatsVM = [];

        protected override async Task OnInitializedAsync()
        {
            var leagues = await LeagueService.GetLeaguesAsync();
            var agnetStats = new List<AgentStatsViewModel>();

            foreach (var league in leagues)
            {
                var stats = await AgentStatsService.GetAgentStatsForLeague(league.Id);
                if (stats is null)
                    continue;

                agnetStats.Add(new AgentStatsViewModel
                {
                    GoodPredictionsPercentage = stats.GoodPredictionsPercentage,
                    LeagueCountry = league.Country,
                    LeagueId = league.Id,
                    LeagueName = league.Name
                });
            }

            var generalStats = await AgentStatsService.GetAgentStatsForLeague();
            if (generalStats is null)
            {
                _agentStatsVM = agnetStats;
                return;
            }

            agnetStats.Add(new AgentStatsViewModel
            {
                GoodPredictionsPercentage = generalStats.GoodPredictionsPercentage,
                LeagueId = -1,
                LeagueCountry = "Global",
                LeagueName = "All Leagues"
            });

            _agentStatsVM = agnetStats.Where(ags => ags.GoodPredictionsPercentage > 0);
        }
    }
}
