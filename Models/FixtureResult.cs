namespace BookMakerPredictionsFE.Models
{
    public class FixtureResult
    {
        public int FixtureId { get; set; }
        public int? HomeGoals { get; set; }
        public int? AwayGoals { get; set; }
        public required string Result { get; set; }
    }
}
