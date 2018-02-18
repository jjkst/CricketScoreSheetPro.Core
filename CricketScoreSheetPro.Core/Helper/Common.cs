using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Helper
{
    public class Common
    {
        public static string ConvertBallstoOvers(int balls)
        {
            return balls/6 + "." + balls%6;
        }
    }
}
