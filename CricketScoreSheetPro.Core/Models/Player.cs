using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Models
{
    public class Player
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IList<string> Roles { get; set; }
    }

}
