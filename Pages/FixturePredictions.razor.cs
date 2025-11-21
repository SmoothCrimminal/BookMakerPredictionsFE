using BookMakerPredictionsFE.Models;
using BookMakerPredictionsFE.Services;
using BookMakerPredictionsFE.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BookMakerPredictionsFE.Pages
{
    public partial class FixturePredictions
    {
        [Inject] FixtureService FixtureService { get; set; } = null!;
        [Parameter] public int FixtureId { get; set; }

        private IEnumerable<FixturePredictionsViewModel>? _fixturePredictionsViewModels;
        private FixtureCardViewModel? _fixtureCardViewModel;

        protected override async Task OnInitializedAsync()
        {
            var fixturesWithPredictions = await FixtureService.GetFixturesWithPredictionsAsync();
            _fixturePredictionsViewModels = fixturesWithPredictions
                .Where(fp => fp.AgentEvaluation?.FixtureId == FixtureId && fp.Fixture?.Date.Date >= DateTime.Now.AddDays(-7).Date)
                .Select(fp => new FixturePredictionsViewModel
                {
                    FixtureId = FixtureId,
                    AwayGoals = fp.FixtureResult?.AwayGoals,
                    Confidence = fp.AgentEvaluation?.Confidence,
                    EV = fp.AgentEvaluation?.EV ?? 0,
                    Explanation = fp.AgentEvaluation?.Explanation ?? string.Empty,
                    HomeGoals = fp.FixtureResult?.HomeGoals,
                    Kelly = fp.AgentEvaluation?.Kelly ?? 0,
                    Odds = fp.AgentEvaluation?.Odds ?? 0,
                    PickedBet = fp.AgentEvaluation?.PickedBet ?? string.Empty,
                    Probability = fp.AgentEvaluation?.Probability ?? 0,
                    RecommendedKelly = fp.AgentEvaluation?.RecommendedKelly,
                    Result = fp.FixtureResult?.Result ?? string.Empty,
                    CorrectEvaluation = fp.AgentEvaluation?.CorrectEvaluation
                });

            var fixture = fixturesWithPredictions.FirstOrDefault(fp => fp.Fixture?.Id == FixtureId);
            if (fixture is null)
                return;

            _fixtureCardViewModel = new FixtureCardViewModel
            {
                AwayGoals = fixture.FixtureResult?.AwayGoals,
                AwayLogo = fixture.AwayTeam?.Logo,
                AwayName = fixture.AwayTeam?.Name ?? "Unknown",
                AwayTeamId = fixture.AwayTeam?.Id ?? -1,
                Date = fixture.Fixture?.Date ?? DateTime.MinValue,
                FixtureId = FixtureId,
                HomeGoals = fixture.FixtureResult?.HomeGoals,
                HomeLogo = fixture.HomeTeam?.Logo,
                HomeName = fixture.HomeTeam?.Name ?? "Unknown",
                HomeTeamId = fixture.HomeTeam?.Id ?? -1,
                ResultRaw = fixture.FixtureResult?.Result
            };
        }

        private string GetRowStyle(FixturePredictionsViewModel fixturePredictionsViewModel)
        {
            if (!fixturePredictionsViewModel.CorrectEvaluation.HasValue)
                return string.Empty;

            if (fixturePredictionsViewModel.CorrectEvaluation.Value)
                return "background-color: rgba(0, 200, 0, 0.10);";

            return "background-color: rgba(255, 0, 0, 0.12);";
        }

        private string DisplayDecimal(decimal decimalValue)
        {
            return decimalValue.ToString("F1");
        }

        private string GetScoreText(FixtureCardViewModel vm)
        {
            if (!vm.HomeGoals.HasValue || !vm.AwayGoals.HasValue)
                return "- : -";

            return $"{vm.HomeGoals} : {vm.AwayGoals}";
        }

        private string GetTeamClass(FixtureCardViewModel vm, bool isHome)
        {
            if (!vm.HomeGoals.HasValue || !vm.AwayGoals.HasValue)
                return string.Empty;

            if (vm.HomeGoals == vm.AwayGoals)
                return string.Empty;

            var isWinner = isHome ? vm.HomeGoals > vm.AwayGoals : vm.AwayGoals > vm.HomeGoals;
            return isWinner ? "font-weight-bold" : "text-secondary";
        }
    }
}
