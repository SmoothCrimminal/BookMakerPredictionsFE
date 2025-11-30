using BookMakerPredictionsFE.Models;
using BookMakerPredictionsFE.Services;
using BookMakerPredictionsFE.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BookMakerPredictionsFE.Pages
{
    public partial class Home
    {
        [Inject] FixtureService FixtureService { get; set; } = null!;
        [Inject] NavigationManager NavigationManager { get; set; } = null!;

        [Parameter]
        [SupplyParameterFromQuery]
        public int Page { get; set; }

        [Parameter]
        [SupplyParameterFromQuery]
        public int PageSize { get; set; }

        private int TotalPages => _fixtureCardViewModels is null || _fixtureCardViewModels.Count() == 0
          ? 1
          : (int)Math.Ceiling(_fixtureCardViewModels.Count() / (double)PageSize);
        private IEnumerable<FixtureCardViewModel> PagedFixtures => _fixtureCardViewModels is null
           ? Enumerable.Empty<FixtureCardViewModel>()
           : _fixtureCardViewModels
               .Skip((Page - 1) * PageSize)
               .Take(PageSize);

        private IEnumerable<FixtureCardViewModel>? _fixtureCardViewModels;

        protected override async Task OnInitializedAsync()
        {
            await InitializeView();
        }

        private async Task InitializeView()
        {
            var fixtureWithPredictions = await FixtureService.GetFixturesWithPredictionsAsync();

            _fixtureCardViewModels = fixtureWithPredictions
                .DistinctBy(f => f.Fixture?.Id)
                .OrderByDescending(f => f.Fixture?.Date)
                .Select(f => new FixtureCardViewModel
            {
                AwayGoals = f.FixtureResult?.AwayGoals,
                HomeGoals = f.FixtureResult?.HomeGoals,
                AwayLogo = f.AwayTeam?.Logo,
                AwayName = GetTeamName(f.AwayTeam?.Name),
                AwayTeamId = f.AwayTeam?.Id ?? 0,
                Date = f.Fixture?.Date ?? DateTime.MinValue,
                FixtureId = f.Fixture?.Id ?? 0,
                HomeLogo = f.HomeTeam?.Logo,
                HomeName = GetTeamName(f.HomeTeam?.Name),
                HomeTeamId = f.HomeTeam?.Id ?? 0,
                ResultRaw = f.FixtureResult?.Result
            });

            if (Page == 0 && PageSize == 0)
            {
                Page = 1;
                PageSize = 9;
            }
        }

        private void OnPageChanged(int page)
        {
            NavigationManager.NavigateTo($"/?page={page}&pageSize={PageSize}");
        }

        private string GetTeamName(string? teamName)
        {
            if (string.IsNullOrWhiteSpace(teamName))
                return string.Empty;

            if (teamName.Length > 20)
                return $"{teamName[..20]}...";

            return teamName;
        }

        private string GetScoreText(FixtureCardViewModel vm)
        {
            if (!vm.HomeGoals.HasValue || !vm.AwayGoals.HasValue)
                return "- : -";

            return $"{vm.HomeGoals} : {vm.AwayGoals}";
        }

        private string GetStatusText(FixtureCardViewModel vm)
        {
            if (!vm.HomeGoals.HasValue || !vm.AwayGoals.HasValue)
                return "NS"; // brak wyniku

            if (vm.HomeGoals > vm.AwayGoals) return "FT (Home)";
            if (vm.HomeGoals < vm.AwayGoals) return "FT (Away)";
            return "FT (Draw)";
        }

        private Color GetStatusColor(FixtureCardViewModel vm)
        {
            if (!vm.HomeGoals.HasValue || !vm.AwayGoals.HasValue)
                return Color.Info;

            if (vm.HomeGoals > vm.AwayGoals) return Color.Success;
            if (vm.HomeGoals < vm.AwayGoals) return Color.Error;
            return Color.Warning;
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

        private void NavigateToPredictions(int fixtureId)
        {
            NavigationManager.NavigateTo($"/predictions/{fixtureId}");
        }
    }
}
