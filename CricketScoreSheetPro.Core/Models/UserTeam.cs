namespace CricketScoreSheetPro.Core.Models
{
    public class UserTeam
    {
        public string TeamId { get; set; }
        public string TournamentId { get; set; }
        public string TeamName { get; set; }
        public bool ImportedFlg { get; set; }
    }
}
