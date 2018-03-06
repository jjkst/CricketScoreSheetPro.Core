using CricketScoreSheetPro.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CricketScoreSheetPro.Core.Services.Implementations
{
    public class BallService
    {
        private Ball _thisBall { get; set; }
        private bool _undo { get; set; }

        public BallService(Ball thisBall, bool undo = false)
        {
            _thisBall = thisBall ?? throw new ArgumentNullException("This Ball is null");
            _undo = undo;
        }

        public Match UpdateMatchThisBall(Match currentMatch, string battingTeamName)
        {
            if (currentMatch == null) throw new ArgumentNullException("Current match not set");

            TeamInning battingTeam;
            TeamInning bowlingTeam;
            if (currentMatch.HomeTeam.Name.ToLower() == battingTeamName.ToLower())
            {
                battingTeam = currentMatch.HomeTeam;
                bowlingTeam = currentMatch.AwayTeam;
            }
            else if (currentMatch.AwayTeam.TeamName.ToLower() == battingTeamName.ToLower())
            {
                battingTeam = currentMatch.AwayTeam;
                bowlingTeam = currentMatch.HomeTeam;
            }
            else
            {
                throw new ArgumentException("Batting team not found");
            }

            if (_undo && battingTeam.Balls <= 0) throw new ArgumentException("Batting team innings is not started yet.");
            if (!_undo && battingTeam.Complete) throw new ArgumentException("Batting team innings is already over.");

            var undoval = _undo ? -1 : 1;
            battingTeam.Runs = battingTeam.Runs +
                ((_thisBall.RunsScored + _thisBall.Wide + _thisBall.NoBall + _thisBall.Byes + _thisBall.LegByes) * undoval);
            battingTeam.Wickets = battingTeam.Wickets +
                    ((_thisBall.HowOut == "not out" || _thisBall.HowOut == "retired" ? 0 : 1) * undoval) +
                    ((_thisBall.RunnerHowOut.Contains("run out") ? 1 : 0) * undoval);
            battingTeam.Balls = battingTeam.Balls + ((_thisBall.Wide > 0 || _thisBall.NoBall > 0 ? 0 : 1) * undoval);
            battingTeam.Wides = battingTeam.Wides + ((_thisBall.Wide > 0 ? 1 : 0) * undoval);
            battingTeam.NoBalls = battingTeam.NoBalls + ((_thisBall.NoBall > 0 ? 1 : 0) * undoval);
            battingTeam.Byes = battingTeam.Byes + (_thisBall.Byes * undoval);
            battingTeam.LegByes = battingTeam.LegByes + (_thisBall.LegByes * undoval);

            //No need to care match status when undo so return
            if (_undo)
            {
                battingTeam.Complete = false;
                currentMatch.MatchComplete = false;
                currentMatch.WinningTeamName = string.Empty;
                currentMatch.Comments = string.Empty;
                return currentMatch;
            }

            //Batting innings complete
            battingTeam.Complete = battingTeam.Balls >= currentMatch.TotalOvers * 6;
            
            //Match complete
            if (bowlingTeam.Complete && battingTeam.Complete) currentMatch.MatchComplete = true;

            //Batting team chased successfully
            if (bowlingTeam.Complete && battingTeam.Runs > bowlingTeam.Runs)
            {
                currentMatch.MatchComplete = true;
                battingTeam.Complete = true;
                currentMatch.WinningTeamName = battingTeam.TeamName;
                currentMatch.Comments = battingTeam.TeamName + " won by " + (11 - battingTeam.Wickets) + " wickets";
            }

            //Batting team chase unsuccessfull
            if (currentMatch.MatchComplete && battingTeam.Runs < bowlingTeam.Runs)
            {
                currentMatch.WinningTeamName = bowlingTeam.TeamName;
                var diffruns = bowlingTeam.Runs - battingTeam.Runs;
                currentMatch.Comments = bowlingTeam.TeamName + " won by " + diffruns + " runs";
            }

            //Match is tie
            if (currentMatch.MatchComplete && battingTeam.Runs == bowlingTeam.Runs)
            {
                currentMatch.WinningTeamName = "Tie";
                currentMatch.Comments = "Game is tie";
            }

            return currentMatch;
        }

        public PlayerInning UpdateBatsmanThisBall(PlayerInning batsman)
        {
            if (batsman == null) throw new ArgumentNullException("Batsman not found");
            if (_undo && batsman.BallsPlayed <= 0) throw new ArgumentException("Batsman haven't played any ball");

            var undoval = _undo ? -1 : 1;
            batsman.HowOut = _undo ? "not out" : _thisBall.HowOut;
            batsman.RunsTaken = batsman.RunsTaken + (_thisBall.RunsScored * undoval);
            batsman.BallsPlayed = batsman.BallsPlayed + ((_thisBall.Wide > 0 ? 0 : 1) * undoval);
            if (_thisBall.RunsScored == 4) batsman.Fours = batsman.Fours + (1 * undoval);
            if (_thisBall.RunsScored == 6) batsman.Sixes = batsman.Sixes + (1 * undoval);

            return batsman;
        }

        public PlayerInning UpdateRunnerThisBall(PlayerInning runner)
        {
            if (runner == null) throw new ArgumentNullException("Runner not found");

            // Update is runner out
            if (_thisBall.RunnerHowOut == "run out")
            {
                runner.HowOut = _undo ? "not out" : _thisBall.RunnerHowOut;
            }

            return runner;
        }

        public PlayerInning UpdateFielderThisBall(PlayerInning fielder)
        {
            if (fielder == null) throw new ArgumentNullException("Fielder not found");
            var undoval = _undo ? -1 : 1;
            fielder.Catches = fielder.Catches + ((_undo && fielder.Catches <= 0) ? 0 :
                ((_thisBall.HowOut.Contains($"c {fielder.PlayerName}") ? 1 : 0) * undoval));
            fielder.Stumpings = fielder.Stumpings + ((_undo && fielder.Stumpings <= 0) ? 0 :
                ((_thisBall.HowOut.Contains($"st †{fielder.PlayerName}") ? 1 : 0) * undoval));
            fielder.RunOuts = fielder.RunOuts + ((_undo && fielder.RunOuts <= 0) ? 0 :
                ((_thisBall.HowOut.Contains($"runout {fielder.PlayerName}") ? 1 : 0) * undoval));
            return fielder;
        }

        public PlayerInning UpdateBowlerThisBall(PlayerInning bowler)
        {
            if (bowler == null) throw new ArgumentNullException("Bowler not found");
            if (_undo && bowler.BallsBowled <= 0) throw new ArgumentException("Bowler haven't bowled any ball");

            var undoval = _undo ? -1 : 1;
            bowler.RunsGiven = bowler.RunsGiven +
                ((_thisBall.RunsScored + _thisBall.Wide + _thisBall.NoBall) * undoval);
            bowler.BallsBowled = bowler.BallsBowled + (((_thisBall.Wide > 0 || _thisBall.NoBall > 0) ? 0 : 1) * undoval);
            bowler.Wickets = bowler.Wickets + (((_thisBall.HowOut == "not out" || _thisBall.HowOut == "retired") ? 0 : 1) * undoval);
            bowler.Wides = bowler.Wides + ((_thisBall.Wide > 0 ? 1 : 0) * undoval);
            bowler.NoBalls = bowler.NoBalls + ((_thisBall.NoBall > 0 ? 1 : 0) * undoval);
            return bowler;
        }

        public int GetMaidenValue(List<Ball> thisbowlerovers)
        {
            if (thisbowlerovers == null) throw new ArgumentNullException("Balls collection not found");
            if (thisbowlerovers.Count() % 6 != 0) return 0;

            //check if bowler given runs in last 6 balls if not add or subtract maiden based on undo or not
            int i = 6;
            bool runflg = false;
            foreach (var b in thisbowlerovers.AsEnumerable().Reverse())
            {
                if (i == 0) break;
                if ((b.RunsScored + b.Wide + b.NoBall) > 0)
                {
                    runflg = true;
                    break;
                }
                i = i - (((b.Wide > 0 || b.NoBall > 0) ? 0 : 1) == 1 ? 1 : 0);
            }

            if (runflg) return 0;
            else return _undo ? -1 : 1;
        }
    }
}
