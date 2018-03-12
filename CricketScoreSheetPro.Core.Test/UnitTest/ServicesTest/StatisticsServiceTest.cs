using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CricketScoreSheetPro.Core.Models;
using System.Collections.Generic;
using CricketScoreSheetPro.Core.Services.Implementations;
using FluentAssertions;
using CricketScoreSheetPro.Core.Test.Extensions;

namespace CricketScoreSheetPro.Core.Test.UnitTest.ServicesTest
{
    [TestClass]
    public class StatisticsServiceTest
    {

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "Null player inning passed to calculate statistics.")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetPlayerStatistics_Null()
        {
            //Arrange 
            var stats = new StatisticsService();

            //Act
            var playerStats = stats.GetPlayerStatistics(null);

            //Assert
            playerStats.Should().BeNull();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "player innings list is empty")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetPlayerStatistics_EmptyList()
        {
            //Arrange 
            var stats = new StatisticsService();

            //Act
            var playerStats = stats.GetPlayerStatistics(new List<PlayerInning>());

            //Assert
            playerStats.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetPlayerStatistics()
        {
            //Arrange 
            var stats = new StatisticsService();
            var playerInnings = new List<PlayerInning>
            {
                new PlayerInning{BallsBowled = 18, BallsPlayed = 50, Catches = 5, Fours = 6, HowOut = "bowled", Maiden = 1, NoBalls = 4, PlayerName = "JK", RunsGiven = 20,
                 RunOuts = 2, RunsTaken = 40, Sixes = 2, Stumpings = 2, Wickets = 1, Wides = 3},
                new PlayerInning{BallsBowled = 18, BallsPlayed = 0, Catches = 5, Fours = 0, HowOut = "not out", Maiden = 0, NoBalls = 4, PlayerName = "JK", RunsGiven = 20,
                 RunOuts = 2, RunsTaken = 0, Sixes = 0, Stumpings = 2, Wickets = 3, Wides = 3},
                new PlayerInning{BallsBowled = 18, BallsPlayed = 50, Catches = 5, Fours = 6, HowOut = "run out", Maiden = 0, NoBalls = 4, PlayerName = "JK", RunsGiven = 21,
                 RunOuts = 2, RunsTaken = 40, Sixes = 2, Stumpings = 2, Wickets = 3, Wides = 3},
                new PlayerInning{BallsBowled = 18, BallsPlayed = 50, Catches = 5, Fours = 6, HowOut = "lbw", Maiden = 1, NoBalls = 4, PlayerName = "JK", RunsGiven = 20,
                 RunOuts = 2, RunsTaken = 40, Sixes = 2, Stumpings = 2, Wickets = 1, Wides = 3},
                new PlayerInning{BallsBowled = 18, BallsPlayed = 50, Catches = 5, Fours = 6, HowOut = "c player b player2", Maiden = 1, NoBalls = 4, PlayerName = "JK", RunsGiven = 20,
                 RunOuts = 2, RunsTaken = 99, Sixes = 2, Stumpings = 2, Wickets = 1, Wides = 3},
            };

            //Act
            var playerStats = stats.GetPlayerStatistics(playerInnings);

            //Assert
            playerStats.Should().NotBeNull();
            playerStats.Balls.Should().Be(200);
            playerStats.BallsBowled.Should().Be(90);
            playerStats.BattingAvg.Should().Be(Math.Round((decimal)219/4, 2));
            playerStats.BB.Should().Be("3/20");
            playerStats.BowlingAvg.Should().Be(Math.Round((decimal)101 /9, 2));
            playerStats.BowlingSR.Should().Be(Math.Round((decimal)90 /9,2));
            playerStats.Catches.Should().Be(25);
            playerStats.Econ.Should().Be(Math.Round((decimal)101/15, 2));
            playerStats.Fifties.Should().Be(1);
            playerStats.FWI.Should().Be(0);
            playerStats.HS.Should().Be(99);
            playerStats.Hundreds.Should().Be(0);
            playerStats.Innings.Should().Be(4);
            playerStats.Maiden.Should().Be(3);
            playerStats.Matches.Should().Be(5);
            playerStats.NotOuts.Should().Be(0);
            playerStats.PlayerName.Should().Be("JK");
            playerStats.Runs.Should().Be(219);
            playerStats.RunsGiven.Should().Be(101);
            playerStats.SR.Should().Be(Math.Round((decimal)219/200*100, 2));
            playerStats.Stumpings.Should().Be(10);
            playerStats.TWI.Should().Be(0);
            playerStats.Wickets.Should().Be(9);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetPlayerStatistics_FiveAndTenWI()
        {
            //Arrange 
            var stats = new StatisticsService();
            var playerInnings = new List<PlayerInning>
            {
                new PlayerInning{BallsBowled = 18, BallsPlayed = 50, Catches = 5, Fours = 6, HowOut = "bowled", Maiden = 1, NoBalls = 4, PlayerName = "JK", RunsGiven = 20,
                 RunOuts = 2, RunsTaken = 40, Sixes = 2, Stumpings = 2, Wickets = 5, Wides = 3},
                new PlayerInning{BallsBowled = 18, BallsPlayed = 50, Catches = 5, Fours = 6, HowOut = "not out", Maiden = 0, NoBalls = 4, PlayerName = "JK", RunsGiven = 21,
                 RunOuts = 2, RunsTaken = 40, Sixes = 2, Stumpings = 2, Wickets = 3, Wides = 3},
                new PlayerInning{BallsBowled = 18, BallsPlayed = 50, Catches = 5, Fours = 6, HowOut = "lbw", Maiden = 1, NoBalls = 4, PlayerName = "JK", RunsGiven = 20,
                 RunOuts = 2, RunsTaken = 40, Sixes = 2, Stumpings = 2, Wickets = 1, Wides = 3},
                new PlayerInning{BallsBowled = 18, BallsPlayed = 50, Catches = 5, Fours = 6, HowOut = "c player b player2", Maiden = 1, NoBalls = 4, PlayerName = "JK", RunsGiven = 20,
                 RunOuts = 2, RunsTaken = 100, Sixes = 2, Stumpings = 2, Wickets = 10, Wides = 3},
            };

            //Act
            var playerStats = stats.GetPlayerStatistics(playerInnings);

            //Assert
            playerStats.Should().NotBeNull();
            playerStats.Balls.Should().Be(200);
            playerStats.BallsBowled.Should().Be(72);
            playerStats.BattingAvg.Should().Be(Math.Round((decimal)220/3, 2));
            playerStats.BB.Should().Be("10/20");
            playerStats.BowlingAvg.Should().Be(Math.Round((decimal)81/19, 2));
            playerStats.BowlingSR.Should().Be(Math.Round((decimal)72/19, 2));
            playerStats.Catches.Should().Be(20);
            playerStats.Econ.Should().Be(Math.Round((decimal)81/12, 2));
            playerStats.Fifties.Should().Be(0);
            playerStats.FWI.Should().Be(1);
            playerStats.HS.Should().Be(100);
            playerStats.Hundreds.Should().Be(1);
            playerStats.Innings.Should().Be(4);
            playerStats.Maiden.Should().Be(3);
            playerStats.Matches.Should().Be(4);
            playerStats.NotOuts.Should().Be(1);
            playerStats.PlayerName.Should().Be("JK");
            playerStats.Runs.Should().Be(220);
            playerStats.RunsGiven.Should().Be(81);
            playerStats.SR.Should().Be(Math.Round((decimal)220 / 200 * 100, 2));
            playerStats.Stumpings.Should().Be(8);
            playerStats.TWI.Should().Be(1);
            playerStats.Wickets.Should().Be(19);
        }
    }
}
