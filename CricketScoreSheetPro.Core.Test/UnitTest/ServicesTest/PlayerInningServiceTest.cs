using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Repositories.Interfaces;
using CricketScoreSheetPro.Core.Services.Implementations;
using CricketScoreSheetPro.Core.Test.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Test.UnitTest.ServicesTest
{
    [TestClass]
    public class PlayerInningServiceTest
    {

        private static PlayerInning PlayerInning { get; set; }
        private static PlayerInningService PlayerInningService { get; set; }

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            //Arrange
            PlayerInning = new PlayerInning { PlayerName = "PlayerName", TeamId = "TeamId"};            
            var players = new List<PlayerInning> { PlayerInning };

            var mockPlayerRepo = new Mock<IRepository<PlayerInning>>();
            mockPlayerRepo.Setup(x => x.Create(It.IsAny<PlayerInning>())).Returns("PlayerInningId");
            mockPlayerRepo.Setup(x => x.GetFilteredList(It.IsAny<string>(), It.IsAny<string>())).Returns(players);
            mockPlayerRepo.Setup(x => x.GetItem(It.IsAny<string>())).Returns(PlayerInning);

            PlayerInningService = new PlayerInningService(mockPlayerRepo.Object);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "PlayerInningRepository is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void PlayerInningService_NullTeamPlayerRepository()
        {
            //Act
            var playerService = new PlayerInningService(null);

            //Assert
            playerService.Should().BeNull();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "PlayerInning is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void AddPlayerInnings_Null()
        {
            //Act            
            var val = PlayerInningService.AddPlayerInnings(null);

            //Assert
            val.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void AddPlayerInnings()
        {
            //Act            
            var val = PlayerInningService.AddPlayerInnings(PlayerInning);

            //Assert
            val.Should().NotBeNull();
            val.Should().BeEquivalentTo("PlayerInningId");
        }
       
        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdatePlayerInning()
        {
            //Act
            PlayerInningService.UpdatePlayerInning("PID", PlayerInning);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "PlayerInning ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdatePlayerInning_EmptyPlayerId()
        {
            //Act
            PlayerInningService.UpdatePlayerInning("", PlayerInning);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "PlayerInning is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdatePlayerInning_Null()
        {
            //Act
            PlayerInningService.UpdatePlayerInning("PID", null);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetPlayerInning()
        {
            //Act
            var val = PlayerInningService.GetPlayerInning("PID");

            //Assert
            val.Should().BeEquivalentTo(PlayerInning);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "PlayerInning ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetPlayerInning_EmptyPlayerId()
        {
            //Act
            PlayerInningService.GetPlayerInning("");
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetPlayerInnings()
        {
            //Act
            var val = PlayerInningService.GetPlayerInnings("playerId");

            //Assert
            val.Should().BeEquivalentTo(new List<PlayerInning> { PlayerInning });
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "PlayerID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetPlayerInnings_EmptyPlayerId()
        {
            //Act
            PlayerInningService.GetPlayerInnings(null);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetPlayerInningsByTournamentId()
        {
            //Act
            var val = PlayerInningService.GetPlayerInningsByTournamentId("playerId","tournamentId");

            //Assert
            val.Should().BeEquivalentTo(new List<PlayerInning> { PlayerInning });
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "PlayerID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetPlayerInningsByTournamentId_EmptyPlayerId()
        {
            //Act
            var val = PlayerInningService.GetPlayerInningsByTournamentId("", "tournamentId");

            //Assert
            val.Should().BeEquivalentTo(new List<PlayerInning> { PlayerInning });
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "TournamentId is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetPlayerInningsByTournamentId_EmptyTournamentId()
        {
            //Act
            var val = PlayerInningService.GetPlayerInningsByTournamentId("playerId", "");

            //Assert
            val.Should().BeEquivalentTo(new List<PlayerInning> { PlayerInning });
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetAllPlayerInningsByTeamMatchId()
        {
            //Act
            var val = PlayerInningService.GetAllPlayerInningsByTeamMatchId("playerId", "tournamentId");

            //Assert
            val.Should().BeEquivalentTo(new List<PlayerInning> { PlayerInning });
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "TeamID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetAllPlayerInningsByTeamMatchId_EmptyTeamId()
        {
            //Act
            var val = PlayerInningService.GetAllPlayerInningsByTeamMatchId("", "matchId");

            //Assert
            val.Should().BeEquivalentTo(new List<PlayerInning> { PlayerInning });
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "MatchId is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetAllPlayerInningsByTeamMatchId_EmptyMatchId()
        {
            //Act
            var val = PlayerInningService.GetAllPlayerInningsByTeamMatchId("teamId", "");

            //Assert
            val.Should().BeEquivalentTo(new List<PlayerInning> { PlayerInning });
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void DeleteAllPlayerInnings()
        {
            //Act
            PlayerInningService.DeleteAllPlayerInnings();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void DeletePlayer()
        {
            //Act
            PlayerInningService.DeletePlayerInning("PID");
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "PlayerInning ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void DeletePlayer_EmptyPlayerId()
        {
            //Act
            PlayerInningService.DeletePlayerInning("");
        }
    }
}
