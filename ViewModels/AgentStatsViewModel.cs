namespace BookMakerPredictionsFE.ViewModels
{
    public class AgentStatsViewModel
    {
        public int LeagueId { get; set; }
        public string LeagueName { get; set; } = string.Empty;
        public string LeagueCountry { get; set; } = string.Empty;

        public decimal GoodPredictionsPercentage { get; set; }
    }
}
