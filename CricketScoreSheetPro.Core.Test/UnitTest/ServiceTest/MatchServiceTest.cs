using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CricketScoreSheetPro.Core.Services;
using CricketScoreSheetPro.Core.Models;
using FluentAssertions;
using System.Collections.Generic;
using CricketScoreSheetPro.Core.Test.Extensions;

namespace CricketScoreSheetPro.Core.Test.UnitTest.ServiceTest
{
    [TestClass]
    public class MatchServiceTest
    {
        private UserMatch _match { get; set; }

        public MatchServiceTest()
        {
            _match = new UserMatch
            {
                Id = "MatchId",
                TournamentId = "TournamentId",
                AddDate = DateTime.Today,
                HomeTeam = new Innings { TeamId = "HomeTeamId" , TeamName = "HomeTeamName", Balls = 100, Byes = 3, LegByes = 3,
                                            NoBalls = 2, Runs = 80, Wickets = 4, Wides = 6},
                AwayTeam = new Innings { TeamId = "AwayTeamId", TeamName = "AwayTeamName"},
                Location = "Richmond, VA",
                TotalOvers = 20,                
                Umpires = new List<string> {"umpireA", "umpireB"}
            };

        }

        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Current match not set")]
        [TestMethod]
        public void UpdateThisBall_NullMatch_Negative()
        {
            //Arrange 
            var matchService = new UserMatchService("muid");

            //Act
            var match = matchService.UpdateThisBall(null, "battingTeamName", new Ball());

            //Assert
            match.Should().BeNull();
        }

        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Ball not found")]
        [TestMethod]
        public void UpdateThisBall_NullBall_Negative()
        {
            //Arrange 
            var matchService = new UserMatchService("muid");

            //Act
            var match = matchService.UpdateThisBall(new UserMatch(), "battingTeamName", null);

            //Assert
            match.Should().BeNull();
        }

        [ExpectedExceptionExtension(typeof(ArgumentException), "Batting team innings is not started yet.")]
        [TestMethod]
        public void UpdateThisBall_NotYetStarted_Negative()
        {
            //Arrange 
            var matchService = new UserMatchService("muid");
            var currentmatch = new UserMatch()
            {
                HomeTeam = new Innings { TeamName = "BattingTeamName", InningStatus = true },
                AwayTeam = new Innings { TeamName = "BowlingTeamName", InningStatus = false }
            };

            //Act
            var match = matchService.UpdateThisBall(currentmatch, "BattingTeamName", new Ball(), true);

            //Assert
            match.Should().BeNull();
        }

        [ExpectedExceptionExtension(typeof(ArgumentException), "Batting team innings is already over.")]
        [TestMethod]
        public void UpdateThisBall_InningsOver_Negative()
        {
            //Arrange 
            var matchService = new UserMatchService("muid");
            var currentmatch = new UserMatch()
            {
                HomeTeam = new Innings { TeamName = "BattingTeamName", InningStatus = true, Balls = 1 },
                AwayTeam = new Innings { TeamName = "BowlingTeamName", InningStatus = false }
            };
            
            //Act
            var match = matchService.UpdateThisBall(currentmatch, "battingTeamName", new Ball());

            //Assert
            match.Should().BeNull();
        }

        [ExpectedExceptionExtension(typeof(ArgumentException), "Team not found")]
        [TestMethod]
        public void UpdateThisBall_WrongTeamName_Negative()
        {
            //Arrange 
            var matchService = new UserMatchService("muid");
            var currentmatch = new UserMatch()
            {
                HomeTeam = new Innings { TeamName = "BattingTeamName", InningStatus = true, Balls = 1 },
                AwayTeam = new Innings { TeamName = "BowlingTeamName", InningStatus = false }
            };

            //Act
            var match = matchService.UpdateThisBall(currentmatch, "NewTeam", new Ball());

            //Assert
            match.Should().BeNull();
        }

        [TestMethod]
        public void UpdateThisBall_Undo_Positive()
        {
            //Arrange 
            var thisball = new Ball
            {
                ActiveBatsmanId = "BatsmanId",
                HowOut = "Bowled",
                RunsScored = 4,
                Wide = 1,
                NoBall = 1,
                Byes = 1,
                LegByes = 1,
                RunnerHowOut = "run out"         
            };
            var runs = _match.HomeTeam.Runs;
            var wickets = _match.HomeTeam.Wickets;
            var balls = _match.HomeTeam.Balls;
            var wides = _match.HomeTeam.Wides;
            var noballs = _match.HomeTeam.NoBalls;
            var byes = _match.HomeTeam.Byes;
            var legbyes = _match.HomeTeam.LegByes;
            
            var matchService = new UserMatchService("muid");

            //Act
            matchService.UpdateThisBall(_match, "HomeTeamName", thisball, true);

            //Assert
            _match.HomeTeam.Runs.Should().Be(runs - thisball.RunsScored - thisball.Wide - thisball.NoBall
                - thisball.Byes - thisball.LegByes);
            _match.HomeTeam.Wickets.Should().Be(wickets - 1 - 1); //bowled + runout
            _match.HomeTeam.Balls.Should().Be(balls);
            _match.HomeTeam.Wides.Should().Be(wides - thisball.Wide);
            _match.HomeTeam.NoBalls.Should().Be(noballs - thisball.NoBall);
            _match.HomeTeam.Byes.Should().Be(byes - thisball.Byes);
            _match.HomeTeam.LegByes.Should().Be(legbyes - thisball.LegByes);
            _match.HomeTeam.InningStatus.Should().BeFalse();
            _match.MatchComplete.Should().BeFalse();
            _match.WinningTeamName.Should().BeNullOrEmpty();
            _match.Comments.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public void UpdateThisBall_Add_Positive()
        {
            //Arrange 
            var thisball = new Ball
            {
                ActiveBatsmanId = "BatsmanId",
                HowOut = "Bowled",
                RunsScored = 4,
                Wide = 1,
                NoBall = 1,
                Byes = 1,
                LegByes = 1,
                RunnerHowOut = "run out"
            };
            var runs = _match.HomeTeam.Runs;
            var wickets = _match.HomeTeam.Wickets;
            var balls = _match.HomeTeam.Balls;
            var wides = _match.HomeTeam.Wides;
            var noballs = _match.HomeTeam.NoBalls;
            var byes = _match.HomeTeam.Byes;
            var legbyes = _match.HomeTeam.LegByes;
            var matchService = new UserMatchService("muid");

            //Act
            matchService.UpdateThisBall(_match, "HomeTeamName", thisball);

            //Assert
            _match.HomeTeam.Runs.Should().Be(runs + thisball.RunsScored + thisball.Wide + thisball.NoBall
                + thisball.Byes + thisball.LegByes);
            _match.HomeTeam.Wickets.Should().Be(wickets + 1 + 1); //bowled + runout
            _match.HomeTeam.Balls.Should().Be(balls);
            _match.HomeTeam.Wides.Should().Be(wides + thisball.Wide);
            _match.HomeTeam.NoBalls.Should().Be(noballs + thisball.NoBall);
            _match.HomeTeam.Byes.Should().Be(byes + thisball.Byes);
            _match.HomeTeam.LegByes.Should().Be(legbyes + thisball.LegByes);
            _match.HomeTeam.InningStatus.Should().BeFalse();
            _match.MatchComplete.Should().BeFalse();
            _match.WinningTeamName.Should().BeNullOrEmpty();
            _match.Comments.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public void UpdateThisBall_BattingCompletedInnings_Positive()
        {
            //Arrange 
            var thisball = new Ball
            {
                ActiveBatsmanId = "BatsmanId",
                HowOut = "Bowled",
                RunsScored = 4,
                Byes = 1,
                LegByes = 1,
                RunnerHowOut = "run out",
            };
            _match.HomeTeam.Balls = (_match.TotalOvers*6) - 1;
            var runs = _match.HomeTeam.Runs;
            var wickets = _match.HomeTeam.Wickets;
            var balls = _match.HomeTeam.Balls;
            var wides = _match.HomeTeam.Wides;
            var noballs = _match.HomeTeam.NoBalls;
            var byes = _match.HomeTeam.Byes;
            var legbyes = _match.HomeTeam.LegByes;
            var matchService = new UserMatchService("muid");

            //Act
            matchService.UpdateThisBall(_match, "HomeTeamName", thisball);

            //Assert
            _match.HomeTeam.Runs.Should().Be(runs + thisball.RunsScored + thisball.Wide + thisball.NoBall
                + thisball.Byes + thisball.LegByes);
            _match.HomeTeam.Wickets.Should().Be(wickets + 1 + 1); //bowled + runout
            _match.HomeTeam.Balls.Should().Be(balls + 1);
            _match.HomeTeam.Wides.Should().Be(wides + thisball.Wide);
            _match.HomeTeam.NoBalls.Should().Be(noballs + thisball.NoBall);
            _match.HomeTeam.Byes.Should().Be(byes + thisball.Byes);
            _match.HomeTeam.LegByes.Should().Be(legbyes + thisball.LegByes);
            _match.HomeTeam.InningStatus.Should().BeTrue();
            _match.MatchComplete.Should().BeFalse();
            _match.WinningTeamName.Should().BeNullOrEmpty();
            _match.Comments.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public void UpdateThisBall_BattingChasedSuccessfully_Positive()
        {
            //Arrange 
            var thisball = new Ball
            {
                ActiveBatsmanId = "BatsmanId",
                HowOut = "Bowled",
                RunsScored = 4,
                Byes = 1,
                LegByes = 1,
                RunnerHowOut = "run out",
            };
            _match.AwayTeam.Runs = _match.HomeTeam.Runs + 3;
            _match.AwayTeam.InningStatus = true;

            var runs = _match.HomeTeam.Runs;
            var wickets = _match.HomeTeam.Wickets;
            var balls = _match.HomeTeam.Balls;
            var wides = _match.HomeTeam.Wides;
            var noballs = _match.HomeTeam.NoBalls;
            var byes = _match.HomeTeam.Byes;
            var legbyes = _match.HomeTeam.LegByes;
            var matchService = new UserMatchService("muid");

            //Act
            matchService.UpdateThisBall(_match, "HomeTeamName", thisball);

            //Assert
            _match.HomeTeam.Runs.Should().Be(runs + thisball.RunsScored + thisball.Wide + thisball.NoBall
                + thisball.Byes + thisball.LegByes);
            _match.HomeTeam.Wickets.Should().Be(wickets + 1 + 1); //bowled + runout
            _match.HomeTeam.Balls.Should().Be(balls + 1);
            _match.HomeTeam.Wides.Should().Be(wides + thisball.Wide);
            _match.HomeTeam.NoBalls.Should().Be(noballs + thisball.NoBall);
            _match.HomeTeam.Byes.Should().Be(byes + thisball.Byes);
            _match.HomeTeam.LegByes.Should().Be(legbyes + thisball.LegByes);
            _match.HomeTeam.InningStatus.Should().BeTrue();
            _match.MatchComplete.Should().BeTrue();
            _match.WinningTeamName.Should().Be(_match.HomeTeam.TeamName);
            _match.Comments.Should().Be(_match.HomeTeam.TeamName + " won by " + (11 - _match.HomeTeam.Wickets) + " wickets");
        }

        [TestMethod]
        public void UpdateThisBall_BattingLostChasing_Positive()
        {
            //Arrange 
            var thisball = new Ball
            {
                ActiveBatsmanId = "BatsmanId",
                HowOut = "Bowled",
                RunnerHowOut = "run out",
            };
            _match.AwayTeam.Runs = _match.HomeTeam.Runs + 3;
            _match.AwayTeam.InningStatus = true;
            _match.HomeTeam.Balls = (_match.TotalOvers * 6) - 1;

            var runs = _match.HomeTeam.Runs;
            var wickets = _match.HomeTeam.Wickets;
            var balls = _match.HomeTeam.Balls;
            var wides = _match.HomeTeam.Wides;
            var noballs = _match.HomeTeam.NoBalls;
            var byes = _match.HomeTeam.Byes;
            var legbyes = _match.HomeTeam.LegByes;
            var matchService = new UserMatchService("muid");

            //Act
            matchService.UpdateThisBall(_match, "HomeTeamName", thisball);

            //Assert
            _match.HomeTeam.Runs.Should().Be(runs + thisball.RunsScored + thisball.Wide + thisball.NoBall
                + thisball.Byes + thisball.LegByes);
            _match.HomeTeam.Wickets.Should().Be(wickets + 1 + 1); //bowled + runout
            _match.HomeTeam.Balls.Should().Be(balls + 1);
            _match.HomeTeam.Wides.Should().Be(wides + thisball.Wide);
            _match.HomeTeam.NoBalls.Should().Be(noballs + thisball.NoBall);
            _match.HomeTeam.Byes.Should().Be(byes + thisball.Byes);
            _match.HomeTeam.LegByes.Should().Be(legbyes + thisball.LegByes);
            _match.HomeTeam.InningStatus.Should().BeTrue();
            _match.MatchComplete.Should().BeTrue();
            _match.WinningTeamName.Should().Be(_match.AwayTeam.TeamName);
            _match.Comments.Should().Be(_match.AwayTeam.TeamName + " won by "+(_match.AwayTeam.Runs-_match.HomeTeam.Runs+ " runs"));
        }

        [TestMethod]
        public void UpdateThisBall_Tie_Positive()
        {
            //Arrange 
            var thisball = new Ball
            {
                ActiveBatsmanId = "BatsmanId",
                HowOut = "Bowled",
                RunnerHowOut = "run out",
            };
            _match.AwayTeam.Runs = _match.HomeTeam.Runs;
            _match.AwayTeam.InningStatus = true;
            _match.HomeTeam.Balls = (_match.TotalOvers * 6) - 1; 

            var runs = _match.HomeTeam.Runs;
            var wickets = _match.HomeTeam.Wickets;
            var balls = _match.HomeTeam.Balls;
            var wides = _match.HomeTeam.Wides;
            var noballs = _match.HomeTeam.NoBalls;
            var byes = _match.HomeTeam.Byes;
            var legbyes = _match.HomeTeam.LegByes;
            var matchService = new UserMatchService("muid");

            //Act
            matchService.UpdateThisBall(_match, "HomeTeamName", thisball);

            //Assert
            _match.HomeTeam.Runs.Should().Be(runs + thisball.RunsScored + thisball.Wide + thisball.NoBall
                + thisball.Byes + thisball.LegByes);
            _match.HomeTeam.Wickets.Should().Be(wickets + 1 + 1); //bowled + runout
            _match.HomeTeam.Balls.Should().Be(balls + 1);
            _match.HomeTeam.Wides.Should().Be(wides + thisball.Wide);
            _match.HomeTeam.NoBalls.Should().Be(noballs + thisball.NoBall);
            _match.HomeTeam.Byes.Should().Be(byes + thisball.Byes);
            _match.HomeTeam.LegByes.Should().Be(legbyes + thisball.LegByes);
            _match.HomeTeam.InningStatus.Should().BeTrue();
            _match.MatchComplete.Should().BeTrue();
            _match.WinningTeamName.Should().Be("Tie");
            _match.Comments.ToLower().Should().Be("game is tie");
        }
    }
}
