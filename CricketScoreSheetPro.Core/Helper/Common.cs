using Firebase.Database;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CricketScoreSheetPro.Core.Helper
{
    public class Common
    {
        public static string BallsToOversValueConverter(int balls)
        {
            return balls/6 + "." + balls%6;
        }
        
    }
}
