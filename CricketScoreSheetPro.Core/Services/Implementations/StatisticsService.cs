using CricketScoreSheetPro.Core.Helper;
using CricketScoreSheetPro.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Services.Implementations
{
    public class StatisticsService
    {
        public PlayerStatistics GetPlayerStatistics(IList<PlayerInning> players)
        {
            var playersStatistics = new PlayerStatistics
            {
                PlayerName = players.First().PlayerName,
                TeamName = players.First().TeamId                
            };

            playersStatistics.Matches = players.Count;
            playersStatistics.Innings = players.Count(p => p.BallsPlayed > 0 || (p.HowOut != null && p.HowOut.ToLower() != "not out"));
            playersStatistics.NotOuts = players.Count(p => p.BallsPlayed > 0 && (p.HowOut == null || p.HowOut.ToLower() == "not out"));
            playersStatistics.Runs = players.Sum(r => r.RunsTaken);
            playersStatistics.HS = players.Max(r => r.RunsTaken);
            playersStatistics.Balls = players.Sum(r => r.BallsPlayed);
            playersStatistics.Hundreds = players.Count(p => p.RunsTaken >= 100);
            playersStatistics.Fifties = players.Count(p => p.RunsTaken >= 50 && p.RunsTaken < 100);

            var noOfOuts = (playersStatistics.Matches - playersStatistics.NotOuts) == 0 ? 1 : (playersStatistics.Matches - playersStatistics.NotOuts);
            playersStatistics.BattingAvg = Math.Round((decimal)playersStatistics.Runs / noOfOuts, 2, MidpointRounding.AwayFromZero);
            playersStatistics.SR = (playersStatistics.Balls == 0) ? 0 : Math.Round((decimal)playersStatistics.Runs * 100 / playersStatistics.Balls, 2, MidpointRounding.AwayFromZero);

            playersStatistics.BallsBowled = players.Sum(p => p.BallsBowled);
            var overs = Functions.BallsToOversValueConverter(playersStatistics.BallsBowled);

            playersStatistics.Maiden = players.Sum(p => p.Maiden);
            playersStatistics.RunsGiven = players.Sum(p => p.RunsGiven);
            playersStatistics.Wickets = players.Sum(p => p.Wickets);
            var playerBb = players.Where(s => s.Wickets == players.Max(w => w.Wickets)).OrderBy(m => m.RunsGiven).First();
            playersStatistics.BB = $"{playerBb.Wickets}/{playerBb.RunsGiven}";
            playersStatistics.FWI = players.Count(p => p.Wickets > 4);
            playersStatistics.TWI = players.Count(p => p.Wickets > 9);
            playersStatistics.BowlingAvg = (playersStatistics.Wickets == 0) ? 0 : Math.Round((decimal)playersStatistics.RunsGiven / playersStatistics.Wickets, 2, MidpointRounding.AwayFromZero);
            playersStatistics.Econ = (playersStatistics.BallsBowled == 0) ? 0 : Math.Round((decimal)playersStatistics.RunsGiven / Convert.ToDecimal(overs), 2, MidpointRounding.AwayFromZero);
            playersStatistics.BowlingSR = (playersStatistics.Wickets == 0) ? 0 : Math.Round((decimal)playersStatistics.BallsBowled / playersStatistics.Wickets, 2, MidpointRounding.AwayFromZero);

            playersStatistics.Catches = players.Sum(p => p.Catches);
            playersStatistics.Stumpings = players.Sum(p => p.Stumpings);

            return playersStatistics;
        }

        public TeamStatistics GetTeamStatistics(IList<TeamDetail> teams)
        {
            var teamsStatistics = new TeamStatistics();
            return teamsStatistics;
        }
    }
}
