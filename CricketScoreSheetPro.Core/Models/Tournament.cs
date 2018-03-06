using System;

namespace CricketScoreSheetPro.Core.Models
{
    public class Tournament
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public bool ImportedFlg { get; set; }
        public DateTime AddDate { get; set; }
        public Tournament()
        {
            AddDate = DateTime.Today;
        }
    }
}
