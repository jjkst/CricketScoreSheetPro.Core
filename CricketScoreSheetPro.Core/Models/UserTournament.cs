using System;

namespace CricketScoreSheetPro.Core.Models
{
    public class UserTournament
    {
        public string TournamentId { get; set; }
        public string TournamentName { get; set; }
        public string Status { get; set; }
        public bool ImportedFlg { get; set; }
        public DateTime AddDate { get; set; }
        public UserTournament()
        {
            AddDate = DateTime.Today;
        }
    }
}
