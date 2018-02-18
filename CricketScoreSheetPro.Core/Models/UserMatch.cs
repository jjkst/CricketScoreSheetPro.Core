using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Models
{
    public class UserMatch
    {
        public string Id { get; set; }

        public string TournamentId { get; set; }

        public DateTime AddDate { get; set; }

        public int TotalOvers { get; set; }

        public string Location { get; set; }

        public bool MatchComplete { get; set; }

        public Innings HomeTeam { get; set; }

        public Innings AwayTeam { get; set; }

        public string WinningTeamName { get; set; }

        public string Comments { get; set; }

        public IList<string> Umpires { get; set; }

        public UserMatch()
        {
            AddDate = DateTime.Today;
            Umpires = new List<string>();
        }

    }
}
