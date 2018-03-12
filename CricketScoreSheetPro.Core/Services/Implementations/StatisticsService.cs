using CricketScoreSheetPro.Core.Helper;
using CricketScoreSheetPro.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CricketScoreSheetPro.Core.Services.Implementations
{
    public class StatisticsService
    {
        public PlayerStatistics GetPlayerStatistics(IList<PlayerInning> playersinnings)
        {
            if(playersinnings == null) throw new ArgumentNullException($"Null player inning passed to calculate statistics.");
            if(playersinnings.Count <= 0) throw new ArgumentNullException($"player innings list is empty");

            var playersStatistics = new PlayerStatistics
            {   
                PlayerName = playersinnings.FirstOrDefault().PlayerName,
                TeamName = playersinnings.FirstOrDefault().TeamId                
            };

            playersStatistics.Matches = playersinnings.Count;
            playersStatistics.Innings = playersinnings.Count(p => p.BallsPlayed > 0 || (p.HowOut != null && p.HowOut.ToLower() != "not out"));
            playersStatistics.NotOuts = playersinnings.Count(p => p.BallsPlayed > 0 && (p.HowOut == null || p.HowOut.ToLower() == "not out"));
            playersStatistics.Runs = playersinnings.Sum(r => r.RunsTaken);
            playersStatistics.HS = playersinnings.Max(r => r.RunsTaken);
            playersStatistics.Balls = playersinnings.Sum(r => r.BallsPlayed);
            playersStatistics.Hundreds = playersinnings.Count(p => p.RunsTaken >= 100);
            playersStatistics.Fifties = playersinnings.Count(p => p.RunsTaken >= 50 && p.RunsTaken < 100);

            var noOfOuts = (playersStatistics.Innings - playersStatistics.NotOuts) == 0 ? 1 : (playersStatistics.Innings - playersStatistics.NotOuts);
            playersStatistics.BattingAvg = Math.Round((decimal)playersStatistics.Runs / noOfOuts, 2, MidpointRounding.AwayFromZero);
            playersStatistics.SR = (playersStatistics.Balls == 0) ? 0 : Math.Round((decimal)playersStatistics.Runs * 100 / playersStatistics.Balls, 2, MidpointRounding.AwayFromZero);

            playersStatistics.BallsBowled = playersinnings.Sum(p => p.BallsBowled);
            var overs = Functions.BallsToOversValueConverter(playersStatistics.BallsBowled);

            playersStatistics.Maiden = playersinnings.Sum(p => p.Maiden);
            playersStatistics.RunsGiven = playersinnings.Sum(p => p.RunsGiven);
            playersStatistics.Wickets = playersinnings.Sum(p => p.Wickets);
            var playerBb = playersinnings.Where(s => s.Wickets == playersinnings.Max(w => w.Wickets)).OrderBy(m => m.RunsGiven).First();
            playersStatistics.BB = $"{playerBb.Wickets}/{playerBb.RunsGiven}";
            playersStatistics.FWI = playersinnings.Count(p => p.Wickets > 4 && p.Wickets < 10);
            playersStatistics.TWI = playersinnings.Count(p => p.Wickets > 9);
            playersStatistics.BowlingAvg = (playersStatistics.Wickets == 0) ? 0 : Math.Round((decimal)playersStatistics.RunsGiven / playersStatistics.Wickets, 2, MidpointRounding.AwayFromZero);
            playersStatistics.Econ = (playersStatistics.BallsBowled == 0) ? 0 : Math.Round((decimal)playersStatistics.RunsGiven / Convert.ToDecimal(overs), 2, MidpointRounding.AwayFromZero);
            playersStatistics.BowlingSR = (playersStatistics.Wickets == 0) ? 0 : Math.Round((decimal)playersStatistics.BallsBowled / playersStatistics.Wickets, 2, MidpointRounding.AwayFromZero);

            playersStatistics.Catches = playersinnings.Sum(p => p.Catches);
            playersStatistics.Stumpings = playersinnings.Sum(p => p.Stumpings);

            return playersStatistics;
        }

        public TeamStatistics GetTeamStatistics(IList<TeamDetail> teams)
        {
            var teamsStatistics = new TeamStatistics();
            return teamsStatistics;
        }
    }
}
