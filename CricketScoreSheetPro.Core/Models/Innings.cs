using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Models
{
    public class Innings
    {
        public string TeamId { get; set; }

        public string TeamName { get; set; }

        public int Runs { get; set; }

        public int Wickets { get; set; }

        public int Balls { get; set; }

        public int NoBalls { get; set; }

        public int Wides { get; set; }

        public int Byes { get; set; }

        public int LegByes { get; set; }

        public bool InningStatus { get; set; }
    }
}
