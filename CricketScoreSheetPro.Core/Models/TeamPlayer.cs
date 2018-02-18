using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Models
{
    public class TeamPlayer
    {
        public string PlayerId { get; set; }

        public string PlayerName { get; set; }

        public IList<string> Roles { get; set; }
    }

}
