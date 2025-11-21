namespace BookMakerPredictionsFE.Models
{
    public class FixtureWithPrediction
    {
        public AgentEvaluation? AgentEvaluation { get; set; }
        public Fixture? Fixture { get; set; }
        public FixtureResult? FixtureResult { get; set; }
        public Team? HomeTeam { get; set; }
        public Team? AwayTeam { get; set; }
    }
}
