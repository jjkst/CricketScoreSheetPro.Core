using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CricketScoreSheetPro.Core.Services;
using FluentAssertions;
using CricketScoreSheetPro.Core.Models;
using Moq;
using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Test.UnitTest.ServiceTest
{
    [TestClass]
    public class PlayerServiceTest
    {
        private Player _player { get; set; }

        public PlayerServiceTest()
        {
            _player = new Player
            {
                Name = "JK",
                Roles = new List<string> { "Batsman", "Bowler" },
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

        #region UpdateBatsmanThisBall

        [ExpectedException(typeof(ArgumentNullException), "Ball not found")]
        [TestMethod]
        [TestCategory("UnitTest")]
        public void WhenThisBall_Null_Negative()
        {
            //Arrange
            var playerService = new PlayerService(null);

            //Act
            var val = playerService.UpdateBatsmanThisBall(new Player());

            //Assert
            val.Should().NotBeNull();
        }

        [ExpectedException(typeof(ArgumentNullException), "Batsman not found")]
        [TestMethod]
        [TestCategory("UnitTest")]
        public void UpdateBatsmanThisBall_Null_Negative()
        {
            //Arrange
            var playerService = new PlayerService(new Ball());

            //Act
            var val = playerService.UpdateBatsmanThisBall(null);

            //Assert
            val.Should().NotBeNull();
        }

        [ExpectedException(typeof(ArgumentException), "Batsman haven't played any ball")]
        [TestMethod]
        [TestCategory("UnitTest")]
        public void UpdateBatsmanThisBall_Undo_ZeroBalls_Negative()
        {
            //Arrange
            var playerService = new PlayerService(new Ball(), true);
            
            //Act
            var val = playerService.UpdateBatsmanThisBall(new Player());

            //Assert
            val.Should().NotBeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void UpdateBatsmanThisBall_Add_Positive()
        {
            //Arrange
            var thisball = new Ball
            {
                ActiveBatsmanId = "BatsmanId",
                HowOut = "Bowled",
                RunsScored = 4
            };
            var howout = _player.HowOut;
            var runstaken = _player.RunsTaken;
            var ballsplayed = _player.BallsPlayed;
            var fours = _player.Fours;
            var sixes = _player.Sixes;
            var playerService = new PlayerService(thisball);

            //Act
            playerService.UpdateBatsmanThisBall(_player);

            //Assert
            _player.Should().NotBeNull();
            _player.HowOut.Should().Be(thisball.HowOut);
            _player.RunsTaken.Should().Be(runstaken + thisball.RunsScored);
            _player.BallsPlayed.Should().Be(ballsplayed + 1);
            _player.Fours.Should().Be(fours + 1);
            _player.Sixes.Should().Be(sixes);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void UpdateBatsmanThisBall_AddWhenWide_Positive()
        {
            //Arrange
            var thisball = new Ball
            {
                ActiveBatsmanId = "BatsmanId",
                RunsScored = 6,
                Wide = 1
            };
            var runstaken = _player.RunsTaken;
            var ballsplayed = _player.BallsPlayed;
            var fours = _player.Fours;
            var sixes = _player.Sixes;
            var playerService = new PlayerService(thisball);

            //Act
            playerService.UpdateBatsmanThisBall(_player);

            //Assert
            _player.Should().NotBeNull();
            _player.HowOut.Should().Be("not out");
            _player.RunsTaken.Should().Be(runstaken + thisball.RunsScored);
            _player.BallsPlayed.Should().Be(ballsplayed);
            _player.Fours.Should().Be(fours);
            _player.Sixes.Should().Be(sixes + 1);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void UpdateBatsmanThisBall_Undo_Positive()
        {
            //Arrange
            var thisball = new Ball
            {
                ActiveBatsmanId = "BatsmanId",
                HowOut = "Bowled",
                RunsScored = 1
            };
            var runstaken = _player.RunsTaken;
            var ballsplayed = _player.BallsPlayed;
            var fours = _player.Fours;
            var sixes = _player.Sixes;
            var playerService = new PlayerService(thisball, true);

            //Act
            playerService.UpdateBatsmanThisBall(_player);

            //Assert
            _player.Should().NotBeNull();
            _player.HowOut.Should().Be("not out");
            _player.RunsTaken.Should().Be(runstaken - thisball.RunsScored);
            _player.BallsPlayed.Should().Be(ballsplayed - 1);
            _player.Fours.Should().Be(fours);
            _player.Sixes.Should().Be(sixes);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void UpdateBatsmanThisBall_UndoWhenWide_Positive()
        {
            //Arrange
            var thisball = new Ball
            {
                ActiveBatsmanId = "BatsmanId",
                RunsScored = 1,
                Wide = 1
            };
            var runstaken = _player.RunsTaken;
            var ballsplayed = _player.BallsPlayed;
            var fours = _player.Fours;
            var sixes = _player.Sixes;
            var playerService = new PlayerService(thisball, true);

            //Act
            playerService.UpdateBatsmanThisBall(_player);

            //Assert
            _player.Should().NotBeNull();
            _player.HowOut.Should().Be("not out");
            _player.RunsTaken.Should().Be(runstaken - thisball.RunsScored);
            _player.BallsPlayed.Should().Be(ballsplayed);
            _player.Fours.Should().Be(fours);
            _player.Sixes.Should().Be(sixes);
        }

        #endregion UpdateBatsmanThisBall

        #region UpdateRunnerThisBall

        [ExpectedException(typeof(ArgumentNullException), "Runner not found")]
        [TestMethod]
        [TestCategory("UnitTest")]
        public void UpdateRunnerThisBall_Null_Negative()
        {
            //Arrange
            var playerService = new PlayerService(new Ball());

            //Act
            var val = playerService.UpdateRunnerThisBall(null);

            //Assert
            val.Should().NotBeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void UpdateRunnerThisBall_NoChange_Positive()
        {
            //Arrange
            var thisball = new Ball
            {
                RunnerBatsmanId = "RunnerId",
            };
            var runnerout = _player.HowOut;
            var playerService = new PlayerService(thisball);

            //Act
            playerService.UpdateRunnerThisBall(_player);

            //Assert
            _player.HowOut.Should().Be(runnerout);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void UpdateRunnerThisBall_Out_Positive()
        {
            //Arrange
            var thisball = new Ball
            {
                RunnerBatsmanId = "RunnerId",
                RunnerHowOut = "run out"
            };
            var runnerout = _player.HowOut;
            var playerService = new PlayerService(thisball);

            //Act
            playerService.UpdateRunnerThisBall(_player);

            //Assert
            _player.HowOut.Should().Be(thisball.RunnerHowOut);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void UpdateRunnerThisBall_UndoOut_Positive()
        {
            //Arrange
            var thisball = new Ball
            {
                RunnerBatsmanId = "RunnerId",
                RunnerHowOut = "run out"
            };
            var runnerout = _player.HowOut;
            var playerService = new PlayerService(thisball, true);

            //Act
            playerService.UpdateRunnerThisBall(_player);

            //Assert
            _player.HowOut.Should().Be(runnerout);
        }

        #endregion UpdateRunnerThisBall

        #region UpdateFielderThisBall

        [ExpectedException(typeof(ArgumentNullException), "Fielder not found")]
        [TestMethod]
        [TestCategory("UnitTest")]
        public void UpdateFielderThisBall_Null_Negative()
        {
            //Arrange
            var playerService = new PlayerService(new Ball());

            //Act
            var val = playerService.UpdateFielderThisBall(null);

            //Assert
            val.Should().NotBeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void UpdateFielderThisBall_Catch_Positive()
        {
            //Arrange
            var thisball = new Ball
            {
                FielderId = "fielderId",
                HowOut = $"c {_player.Name}"
            };
            var catches = _player.Catches;
            var playerService = new PlayerService(thisball);

            //Act
            playerService.UpdateFielderThisBall(_player);

            //Assert
            _player.Catches.Should().Be(catches + 1);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void UpdateFielderThisBall_UndoCatch_Positive()
        {
            //Arrange
            var thisball = new Ball
            {
                FielderId = "fielderId",
                HowOut = $"c {_player.Name}"
            };
            var catches = _player.Catches;
            var playerService = new PlayerService(thisball, true);

            //Act
            playerService.UpdateFielderThisBall(_player);

            //Assert
            _player.Catches.Should().Be(catches - 1);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void UpdateFielderThisBall_UndoCatchWhenZero_Positive()
        {
            //Arrange
            var thisball = new Ball
            {
                FielderId = "fielderId",
                HowOut = $"c {_player.Name}"
            };
            _player.Catches = 0;
            var playerService = new PlayerService(thisball, true);

            //Act
            playerService.UpdateFielderThisBall(_player);

            //Assert
            _player.Catches.Should().Be(0);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void UpdateFielderThisBall_Stumpings_Positive()
        {
            //Arrange
            var thisball = new Ball
            {
                FielderId = "fielderId",
                HowOut = $"st †{_player.Name}"
            };
            var stumpings = _player.Stumpings;
            var playerService = new PlayerService(thisball);

            //Act
            playerService.UpdateFielderThisBall(_player);

            //Assert
            _player.Stumpings.Should().Be(stumpings + 1);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void UpdateFielderThisBall_UndoStumping_Positive()
        {
            //Arrange
            var thisball = new Ball
            {
                FielderId = "fielderId",
                HowOut = $"st †{_player.Name}"
            };
            var stumping = _player.Stumpings;
            var playerService = new PlayerService(thisball, true);

            //Act
            playerService.UpdateFielderThisBall(_player);

            //Assert
            _player.Stumpings.Should().Be(stumping - 1);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void UpdateFielderThisBall_UndoStumpingWhenZero_Positive()
        {
            //Arrange
            var thisball = new Ball
            {
                FielderId = "fielderId",
                HowOut = $"st †{_player.Name}"
            };
            _player.Stumpings = 0;
            var playerService = new PlayerService(thisball, true);

            //Act
            playerService.UpdateFielderThisBall(_player);

            //Assert
            _player.Stumpings.Should().Be(0);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void UpdateFielderThisBall_Runouts_Positive()
        {
            //Arrange
            var thisball = new Ball
            {
                FielderId = "fielderId",
                HowOut = $"runout {_player.Name}"
            };
            var runouts = _player.RunOuts;
            var playerService = new PlayerService(thisball);

            //Act
            playerService.UpdateFielderThisBall(_player);

            //Assert
            _player.RunOuts.Should().Be(runouts + 1);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void UpdateFielderThisBall_UndoRunouts_Positive()
        {
            //Arrange
            var thisball = new Ball
            {
                FielderId = "fielderId",
                HowOut = $"runout {_player.Name}"
            };
            var runouts = _player.RunOuts;
            var playerService = new PlayerService(thisball, true);

            //Act
            playerService.UpdateFielderThisBall(_player);

            //Assert
            _player.RunOuts.Should().Be(runouts - 1);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void UpdateFielderThisBall_UndoRunoutsWhenZero_Positive()
        {
            //Arrange
            var thisball = new Ball
            {
                FielderId = "fielderId",
                HowOut = $"runout {_player.Name}"
            };
            _player.RunOuts = 0;
            var playerService = new PlayerService(thisball, true);

            //Act
            playerService.UpdateFielderThisBall(_player);

            //Assert
            _player.RunOuts.Should().Be(0);
        }

        #endregion UpdateFielderThisBall
    }
}
