namespace BookMakerPredictionsFE.Models
{
    public class Fixture
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public string? Winner { get; set; }
    }
}
