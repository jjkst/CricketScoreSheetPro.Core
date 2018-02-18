using CricketScoreSheetPro.Core.Helper;
using CricketScoreSheetPro.Core.Models;
using System;
using System.Linq;

namespace CricketScoreSheetPro.Core.Services
{
    public class PlayerService : BaseService<Player>
    {
        internal Ball _thisBall { get; set; }
        internal bool _undo { get; set; }

        public PlayerService()
        {
            _reference = Client.Child("Players");
        }

        public PlayerService(Ball thisBall, bool undo = false)
        {
            _thisBall = thisBall ?? throw new ArgumentNullException("Ball not found");
            _undo = undo;
            _reference = Client.Child("Players");
        }

        public Player UpdateBatsmanThisBall(Player batsman)
        {
            if(batsman == null) throw new ArgumentNullException("Batsman not found");
            if (_undo && batsman.BallsPlayed <= 0) throw new ArgumentException("Batsman haven't played any ball");

            var undoval = _undo ? -1 : 1;
            batsman.HowOut = _undo ? "not out" : _thisBall.HowOut;                        
            batsman.RunsTaken = batsman.RunsTaken + (_thisBall.RunsScored * undoval);
            batsman.BallsPlayed = batsman.BallsPlayed + ((_thisBall.Wide > 0 ? 0 : 1) * undoval);
            if (_thisBall.RunsScored == 4) batsman.Fours = batsman.Fours + (1 * undoval);
            if (_thisBall.RunsScored == 6) batsman.Sixes = batsman.Sixes + (1 * undoval);
                
            return batsman;
        }

        public Player UpdateRunnerThisBall(Player runner)
        {
            if (runner == null) throw new ArgumentNullException("Runner not found");

            // Update is runner out
            if (_thisBall.RunnerHowOut == "run out")
            {
                runner.HowOut = _undo ? "not out" : _thisBall.RunnerHowOut;
            }

            return runner;
        }

        public Player UpdateFielderThisBall(Player fielder)
        {
            if (fielder == null) throw new ArgumentNullException("Fielder not found");
            var undoval = _undo ? -1 : 1;
            fielder.Catches = fielder.Catches + ( (_undo && fielder.Catches <= 0) ? 0 :
                ( (_thisBall.HowOut.Contains($"c {fielder.Name}") ? 1 : 0) * undoval));
            fielder.Stumpings = fielder.Stumpings + ( (_undo && fielder.Stumpings <= 0) ? 0 :
                ( (_thisBall.HowOut.Contains($"st †{fielder.Name}") ? 1 : 0) * undoval));          
            fielder.RunOuts = fielder.RunOuts + ( (_undo && fielder.RunOuts <= 0) ? 0 :
                ((_thisBall.HowOut.Contains($"runout {fielder.Name}") ? 1 : 0) * undoval));
            return fielder;
        }

        public Player UpdateBowlerThisBall(Player bowler, AccessService service)
        {
            if (bowler == null) throw new ArgumentNullException("Bowler not found");

            if (_undo && bowler.BallsPlayed <= 0) throw new ArgumentException("Bowler haven't bowled any ball");
            var undoval = _undo ? -1 : 1;
            bowler.RunsGiven = bowler.RunsGiven + 
                ( (_thisBall.RunsScored + _thisBall.Wide + _thisBall.NoBall + _thisBall.Byes + _thisBall.LegByes)*undoval);
            bowler.BallsBowled = bowler.BallsBowled + ( ((_thisBall.Wide > 0 || _thisBall.NoBall > 0) ? 0 : 1) * undoval);
            bowler.Wickets = bowler.Wickets + ( (((_thisBall.HowOut == "not out" || _thisBall.HowOut == "retired") ? 0 : 1) +
                    (!string.IsNullOrEmpty(_thisBall.RunnerHowOut) && _thisBall.RunnerHowOut.Contains("runout") ? 1 : 0)  ) * undoval);
            bowler.Wides = bowler.Wides + ( (_thisBall.Wide > 0 ? 1 : 0) * undoval);
            bowler.NoBalls = bowler.NoBalls + ( (_thisBall.NoBall > 0 ? 1 : 0) * undoval);
            bowler.Maiden = bowler.Maiden + GetMaidenValue(bowler, service);
            return bowler;
        }

        private int GetMaidenValue(Player bowler, AccessService service)
        {
            var bowlerTeam = service.TeamService.GetItem(bowler.TeamId).Result;
            var match = service.MatchService.GetItem(bowler.MatchId).Result;
            var balls = bowlerTeam.Name == match.HomeTeam.TeamName ? AccessService.HomeTeamBalls : AccessService.AwayTeamBalls;
            var thisbowlerbowledballs = balls.Where(p => p.ActiveBowlerId == bowler.Id);
            if (Common.ConvertBallstoOvers(thisbowlerbowledballs.Count()).Split('.')[1] != "0") return 0;

            //check if bowler given runs in last 6 balls if not add or subtract maiden based on undo or not
            int i = 6;
            bool runflg = false;
            foreach (var b in thisbowlerbowledballs.AsEnumerable().Reverse())
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
