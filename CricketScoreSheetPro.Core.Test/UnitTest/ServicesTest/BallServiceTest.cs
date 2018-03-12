﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CricketScoreSheetPro.Core.Test.Extensions;
using CricketScoreSheetPro.Core.Services.Implementations;
using CricketScoreSheetPro.Core.Models;
using FluentAssertions;
using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Test.UnitTest.ServicesTest
{
    [TestClass]
    public class BallServiceTest
    {
        private static BallService BallService { get; set; }
        private static Ball Thisball { get; set; }
        private static Match Match { get; set; }
        private static PlayerInning Player { get; set; }

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            
        }

        [TestInitialize]
        public void MethodInit()
        {
            Thisball = new Ball
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

            Match = new Match
            {
                Id = "MatchId",
                TournamentId = "TournamentId",
                AddDate = DateTime.Today,
                HomeTeam = new TeamInning
                {
                    TeamId = "HomeTeamId",
                    TeamName = "HomeTeamName",
                    Balls = 100,
                    Byes = 3,
                    LegByes = 3,
                    NoBalls = 2,
                    Runs = 80,
                    Wickets = 4,
                    Wides = 6
                },
                AwayTeam = new TeamInning { TeamId = "AwayTeamId", TeamName = "AwayTeamName" },
                Location = "Richmond, VA",
                TotalOvers = 20,
                PrimaryUmpire = "umpireA",
                SecondaryUmpire = "umpireB" 
            };

            Player = new PlayerInning
            {
                PlayerName = "JK",
                RunsTaken = 10,
                BallsPlayed = 4,
                Fours = 1,
                Sixes = 1,
                RunsGiven = 20,
                BallsBowled = 20,
                Wickets = 5,
                Maiden = 1,
                NoBalls = 3,
                Wides = 2,
                Catches = 1,
                Stumpings = 1,
                RunOuts = 1
            };
        }

        #region UpdateMatchThisBall

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "This Ball is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void WhenThisBall_NullBall()
        {
            //Arrange
            var ballService = new BallService(null);

            //Act
            var val = ballService.UpdateBatsman(new PlayerInning());

            //Assert
            val.Should().BeNull();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Current match not set")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateMatchThisBall_NullMatch()
        {
            //Arrange 
            var ballService = new BallService(new Ball());

            //Act
            var match = ballService.UpdateMatch(null, "battingTeamName");

            //Assert
            match.Should().BeNull();
        }
        
        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "Batting team not found")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateMatchThisBall_BattingTeamNotExist()
        {
            //Arrange 
            var ballService = new BallService(new Ball());

            //Act
            var match = ballService.UpdateMatch(Match, "battingTeamName");

            //Assert
            match.Should().BeNull();
        }
        
        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "Batting team innings is not started yet.")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateMatchThisBall_UndoScoreOnBattingTeamNotYetStarted()
        {
            //Arrange 
            var ballService = new BallService(new Ball(), true);
            Match.HomeTeam.Balls = 0;

            //Act
            var match = ballService.UpdateMatch(Match, "HomeTeamName");

            //Assert
            match.Should().BeNull();
        }
        
        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "Batting team innings is already over.")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateMatchThisBall_UpdateScoreOnInningsOver()
        {
            //Arrange 
            var ballService = new BallService(new Ball());
            Match.HomeTeam.Complete = true;

            //Act
            var match = ballService.UpdateMatch(Match, "HomeTeamName");

            //Assert
            match.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateMatchThisBall_UndoScore()
        {
            //Arrange
            var ballService = new BallService(Thisball, true);
            var runs = Match.HomeTeam.Runs;
            var wickets = Match.HomeTeam.Wickets;
            var balls = Match.HomeTeam.Balls;
            var wides = Match.HomeTeam.Wides;
            var noballs = Match.HomeTeam.NoBalls;
            var byes = Match.HomeTeam.Byes;
            var legbyes = Match.HomeTeam.LegByes;

            //Act
            ballService.UpdateMatch(Match, "HomeTeamName");

            //Assert
            Match.HomeTeam.Runs.Should().Be(runs - Thisball.RunsScored - Thisball.Wide - Thisball.NoBall
                - Thisball.Byes - Thisball.LegByes);
            Match.HomeTeam.Wickets.Should().Be(wickets - 1 - 1); //bowled + runout
            Match.HomeTeam.Balls.Should().Be(balls); //wide or noball
            Match.HomeTeam.Wides.Should().Be(wides - Thisball.Wide);
            Match.HomeTeam.NoBalls.Should().Be(noballs - Thisball.NoBall);
            Match.HomeTeam.Byes.Should().Be(byes - Thisball.Byes);
            Match.HomeTeam.LegByes.Should().Be(legbyes - Thisball.LegByes);
            Match.HomeTeam.Complete.Should().BeFalse();
            Match.MatchComplete.Should().BeFalse();
            Match.WinningTeamName.Should().BeNullOrEmpty();
            Match.Comments.Should().BeNullOrEmpty();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateMatchThisBall_UpdateScore()
        {
            //Arrange
            var ballService = new BallService(Thisball);
            var runs = Match.HomeTeam.Runs;
            var wickets = Match.HomeTeam.Wickets;
            var balls = Match.HomeTeam.Balls;
            var wides = Match.HomeTeam.Wides;
            var noballs = Match.HomeTeam.NoBalls;
            var byes = Match.HomeTeam.Byes;
            var legbyes = Match.HomeTeam.LegByes;

            //Act
            ballService.UpdateMatch(Match, "HomeTeamName");

            //Assert
            Match.HomeTeam.Runs.Should().Be(runs + Thisball.RunsScored + Thisball.Wide + Thisball.NoBall
                + Thisball.Byes + Thisball.LegByes);
            Match.HomeTeam.Wickets.Should().Be(wickets + 1 + 1); //bowled + runout
            Match.HomeTeam.Balls.Should().Be(balls); //wide or noball
            Match.HomeTeam.Wides.Should().Be(wides + Thisball.Wide);
            Match.HomeTeam.NoBalls.Should().Be(noballs + Thisball.NoBall);
            Match.HomeTeam.Byes.Should().Be(byes + Thisball.Byes);
            Match.HomeTeam.LegByes.Should().Be(legbyes + Thisball.LegByes);            
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateMatchThisBall_AwayTeamUpdateScore()
        {
            //Arrange
            Thisball.HowOut = "not out";
            Thisball.RunnerHowOut = "not out";
            var ballService = new BallService(Thisball);           
            var runs = Match.AwayTeam.Runs;
            var wickets = Match.AwayTeam.Wickets;
            var balls = Match.AwayTeam.Balls;
            var wides = Match.AwayTeam.Wides;
            var noballs = Match.AwayTeam.NoBalls;
            var byes = Match.AwayTeam.Byes;
            var legbyes = Match.AwayTeam.LegByes;

            //Act
            ballService.UpdateMatch(Match, "AwayTeamName");

            //Assert
            Match.AwayTeam.Runs.Should().Be(runs + Thisball.RunsScored + Thisball.Wide + Thisball.NoBall
                + Thisball.Byes + Thisball.LegByes);
            Match.AwayTeam.Wickets.Should().Be(wickets); 
            Match.AwayTeam.Balls.Should().Be(balls); //wide or noball
            Match.AwayTeam.Wides.Should().Be(wides + Thisball.Wide);
            Match.AwayTeam.NoBalls.Should().Be(noballs + Thisball.NoBall);
            Match.AwayTeam.Byes.Should().Be(byes + Thisball.Byes);
            Match.AwayTeam.LegByes.Should().Be(legbyes + Thisball.LegByes);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateMatchThisBall_BattingTeamInningsCompleteWhenOverareDone()
        {
            //Arrange
            Thisball.Wide = 0;
            Thisball.NoBall = 0;
            var ballService = new BallService(Thisball);
            Match.HomeTeam.Balls = (Match.TotalOvers * 6) - 1;
            var runs = Match.HomeTeam.Runs;
            var wickets = Match.HomeTeam.Wickets;
            var balls = Match.HomeTeam.Balls;
            var wides = Match.HomeTeam.Wides;
            var noballs = Match.HomeTeam.NoBalls;
            var byes = Match.HomeTeam.Byes;
            var legbyes = Match.HomeTeam.LegByes;
            
            //Act
            ballService.UpdateMatch(Match, "HomeTeamName");

            //Assert
            Match.HomeTeam.Runs.Should().Be(runs + Thisball.RunsScored + Thisball.Wide + Thisball.NoBall
                + Thisball.Byes + Thisball.LegByes);
            Match.HomeTeam.Wickets.Should().Be(wickets + 1 + 1); //bowled + runout
            Match.HomeTeam.Balls.Should().Be(balls + 1); 
            Match.HomeTeam.Wides.Should().Be(wides + Thisball.Wide);
            Match.HomeTeam.NoBalls.Should().Be(noballs + Thisball.NoBall);
            Match.HomeTeam.Byes.Should().Be(byes + Thisball.Byes);
            Match.HomeTeam.LegByes.Should().Be(legbyes + Thisball.LegByes);
            Match.HomeTeam.Complete.Should().BeTrue();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateMatchThisBall_BattingTeamChasedSuccessfully()
        {
            //Arrange
            Thisball.Wide = 0;
            Thisball.NoBall = 0;
            Thisball.RunsScored = 4;
            var ballService = new BallService(Thisball);
            Match.AwayTeam.Runs = Match.HomeTeam.Runs + 3;
            Match.AwayTeam.Complete = true;
            var runs = Match.HomeTeam.Runs;
            var wickets = Match.HomeTeam.Wickets;
            var balls = Match.HomeTeam.Balls;
            var wides = Match.HomeTeam.Wides;
            var noballs = Match.HomeTeam.NoBalls;
            var byes = Match.HomeTeam.Byes;
            var legbyes = Match.HomeTeam.LegByes;

            //Act
            ballService.UpdateMatch(Match, "HomeTeamName");

            //Assert
            Match.HomeTeam.Runs.Should().Be(runs + Thisball.RunsScored + Thisball.Wide + Thisball.NoBall
                + Thisball.Byes + Thisball.LegByes);
            Match.HomeTeam.Wickets.Should().Be(wickets + 1 + 1); //bowled + runout
            Match.HomeTeam.Balls.Should().Be(balls + 1);
            Match.HomeTeam.Wides.Should().Be(wides + Thisball.Wide);
            Match.HomeTeam.NoBalls.Should().Be(noballs + Thisball.NoBall);
            Match.HomeTeam.Byes.Should().Be(byes + Thisball.Byes);
            Match.HomeTeam.LegByes.Should().Be(legbyes + Thisball.LegByes);
            Match.HomeTeam.Complete.Should().BeTrue();
            Match.MatchComplete.Should().BeTrue();
            Match.WinningTeamName.Should().Be(Match.HomeTeam.TeamName);
            Match.Comments.Should().Be(Match.HomeTeam.TeamName + " won by " + (11 - Match.HomeTeam.Wickets) + " wickets"); //total wickets ???
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateMatchThisBall_BattingTeamLostChasing()
        {
            //Arrange
            Thisball.Wide = 0;
            Thisball.NoBall = 0;
            Thisball.RunsScored = 0;
            Thisball.Byes = 0;
            Thisball.LegByes = 0;
            var ballService = new BallService(Thisball);
            Match.AwayTeam.Runs = Match.HomeTeam.Runs + 3;
            Match.AwayTeam.Complete = true;
            Match.HomeTeam.Balls = (Match.TotalOvers * 6) - 1;
            var runs = Match.HomeTeam.Runs;
            var wickets = Match.HomeTeam.Wickets;
            var balls = Match.HomeTeam.Balls;
            var wides = Match.HomeTeam.Wides;
            var noballs = Match.HomeTeam.NoBalls;
            var byes = Match.HomeTeam.Byes;
            var legbyes = Match.HomeTeam.LegByes;

            //Act
            ballService.UpdateMatch(Match, "HomeTeamName");

            //Assert
            Match.HomeTeam.Runs.Should().Be(runs + Thisball.RunsScored + Thisball.Wide + Thisball.NoBall
                + Thisball.Byes + Thisball.LegByes);
            Match.HomeTeam.Wickets.Should().Be(wickets + 1 + 1); //bowled + runout
            Match.HomeTeam.Balls.Should().Be(balls + 1);
            Match.HomeTeam.Wides.Should().Be(wides + Thisball.Wide);
            Match.HomeTeam.NoBalls.Should().Be(noballs + Thisball.NoBall);
            Match.HomeTeam.Byes.Should().Be(byes + Thisball.Byes);
            Match.HomeTeam.LegByes.Should().Be(legbyes + Thisball.LegByes);
            Match.HomeTeam.Complete.Should().BeTrue();
            Match.MatchComplete.Should().BeTrue();
            Match.WinningTeamName.Should().Be(Match.AwayTeam.TeamName);
            Match.Comments.Should().Be(Match.AwayTeam.TeamName + " won by " + (Match.AwayTeam.Runs - Match.HomeTeam.Runs + " runs"));
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateMatchThisBall_MatchIsTie()
        {
            //Arrange
            Thisball.Wide = 0;
            Thisball.NoBall = 0;
            Thisball.RunsScored = 0;
            Thisball.Byes = 0;
            Thisball.LegByes = 0;
            var ballService = new BallService(Thisball);           
            Match.AwayTeam.Complete = true;
            Match.HomeTeam.Balls = (Match.TotalOvers * 6) - 1;
            Match.AwayTeam.Runs = Match.HomeTeam.Runs;

            //Act
            ballService.UpdateMatch(Match, "HomeTeamName");

            //Assert
            Match.HomeTeam.Complete.Should().BeTrue();
            Match.MatchComplete.Should().BeTrue();
            Match.WinningTeamName.Should().Be("Tie");
            Match.Comments.Should().Be("Game is tie");
        }

        #endregion UpdateMatchThisBall

        #region UpdateBatsmanThisBall

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Batsman not found")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateBatsmanThisBall_Null()
        {
            //Arrange
            var ballService = new BallService(Thisball);

            //Act
            var val = ballService.UpdateBatsman(null);

            //Assert
            val.Should().BeNull();
        }
        
        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "Batsman haven't played any ball")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateBatsmanThisBall_UndoWhenZeroBalls()
        {
            //Arrange
            Player.BallsPlayed = 0;
            var ballService = new BallService(Thisball, true);

            //Act
            var val = ballService.UpdateBatsman(Player);

            //Assert
            val.Should().NotBeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateBatsmanThisBall_UpdateScore()
        {
            //Arrange                      
            var thisball = new Ball
            {
                ActiveBatsmanId = "BatsmanId",
                HowOut = "Bowled",
                RunsScored = 4
            };
            var ballService = new BallService(thisball);
            var howout = Player.HowOut;
            var runstaken = Player.RunsTaken;
            var ballsplayed = Player.BallsPlayed;
            var fours = Player.Fours;
            var sixes = Player.Sixes;

            //Act
            var val = ballService.UpdateBatsman(Player);

            //Assert
            Player.Should().NotBeNull();
            Player.HowOut.Should().Be(thisball.HowOut);
            Player.RunsTaken.Should().Be(runstaken + thisball.RunsScored);
            Player.BallsPlayed.Should().Be(ballsplayed + 1);
            Player.Fours.Should().Be(fours + 1);
            Player.Sixes.Should().Be(sixes);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateBatsmanThisBall_UpdateScoreWideBall()
        {
            //Arrange                      
            var thisball = new Ball
            {
                ActiveBatsmanId = "BatsmanId",
                RunsScored = 6,
                Wide = 1
            };
            var ballService = new BallService(thisball);
            var howout = Player.HowOut;
            var runstaken = Player.RunsTaken;
            var ballsplayed = Player.BallsPlayed;
            var fours = Player.Fours;
            var sixes = Player.Sixes;

            //Act
            var val = ballService.UpdateBatsman(Player);

            //Assert
            Player.Should().NotBeNull();
            Player.HowOut.Should().Be("not out");
            Player.RunsTaken.Should().Be(runstaken + thisball.RunsScored);
            Player.BallsPlayed.Should().Be(ballsplayed); //wide
            Player.Fours.Should().Be(fours);
            Player.Sixes.Should().Be(sixes + 1);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateBatsmanThisBall_UndoScore()
        {
            //Arrange                      
            var thisball = new Ball
            {
                ActiveBatsmanId = "BatsmanId",
                HowOut = "Bowled",
                RunsScored = 1
            };
            var ballService = new BallService(thisball, true);
            var runstaken = Player.RunsTaken;
            var ballsplayed = Player.BallsPlayed;
            var fours = Player.Fours;
            var sixes = Player.Sixes;

            //Act
            var val = ballService.UpdateBatsman(Player);

            //Assert
            Player.Should().NotBeNull();
            Player.HowOut.Should().Be("not out");
            Player.RunsTaken.Should().Be(runstaken - thisball.RunsScored);
            Player.BallsPlayed.Should().Be(ballsplayed - 1);
            Player.Fours.Should().Be(fours);
            Player.Sixes.Should().Be(sixes);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateBatsmanThisBall_UndoScoreWhenWide()
        {
            //Arrange                      
            var thisball = new Ball
            {
                ActiveBatsmanId = "BatsmanId",
                RunsScored = 1,
                Wide = 1
            };
            var ballService = new BallService(thisball, true);
            var runstaken = Player.RunsTaken;
            var ballsplayed = Player.BallsPlayed;
            var fours = Player.Fours;
            var sixes = Player.Sixes;

            //Act
            var val = ballService.UpdateBatsman(Player);

            //Assert
            Player.Should().NotBeNull();
            Player.HowOut.Should().Be("not out");
            Player.RunsTaken.Should().Be(runstaken - thisball.RunsScored);
            Player.BallsPlayed.Should().Be(ballsplayed);
            Player.Fours.Should().Be(fours);
            Player.Sixes.Should().Be(sixes);
        }

        #endregion UpdateBatsmanThisBall

        #region UpdateRunnerThisBall

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Runner not found")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateRunnerThisBall_Null()
        {
            //Arrange
            var ballService = new BallService(new Ball());

            //Act
            var val = ballService.UpdateRunner(null);

            //Assert
            val.Should().NotBeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateRunnerThisBall_NoChange()
        {
            //Arrange
            var thisball = new Ball
            {
                RunnerBatsmanId = "RunnerId",
            };
            var howout = Player.HowOut;
            var ballService = new BallService(thisball);

            //Act
            ballService.UpdateRunner(Player);

            //Assert
            Player.HowOut.Should().Be(howout);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateRunnerThisBall_runout()
        {
            //Arrange
            var thisball = new Ball
            {
                RunnerBatsmanId = "RunnerId",
                RunnerHowOut = "run out"
            };
            var ballService = new BallService(thisball);

            //Act
            ballService.UpdateRunner(Player);

            //Assert
            Player.HowOut.Should().Be(thisball.RunnerHowOut);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateRunnerThisBall_UndoRunOut()
        {
            //Arrange
            var thisball = new Ball
            {
                RunnerBatsmanId = "RunnerId",
                RunnerHowOut = "run out"
            };
            var ballService = new BallService(thisball, true);

            //Act
            ballService.UpdateRunner(Player);

            //Assert
            Player.HowOut.Should().Be("not out");
        }

        #endregion UpdateRunnerThisBall

        #region UpdateFielderThisBall
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Fielder not found")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateFielderThisBall_Null()
        {
            //Arrange
            var ballService = new BallService(new Ball());

            //Act
            var val = ballService.UpdateFielder(null);

            //Assert
            val.Should().NotBeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateFielderThisBall_Catch()
        {
            //Arrange
            var thisball = new Ball
            {
                FielderId = "fielderId",
                HowOut = $"c {Player.PlayerName}"
            };
            var catches = Player.Catches;
            var ballService = new BallService(thisball);

            //Act
            ballService.UpdateFielder(Player);

            //Assert
            Player.Catches.Should().Be(catches + 1);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateFielderThisBall_UndoCatch()
        {
            //Arrange
            var thisball = new Ball
            {
                FielderId = "fielderId",
                HowOut = $"c {Player.PlayerName}"
            };
            var catches = Player.Catches;
            var ballService = new BallService(thisball, true);

            //Act
            ballService.UpdateFielder(Player);

            //Assert
            Player.Catches.Should().Be(catches - 1);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateFielderThisBall_UndoCatchWhenZero()
        {
            //Arrange
            var thisball = new Ball
            {
                FielderId = "fielderId",
                HowOut = $"c {Player.PlayerName}"
            };
            Player.Catches = 0;
            var ballService = new BallService(thisball, true);

            //Act
            ballService.UpdateFielder(Player);

            //Assert
            Player.Catches.Should().Be(0);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateFielderThisBall_Stumping()
        {
            //Arrange
            var thisball = new Ball
            {
                FielderId = "fielderId",
                HowOut = $"st †{Player.PlayerName}"
            };
            var stumpings = Player.Stumpings;
            var ballService = new BallService(thisball);

            //Act
            ballService.UpdateFielder(Player);

            //Assert
            Player.Stumpings.Should().Be(stumpings + 1);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateFielderThisBall_UndoStumping()
        {
            //Arrange
            var thisball = new Ball
            {
                FielderId = "fielderId",
                HowOut = $"st †{Player.PlayerName}"
            };
            var stumping = Player.Stumpings;
            var ballService = new BallService(thisball, true);

            //Act
            ballService.UpdateFielder(Player);

            //Assert
            Player.Stumpings.Should().Be(stumping - 1);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateFielderThisBall_UndoStumpingWhenZero()
        {
            //Arrange
            var thisball = new Ball
            {
                FielderId = "fielderId",
                HowOut = $"st †{Player.PlayerName}"
            };
            Player.Stumpings = 0;
            var ballService = new BallService(thisball, true);

            //Act
            ballService.UpdateFielder(Player);

            //Assert
            Player.Stumpings.Should().Be(0);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateFielderThisBall_Runouts()
        {
            //Arrange
            var thisball = new Ball
            {
                FielderId = "fielderId",
                HowOut = $"runout {Player.PlayerName}"
            };
            var runouts = Player.RunOuts;
            var ballService = new BallService(thisball);

            //Act
            ballService.UpdateFielder(Player);

            //Assert
            Player.RunOuts.Should().Be(runouts + 1);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateFielderThisBall_UndoRunouts()
        {
            //Arrange
            var thisball = new Ball
            {
                FielderId = "fielderId",
                HowOut = $"runout {Player.PlayerName}"
            };
            var runouts = Player.RunOuts;
            var ballService = new BallService(thisball, true);

            //Act
            ballService.UpdateFielder(Player);


            //Assert
            Player.RunOuts.Should().Be(runouts - 1);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateFielderThisBall_UndoRunoutsWhenZero()
        {
            //Arrange
            var thisball = new Ball
            {
                FielderId = "fielderId",
                HowOut = $"runout {Player.PlayerName}"
            };
            Player.RunOuts = 0;
            var ballService = new BallService(thisball, true);

            //Act
            ballService.UpdateFielder(Player);

            //Assert
            Player.RunOuts.Should().Be(0);
        }


        #endregion UpdateFielderThisBall

        #region UpdateBowlerThisBall
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Bowler not found")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateBowlerThisBall_Null()
        {
            //Arrange
            var ballService = new BallService(new Ball());

            //Act
            var val = ballService.UpdateBowler(null);

            //Assert
            val.Should().NotBeNull();
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Bowler haven't bowled any ball")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateBowlerThisBall_UndoWhenZeroBallsBowled()
        {
            //Arrange
            var ballService = new BallService(new Ball(), true);

            //Act
            var val = ballService.UpdateBowler(new PlayerInning());

            //Assert
            val.Should().NotBeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateBowler()
        {
            //Arrange
            var thisball = new Ball
            {
                ActiveBowlerId = "BowlerId",
                RunsScored = 4,
                Byes = 1,
                LegByes = 1,
                HowOut = "Bowled",
            };
            var howout = Player.HowOut;
            var runsgiven = Player.RunsGiven;
            var ballsBowled = Player.BallsBowled;
            var wickets = Player.Wickets;
            var wides = Player.Wides;
            var noballs = Player.NoBalls;
            var ballService = new BallService(thisball);

            //Act
            ballService.UpdateBowler(Player);

            //Assert
            Player.Should().NotBeNull();
            Player.RunsGiven.Should().Be(runsgiven + thisball.RunsScored + thisball.Wide + thisball.NoBall);
            Player.BallsBowled.Should().Be(ballsBowled + 1);
            Player.Wickets.Should().Be(wickets + 1);
            Player.Wides.Should().Be(wides);
            Player.NoBalls.Should().Be(noballs);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateBowlerThisBall_UndoScore()
        {
            //Arrange
            var thisball = new Ball
            {
                ActiveBowlerId = "BowlerId",
                RunsScored = 4,
                Byes = 1,
                LegByes = 1,
                HowOut = "Bowled",
            };
            var howout = Player.HowOut;
            var runsgiven = Player.RunsGiven;
            var ballsBowled = Player.BallsBowled;
            var wickets = Player.Wickets;
            var wides = Player.Wides;
            var noballs = Player.NoBalls;
            var ballService = new BallService(thisball, true);

            //Act
            ballService.UpdateBowler(Player);

            //Assert
            Player.Should().NotBeNull();
            Player.RunsGiven.Should().Be(runsgiven - thisball.RunsScored - thisball.Wide - thisball.NoBall);
            Player.BallsBowled.Should().Be(ballsBowled - 1);
            Player.Wickets.Should().Be(wickets - 1);
            Player.Wides.Should().Be(wides);
            Player.NoBalls.Should().Be(noballs);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateBowlerThisBall_UpdateScoreWhenWide()
        {
            //Arrange
            var thisball = new Ball
            {
                ActiveBowlerId = "BowlerId",
                RunsScored = 0,
                Wide = 1,
                Byes = 1,
                LegByes = 1
            };

            var runsgiven = Player.RunsGiven;
            var ballsBowled = Player.BallsBowled;
            var wickets = Player.Wickets;
            var wides = Player.Wides;
            var noballs = Player.NoBalls;
            var ballService = new BallService(thisball);

            //Act
            ballService.UpdateBowler(Player);

            //Assert
            Player.Should().NotBeNull();
            Player.RunsGiven.Should().Be(runsgiven + thisball.RunsScored + thisball.Wide + thisball.NoBall);
            Player.BallsBowled.Should().Be(ballsBowled);
            Player.Wickets.Should().Be(wickets);
            Player.Wides.Should().Be(wides + 1);
            Player.NoBalls.Should().Be(noballs);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateBowlerThisBall_UndoWide()
        {
            //Arrange
            var thisball = new Ball
            {
                ActiveBowlerId = "BowlerId",
                RunsScored = 0,
                Wide = 1,
                Byes = 1,
                LegByes = 1
            };
            var howout = Player.HowOut;
            var runsgiven = Player.RunsGiven;
            var ballsBowled = Player.BallsBowled;
            var wickets = Player.Wickets;
            var wides = Player.Wides;
            var noballs = Player.NoBalls;
            var ballService = new BallService(thisball, true);

            //Act
            ballService.UpdateBowler(Player);

            //Assert
            Player.Should().NotBeNull();
            Player.RunsGiven.Should().Be(runsgiven - thisball.RunsScored - thisball.Wide - thisball.NoBall);
            Player.BallsBowled.Should().Be(ballsBowled);
            Player.Wickets.Should().Be(wickets);
            Player.Wides.Should().Be(wides - 1);
            Player.NoBalls.Should().Be(noballs);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateBowlerThisBall_noball()
        {
            //Arrange
            var thisball = new Ball
            {
                ActiveBowlerId = "BowlerId",
                RunsScored = 2,
                NoBall = 1,
                Byes = 1,
                LegByes = 1
            };
            var runsgiven = Player.RunsGiven;
            var ballsBowled = Player.BallsBowled;
            var wickets = Player.Wickets;
            var wides = Player.Wides;
            var noballs = Player.NoBalls;
            var ballService = new BallService(thisball);

            //Act
            ballService.UpdateBowler(Player);

            //Assert
            Player.Should().NotBeNull();
            Player.RunsGiven.Should().Be(runsgiven + thisball.RunsScored + thisball.Wide + thisball.NoBall);
            Player.BallsBowled.Should().Be(ballsBowled);
            Player.Wickets.Should().Be(wickets);
            Player.Wides.Should().Be(wides);
            Player.NoBalls.Should().Be(noballs + 1);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateBowlerThisBall_UndoNoball()
        {
            //Arrange
            var thisball = new Ball
            {
                ActiveBowlerId = "BowlerId",
                RunsScored = 2,
                NoBall = 1,
                Byes = 1,
                LegByes = 1
            };
            var runsgiven = Player.RunsGiven;
            var ballsBowled = Player.BallsBowled;
            var wickets = Player.Wickets;
            var wides = Player.Wides;
            var noballs = Player.NoBalls;
            var ballService = new BallService(thisball, true);

            //Act
            ballService.UpdateBowler(Player);

            //Assert
            Player.Should().NotBeNull();
            Player.RunsGiven.Should().Be(runsgiven - thisball.RunsScored - thisball.Wide - thisball.NoBall);
            Player.BallsBowled.Should().Be(ballsBowled);
            Player.Wickets.Should().Be(wickets);
            Player.Wides.Should().Be(wides);
            Player.NoBalls.Should().Be(noballs - 1);
        }

        #endregion UpdateBowlerThisBall

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Balls collection not found")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetMaidenValue_NullBallsCollection()
        {
            //Arrange
            var ballService = new BallService(new Ball());

            //Act
            var val = ballService.GetMaidenValue(null);

            //Assert
            val.Should().Be(0);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetMaidenValue_LessThanOver_Positive()
        {
            //Arrange
            var ballscollection = new List<Ball>
            {
                new Ball{ActiveBowlerId = "BowlerId", RunsScored = 1, Wide = 0, NoBall = 0}
            };
            var ballService = new BallService(new Ball());

            //Act
            var val = ballService.GetMaidenValue(ballscollection);

            //Assert
            val.Should().Be(0);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetMaidenValue_OverWithRuns()
        {
            //Arrange
            var ballscollection = new List<Ball>
                {
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 1, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 1, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 1, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 1, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 1, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 1, Wide = 0, NoBall = 0},
                };
            var ballService = new BallService(new Ball());

            //Act
            var val = ballService.GetMaidenValue(ballscollection);

            //Assert
            val.Should().Be(0);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetMaidenValue_OverWithoutRuns()
        {
            //Arrange
            var ballscollection = new List<Ball>
                {
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                };
            var ballService = new BallService(new Ball());

            //Act
            var val = ballService.GetMaidenValue(ballscollection);

            //Assert
            val.Should().Be(1);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetMaidenValue_OverWithWide()
        {
            //Arrange
            var ballscollection = new List<Ball>
                {
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 1, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                };
            var ballService = new BallService(new Ball());

            //Act
            var val = ballService.GetMaidenValue(ballscollection);

            //Assert
            val.Should().Be(0);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetMaidenValue_OverWithNoBall()
        {
            //Arrange
            var ballscollection = new List<Ball>
                {
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 1},
                };
            var ballService = new BallService(new Ball());

            //Act
            var val = ballService.GetMaidenValue(ballscollection);

            //Assert
            val.Should().Be(0);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetMaidenValue_MoreThanAnOver_RunInRecentOver()
        {
            //Arrange
            var ballscollection = new List<Ball>
                {
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 1, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                };
            var ballService = new BallService(new Ball());

            //Act
            var val = ballService.GetMaidenValue(ballscollection);

            //Assert
            val.Should().Be(0);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetMaidenValue_MoreThanAnOver_NoRunInRecentOver()
        {
            //Arrange
            var ballscollection = new List<Ball>
                {
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 1, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                };
            var ballService = new BallService(new Ball());

            //Act
            var val = ballService.GetMaidenValue(ballscollection);

            //Assert
            val.Should().Be(1);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetMaidenValue_MoreThanAnOver_InCompleteLastOver()
        {
            //Arrange
            var ballscollection = new List<Ball>
                {
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 1, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},

                };
            var ballService = new BallService(new Ball());

            //Act
            var val = ballService.GetMaidenValue(ballscollection);

            //Assert
            val.Should().Be(0);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetMaidenValue_MoreThanAnOver_Undo_InCompleteLastOver()
        {
            //Arrange
            var ballscollection = new List<Ball>
                {
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 1, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},

                };
            var ballService = new BallService(new Ball(), true);

            //Act
            var val = ballService.GetMaidenValue(ballscollection);

            //Assert
            val.Should().Be(0);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetMaidenValue_MoreThanAnOver_Undo_NoRunInRecentOver()
        {
            //Arrange
            var ballscollection = new List<Ball>
                {
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 1, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                    new Ball{ActiveBowlerId = "BowlerId", RunsScored = 0, Wide = 0, NoBall = 0},
                };
            var ballService = new BallService(new Ball(), true);

            //Act
            var val = ballService.GetMaidenValue(ballscollection);

            //Assert
            val.Should().Be(-1);
        }

    }
}
