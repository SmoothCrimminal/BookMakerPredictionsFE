namespace BookMakerPredictionsFE.ViewModels
{
    public class FixturePredictionsViewModel
    {
        public int FixtureId { get; set; }
        public string Explanation { get; set; } = string.Empty;
        public string PickedBet { get; set; } = string.Empty;
        public decimal Odds { get; set; }
        public decimal EV { get; set; }
        public decimal Kelly { get; set; }
        public decimal Probability { get; set; }
        public decimal? RecommendedKelly { get; set; }
        public decimal? Confidence { get; set; }
        public int? HomeGoals { get; set; }
        public int? AwayGoals { get; set; }
        public string? Result { get; set; }
        public bool? CorrectEvaluation { get; set; }
    }
}
