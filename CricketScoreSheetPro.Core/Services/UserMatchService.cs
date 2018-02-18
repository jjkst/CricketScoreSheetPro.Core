using CricketScoreSheetPro.Core.Models;
using Firebase.Database.Query;

namespace CricketScoreSheetPro.Core.Services
{
    public class UserMatchService : BaseService<UserMatch>, IService<UserMatch>
    {
        private UserMatch CurrentMatch { get; set; }

        private Innings BattingTeam { get; set; }

        private Innings BowlingTeam { get; set; }

        public UserMatchService(string uuid, bool importFlg = false)
        {
            _reference = Client.Child(importFlg ? "Imports" : "Users")
                                .Child(uuid)
                                .Child("UserMatches");
        }

        public bool UpdateThisBall(string matchId, string battingTeamName, Ball thisBall)
        {
            bool updateSuccessful = false;
            if (thisBall == null || string.IsNullOrEmpty(matchId)) return updateSuccessful;

            CurrentMatch = GetItem(matchId).Result;
            if (CurrentMatch == null) throw new System.Exception("Current match is not set");
            SetBattingAndBowlingTeam(battingTeamName);

            if (!BattingTeam.InningStatus) return updateSuccessful;

            BattingTeam.Runs = BattingTeam.Runs + (thisBall.RunsScored + thisBall.Wide + thisBall.NoBall + thisBall.Byes + thisBall.LegByes);
            BattingTeam.Wickets = BattingTeam.Wickets + ((thisBall.HowOut == "not out" || thisBall.HowOut == "retired") ? 0 : 1) +
                    (!string.IsNullOrEmpty(thisBall.RunnerHowOut) && thisBall.RunnerHowOut.Contains("runout") ? 1 : 0);
            BattingTeam.Balls = BattingTeam.Balls + ((thisBall.Wide > 0 || thisBall.NoBall > 0) ? 0 : 1); ;
            BattingTeam.Wides = BattingTeam.Wides + ((thisBall.Wide > 0) ? 1 : 0); ;
            BattingTeam.NoBalls = BattingTeam.NoBalls + ((thisBall.NoBall > 0) ? 1 : 0);
            BattingTeam.Byes = BattingTeam.Byes + thisBall.Byes;
            BattingTeam.LegByes = BattingTeam.LegByes + thisBall.LegByes;
            BattingTeam.InningStatus = BattingTeam.Balls >= CurrentMatch.TotalOvers * 6;

            //Match complete
            if (BowlingTeam.InningStatus && (BattingTeam.InningStatus || BattingTeam.Runs > BowlingTeam.Runs))
            {
                CurrentMatch.MatchComplete = true;
                BattingTeam.InningStatus = true;

                // Chased successfully
                if (BattingTeam.Runs > BowlingTeam.Runs)
                {
                    CurrentMatch.WinningTeamName = BattingTeam.TeamName;
                    CurrentMatch.Comments = BattingTeam.TeamName + " won by " +
                        (11 - BattingTeam.Wickets) + " wickets";
                }
                // batting team lost
                else if (BattingTeam.Runs < BowlingTeam.Runs)
                {
                    var diffruns = BowlingTeam.Runs - BattingTeam.Runs;
                    CurrentMatch.Comments = BowlingTeam.TeamName + " won by " + diffruns + " runs";
                }
                // Game Tie
                else if (BattingTeam.Runs == BowlingTeam.Runs)
                {
                    CurrentMatch.Comments = "Game is tie";
                }
            }

            if (CurrentMatch.HomeTeam.TeamName == battingTeamName)
                CurrentMatch.HomeTeam = BattingTeam;
            else
                CurrentMatch.AwayTeam = BattingTeam;
            updateSuccessful = CreateWithId(matchId, CurrentMatch).Result == CurrentMatch;

            return updateSuccessful;
        }

        public bool UndoThisBall(string matchId, string battingTeamName, Ball thisBall)
        {
            bool updateSuccessful = false;
            if (thisBall == null || string.IsNullOrEmpty(matchId)) return updateSuccessful;

            CurrentMatch = GetItem(matchId).Result;
            if (CurrentMatch == null) throw new System.Exception("Current match is not set");
            SetBattingAndBowlingTeam(battingTeamName);         

            if (BattingTeam.Balls > 0)
            {
                BattingTeam.Runs = BattingTeam.Runs - (thisBall.RunsScored + thisBall.Wide + thisBall.NoBall + thisBall.Byes + thisBall.LegByes);
                BattingTeam.Wickets = BattingTeam.Wickets - ((thisBall.HowOut == "not out" || thisBall.HowOut == "retired") ? 0 : 1) -
                        (!string.IsNullOrEmpty(thisBall.RunnerHowOut) && thisBall.RunnerHowOut.Contains("runout") ? 1 : 0);
                BattingTeam.Balls = BattingTeam.Balls - ((thisBall.Wide > 0 || thisBall.NoBall > 0) ? 0 : 1); ;
                BattingTeam.Wides = BattingTeam.Wides - ((thisBall.Wide > 0) ? 1 : 0); ;
                BattingTeam.NoBalls = BattingTeam.NoBalls - ((thisBall.NoBall > 0) ? 1 : 0);
                BattingTeam.Byes = BattingTeam.Byes - thisBall.Byes;
                BattingTeam.LegByes = BattingTeam.LegByes - thisBall.LegByes;
                BattingTeam.InningStatus = false;

                if (CurrentMatch.HomeTeam.TeamName == battingTeamName)
                    CurrentMatch.HomeTeam = BattingTeam;
                else
                    CurrentMatch.AwayTeam = BattingTeam;
                updateSuccessful = CreateWithId(matchId, CurrentMatch).Result == CurrentMatch;
            }

            return updateSuccessful;
        }

        private void SetBattingAndBowlingTeam(string battingTeamName)
        {          
            var homeTeam = CurrentMatch.HomeTeam;
            var awayTeam = CurrentMatch.AwayTeam;

            if (homeTeam.TeamName == battingTeamName)
            {
                BattingTeam = homeTeam;
                BowlingTeam = awayTeam;
            }
            else
            {
                BattingTeam = awayTeam;
                BowlingTeam = homeTeam;
            }
        }
    }
}
