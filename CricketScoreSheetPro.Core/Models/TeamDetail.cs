using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Models
{
    public class TeamDetail
    {
        public string Name { get; set; }

        public IList<Player> Players { get; set; }

        public TeamDetail()
        {
            Players = new List<Player>();
        }
    }
}
