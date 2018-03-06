using System;
using System.Collections.Generic;
using Firebase.Database;

namespace CricketScoreSheetPro.Core.Models
{
    public class Match
    {
        public string Id { get; set; }

        public string TournamentId { get; set; }

        public DateTime AddDate { get; set; }

        public int TotalOvers { get; set; }

        public string Location { get; set; }

        public bool MatchComplete { get; set; }

        public TeamInning HomeTeam { get; set; }

        public TeamInning AwayTeam { get; set; }

        public string WinningTeamName { get; set; }

        public string Comments { get; set; }

        public IList<string> Umpires { get; set; }

        public bool ImportedFlg { get; set; }

        public Match()
        {
            AddDate = DateTime.Today;
            Umpires = new List<string>();
        }

    }
}
