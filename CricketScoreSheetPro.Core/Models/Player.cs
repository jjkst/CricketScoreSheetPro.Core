using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Models
{
    public class Player
    {
        public string Id { get; set; }

        public string TeamId { get; set; }

        public string MatchId { get; set; }

        public string TournamentId { get; set; }

        public string Name { get; set; }      
        
        public IList<string> Roles { get; set; }

        public int RunsTaken { get; set; }

        public int BallsPlayed { get; set; }

        public int Fours { get; set; }

        public int Sixes { get; set; }

        public string HowOut { get; set; } = "not out";

        public int RunsGiven { get; set; }

        public int BallsBowled { get; set; }

        public int Wickets { get; set; }

        public int Maiden { get; set; }

        public int NoBalls { get; set; }

        public int Wides { get; set; }

        public int Catches { get; set; }

        public int Stumpings { get; set; }

        public int RunOuts { get; set; }

    }
}
