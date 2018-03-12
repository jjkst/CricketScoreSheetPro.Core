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
            mockPlayerRepo.Setup(x => x.CreateAsync(It.IsAny<PlayerInning>())).ReturnsAsync(PlayerInning);
            mockPlayerRepo.Setup(x => x.CreateWithIdAsync(It.IsAny<string>(), It.IsAny<PlayerInning>())).Returns(Task.FromResult(0));            
            mockPlayerRepo.Setup(x => x.GetFilteredListAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(players);
            mockPlayerRepo.Setup(x => x.GetItemAsync(It.IsAny<string>())).ReturnsAsync(PlayerInning);
            mockPlayerRepo.Setup(x => x.DeleteAsync()).Returns(Task.FromResult(0));
            mockPlayerRepo.Setup(x => x.DeleteByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(0));

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
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void AddPlayerInningsAsync()
        {
            //Act            
            var val = PlayerInningService.AddPlayerInningsAsync(PlayerInning);

            //Assert
            val.Result.Should().NotBeNull();
            val.Result.Should().BeEquivalentTo(PlayerInning);
        }
       
        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdatePlayerInningAsync()
        {
            //Act
            var val = PlayerInningService.UpdatePlayerInningAsync("PID", PlayerInning);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "PlayerInning ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdatePlayerInningAsync_EmptyPlayerId()
        {
            //Act
            var val = PlayerInningService.UpdatePlayerInningAsync("", PlayerInning);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "PlayerInning is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdatePlayerInningAsync_Null()
        {
            //Act
            var val = PlayerInningService.UpdatePlayerInningAsync("PID", null);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetPlayerInningAsync()
        {
            //Act
            var val = PlayerInningService.GetPlayerInningAsync("PID");

            //Assert
            val.Result.Should().BeEquivalentTo(PlayerInning);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "PlayerInning ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetPlayerInningAsync_EmptyPlayerId()
        {
            //Act
            var val = PlayerInningService.GetPlayerInningAsync("");

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetPlayerInningsAsync()
        {
            //Act
            var val = PlayerInningService.GetPlayerInningsAsync("playerId");

            //Assert
            val.Result.Should().BeEquivalentTo(new List<PlayerInning> { PlayerInning });
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "PlayerID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetPlayerInningsAsync_EmptyPlayerId()
        {
            //Act
            var val = PlayerInningService.GetPlayerInningsAsync(null);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetPlayerInningsByTournamentIdAsync()
        {
            //Act
            var val = PlayerInningService.GetPlayerInningsByTournamentIdAsync("playerId","tournamentId");

            //Assert
            val.Result.Should().BeEquivalentTo(new List<PlayerInning> { PlayerInning });
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "PlayerID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetPlayerInningsByTournamentIdAsync_EmptyPlayerId()
        {
            //Act
            var val = PlayerInningService.GetPlayerInningsByTournamentIdAsync("", "tournamentId");

            //Assert
            val.Result.Should().BeEquivalentTo(new List<PlayerInning> { PlayerInning });
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "TournamentId is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetPlayerInningsByTournamentIdAsync_EmptyTournamentId()
        {
            //Act
            var val = PlayerInningService.GetPlayerInningsByTournamentIdAsync("playerId", "");

            //Assert
            val.Result.Should().BeEquivalentTo(new List<PlayerInning> { PlayerInning });
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetAllPlayerInningsByTeamMatchIdAsync()
        {
            //Act
            var val = PlayerInningService.GetAllPlayerInningsByTeamMatchIdAsync("playerId", "tournamentId");

            //Assert
            val.Result.Should().BeEquivalentTo(new List<PlayerInning> { PlayerInning });
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "TeamID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetAllPlayerInningsByTeamMatchIdAsync_EmptyTeamId()
        {
            //Act
            var val = PlayerInningService.GetAllPlayerInningsByTeamMatchIdAsync("", "matchId");

            //Assert
            val.Result.Should().BeEquivalentTo(new List<PlayerInning> { PlayerInning });
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "MatchId is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetAllPlayerInningsByTeamMatchIdAsync_EmptyMatchId()
        {
            //Act
            var val = PlayerInningService.GetAllPlayerInningsByTeamMatchIdAsync("teamId", "");

            //Assert
            val.Result.Should().BeEquivalentTo(new List<PlayerInning> { PlayerInning });
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void DeleteAllPlayerInningsAsync()
        {
            //Act
            var val = PlayerInningService.DeleteAllPlayerInningsAsync();

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void DeletePlayerAsync()
        {
            //Act
            var val = PlayerInningService.DeletePlayerInningAsync("PID");

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "PlayerInning ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void DeletePlayerAsync_EmptyPlayerId()
        {
            //Act
            var val = PlayerInningService.DeletePlayerInningAsync("");

            //Assert
            val.Wait();
        }
    }
}
