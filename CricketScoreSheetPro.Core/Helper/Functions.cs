using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CricketScoreSheetPro.Core.Helper
{
    public class Functions
    {
        public static string BallsToOversValueConverter(int balls)
        {
            if(balls < 0) throw new ArgumentException("Balls cannot be negative.");
            return balls/6 + "." + balls%6;
        }
        
    }
}
