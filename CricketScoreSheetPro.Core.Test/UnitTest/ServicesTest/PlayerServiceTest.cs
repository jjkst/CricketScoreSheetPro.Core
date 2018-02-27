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
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Test.UnitTest.ServicesTest
{
    [TestClass]
    public class PlayerServiceTest
    {

        private static Player Player { get; set; }
        private static PlayerService PlayerService { get; set; }

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            //Arrange
            Player = new Player { Name = "PlayerName" };            
            var players = new List<Player> { Player };
            var mockPlayerRepo = new Mock<IRepository<Player>>();
            mockPlayerRepo.Setup(x => x.CreateAsync(It.IsAny<Player>())).ReturnsAsync(Player);
            mockPlayerRepo.Setup(x => x.CreateWithIdAsync(It.IsAny<string>(), It.IsAny<Player>())).Returns(Task.FromResult(0));            
            mockPlayerRepo.Setup(x => x.GetFilteredListAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(players);
            mockPlayerRepo.Setup(x => x.GetItemAsync(It.IsAny<string>())).ReturnsAsync(Player);
            mockPlayerRepo.Setup(x => x.DeleteAsync()).Returns(Task.FromResult(0));
            mockPlayerRepo.Setup(x => x.DeleteByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(0));
            var mockTeamPlayerRepo = new Mock<IRepository<TeamPlayer>>();
            mockTeamPlayerRepo.Setup(x => x.CreateWithIdAsync(It.IsAny<string>(), It.IsAny<TeamPlayer>())).Returns(Task.FromResult(0));
            mockTeamPlayerRepo.Setup(x => x.GetListAsync()).ReturnsAsync(new List<TeamPlayer> { new TeamPlayer {PlayerName = Player.Name } });
            PlayerService = new PlayerService(mockPlayerRepo.Object, mockTeamPlayerRepo.Object);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "PlayerRepository is null")]
        [TestCategory("UnitTest")]
        public void MatchService_NullPlayerRepository()
        {
            //Act
            var playerService = new PlayerService(null, (new Mock<IRepository<TeamPlayer>>()).Object);

            //Assert
            playerService.Should().BeNull();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "TeamPlayerRepository is null")]
        [TestCategory("UnitTest")]
        public void PlayerService_NullTeamPlayerRepository()
        {
            //Act
            var playerService = new PlayerService((new Mock<IRepository<Player>>()).Object, null);

            //Assert
            playerService.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void AddPlayerAsync()
        {
            //Act            
            var val = PlayerService.AddPlayerAsync(Player);

            //Assert
            val.Result.Should().NotBeNull();
            val.Result.Should().BeEquivalentTo(Player);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Player is null")]
        [TestCategory("UnitTest")]
        public void AddPlayerAsync_Null()
        {
            //Act
            var val = PlayerService.AddPlayerAsync(null);

            //Assert
            val.Result.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void UpdatePlayerAsync()
        {
            //Act
            var val = PlayerService.UpdatePlayerAsync("PID", Player);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Player ID is null")]
        [TestCategory("UnitTest")]
        public void UpdatePlayerAsync_EmptyPlayerId()
        {
            //Act
            var val = PlayerService.UpdatePlayerAsync("", Player);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Player is null")]
        [TestCategory("UnitTest")]
        public void UpdatePlayerAsync_Null()
        {
            //Act
            var val = PlayerService.UpdatePlayerAsync("PID", null);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void GetPlayerAsync()
        {
            //Act
            var val = PlayerService.GetPlayerAsync("PID");

            //Assert
            val.Result.Should().BeEquivalentTo(Player);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Player ID is null")]
        [TestCategory("UnitTest")]
        public void GetPlayerAsync_EmptyPlayerId()
        {
            //Act
            var val = PlayerService.GetPlayerAsync("");

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void GetPlayersPerTeamAsync()
        {
            //Act
            var val = PlayerService.GetPlayersPerTeamAsync();

            //Assert
            val.Result.Should().BeEquivalentTo(new List<TeamPlayer> { new TeamPlayer { PlayerName = "PlayerName" } });
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "UserTeamRepo is null")]
        [TestCategory("UnitTest")]
        public void GetPlayersForAllUserTeamsAsync_NullUserTeamRepo()
        {
            //Act
            var val = PlayerService.GetPlayersForAllUserTeamsAsync(null);

            //Assert
            val.Result.Should().BeEquivalentTo(Player);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void GetPlayersForAllUserTeamsAsync()
        {
            //Arrange
            var mockUserTeamRepo = new Mock<IRepository<UserTeam>>();
            mockUserTeamRepo.Setup(x => x.GetListAsync()).ReturnsAsync(new List<UserTeam> { new UserTeam { TeamName = "Chennai Super Kings" } });

            //Act
            var val = PlayerService.GetPlayersForAllUserTeamsAsync(mockUserTeamRepo.Object);

            //Assert
            val.Result.Should().BeEquivalentTo(Player);
        }       


        [TestMethod]
        [TestCategory("UnitTest")]
        public void DeleteAllPlayersAsync()
        {
            //Act
            var val = PlayerService.DeleteAllPlayersAsync();

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void DeletePlayerAsync()
        {
            //Act
            var val = PlayerService.DeletePlayerAsync("PID");

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Player ID is null")]
        [TestCategory("UnitTest")]
        public void DeletePlayerAsync_EmptyMatchId()
        {
            //Act
            var val = PlayerService.DeletePlayerAsync("");

            //Assert
            val.Wait();
        }
    }
}
