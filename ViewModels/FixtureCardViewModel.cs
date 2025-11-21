namespace BookMakerPredictionsFE.ViewModels
{
    public class FixtureCardViewModel
    {
        public int FixtureId { get; set; }
        public DateTime Date { get; set; }

        public int HomeTeamId { get; set; }
        public string HomeName { get; set; } = default!;
        public string? HomeLogo { get; set; }

        public int AwayTeamId { get; set; }
        public string AwayName { get; set; } = default!;
        public string? AwayLogo { get; set; }

        public int? HomeGoals { get; set; }
        public int? AwayGoals { get; set; }

        public string? ResultRaw { get; set; }
    }
}
