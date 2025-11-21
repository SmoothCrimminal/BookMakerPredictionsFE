namespace BookMakerPredictionsFE.Models
{
    public class AgentEvaluation
    {
        public int FixtureId { get; set; }
        public string AgentModel { get; set; } = string.Empty;
        public string Explanation { get; set; } = string.Empty;
        public string PickedBet { get; set; } = string.Empty;
        public decimal Odds { get; set; }
        public decimal EV { get; set; }
        public decimal Kelly { get; set; }
        public decimal Probability { get; set; }
        public decimal? RecommendedKelly { get; set; }
        public decimal? Confidence { get; set; }
        public bool? CorrectEvaluation { get; set; }
    }
}
