using CricketScoreSheetPro.Core.Models;
using Firebase.Database.Query;
using System;

namespace CricketScoreSheetPro.Core.Services
{
    public class UserMatchService : BaseService<UserMatch>, IService<UserMatch>
    {
        public UserMatchService(string uuid, bool importFlg = false)
        {
            _reference = Client.Child(importFlg ? "Imports" : "Users")
                                .Child(uuid)
                                .Child("UserMatches");
        }

        public UserMatch UpdateThisBall(UserMatch currentMatch, string battingTeamName, Ball thisBall, bool undo = false)
        {
            if (thisBall == null) throw new ArgumentNullException("Ball not found");
            if (currentMatch == null) throw new ArgumentNullException("Current match not set");

            Innings battingTeam;
            Innings bowlingTeam; 
            if (currentMatch.HomeTeam.TeamName.ToLower() == battingTeamName.ToLower())
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
                throw new ArgumentException("Team not found");
            }

            if (undo && battingTeam.Balls <= 0) throw new ArgumentException("Batting team innings is not started yet.");
            if (!undo && battingTeam.InningStatus) throw new ArgumentException("Batting team innings is already over.");

            var undoval = undo ? -1 : 1;
            battingTeam.Runs = battingTeam.Runs + 
                ( (thisBall.RunsScored + thisBall.Wide + thisBall.NoBall + thisBall.Byes + thisBall.LegByes) * undoval);
            battingTeam.Wickets = battingTeam.Wickets + 
                    ( (thisBall.HowOut == "not out" || thisBall.HowOut == "retired" ? 0 : 1) * undoval) +
                    ( (thisBall.RunnerHowOut.Contains("run out") ? 1 : 0) * undoval);
            battingTeam.Balls = battingTeam.Balls + ( (thisBall.Wide > 0 || thisBall.NoBall > 0 ? 0 : 1) * undoval);
            battingTeam.Wides = battingTeam.Wides + ( (thisBall.Wide > 0 ? 1 : 0) * undoval);
            battingTeam.NoBalls = battingTeam.NoBalls + ( (thisBall.NoBall > 0 ? 1 : 0) * undoval); 
            battingTeam.Byes = battingTeam.Byes + (thisBall.Byes * undoval);
            battingTeam.LegByes = battingTeam.LegByes + (thisBall.LegByes * undoval);

            if(undo) battingTeam.InningStatus = false;
            else battingTeam.InningStatus = battingTeam.Balls >= currentMatch.TotalOvers * 6;

            //No need to care match status when undo so return
            if (undo) return currentMatch;

            //Match complete
            if (bowlingTeam.InningStatus && battingTeam.InningStatus) currentMatch.MatchComplete = true;

            //Batting team chased successfully
            if (bowlingTeam.InningStatus && battingTeam.Runs > bowlingTeam.Runs)
            {
                currentMatch.MatchComplete = true;
                battingTeam.InningStatus = true;
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

    }
}
