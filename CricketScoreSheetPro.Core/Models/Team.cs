namespace CricketScoreSheetPro.Core.Models
{
    public class Team
    {
        public string Id { get; set; }

        public string TournamentId { get; set; }

        public string MatchId { get; set; }

        public string Name { get; set; }      
        
        public int HighestScore { get; set; }

        public int LowestScore { get; set; }

        public int Wins { get; set; }

        public int Loss { get; set; }

        public int Tie { get; set; }

        public int NoOfMatchesPlayed { get; set; }
    }
}
