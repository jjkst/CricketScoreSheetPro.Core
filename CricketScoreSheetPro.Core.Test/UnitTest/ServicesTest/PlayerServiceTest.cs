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

        private static PlayerInning PlayerInning { get; set; }
        private static PlayerInningService PlayerInningService { get; set; }

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            //Arrange
            PlayerInning = new PlayerInning { PlayerName = "PlayerName", TeamId = "TeamId"};            
            var players = new List<PlayerInning> { PlayerInning };

            var mockTeamDetailRepo = new Mock<IRepository<TeamDetail>>();
            mockTeamDetailRepo.Setup(x => x.CreateWithIdAsync(It.IsAny<string>(), It.IsAny<TeamDetail>())).Returns(Task.FromResult(0));
            mockTeamDetailRepo.Setup(x => x.GetListAsync()).ReturnsAsync(
                new List<TeamDetail> {
                new TeamDetail {
                    Players = new List<Player>
                    {
                        new Player
                        {
                            Name = PlayerInning.PlayerName
                        }
                    }
                }});

            var mockPlayerRepo = new Mock<IRepository<PlayerInning>>();
            mockPlayerRepo.Setup(x => x.CreateAsync(It.IsAny<PlayerInning>())).ReturnsAsync(PlayerInning);
            mockPlayerRepo.Setup(x => x.CreateWithIdAsync(It.IsAny<string>(), It.IsAny<PlayerInning>())).Returns(Task.FromResult(0));            
            mockPlayerRepo.Setup(x => x.GetFilteredListAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(players);
            mockPlayerRepo.Setup(x => x.GetItemAsync(It.IsAny<string>())).ReturnsAsync(PlayerInning);
            mockPlayerRepo.Setup(x => x.DeleteAsync()).Returns(Task.FromResult(0));
            mockPlayerRepo.Setup(x => x.DeleteByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(0));

            PlayerInningService = new PlayerInningService(mockTeamDetailRepo.Object, mockPlayerRepo.Object);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "PlayerRepository is null")]
        [TestCategory("UnitTest")]
        public void PlayerService_NullPlayerRepository()
        {
            //Act
            var playerService = new PlayerInningService(null, (new Mock<IRepository<PlayerInning>>()).Object);

            //Assert
            playerService.Should().BeNull();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "TeamPlayerRepository is null")]
        [TestCategory("UnitTest")]
        public void PlayerService_NullTeamPlayerRepository()
        {
            //Act
            var playerService = new PlayerInningService((new Mock<IRepository<TeamDetail>>()).Object, null);

            //Assert
            playerService.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void AddPlayerAsync()
        {
            //Act            
            var val = PlayerInningService.AddPlayerInningAsync(PlayerInning);

            //Assert
            val.Result.Should().NotBeNull();
            val.Result.Should().BeEquivalentTo(PlayerInning);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Player is null")]
        [TestCategory("UnitTest")]
        public void AddPlayerAsync_Null()
        {
            //Act
            var val = PlayerInningService.AddPlayerAsync(null);

            //Assert
            val.Result.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void UpdatePlayerAsync()
        {
            //Act
            var val = PlayerInningService.UpdatePlayerAsync("PID", PlayerInning);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Player ID is null")]
        [TestCategory("UnitTest")]
        public void UpdatePlayerAsync_EmptyPlayerId()
        {
            //Act
            var val = PlayerInningService.UpdatePlayerAsync("", PlayerInning);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Player is null")]
        [TestCategory("UnitTest")]
        public void UpdatePlayerAsync_Null()
        {
            //Act
            var val = PlayerInningService.UpdatePlayerAsync("PID", null);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void GetPlayerAsync()
        {
            //Act
            var val = PlayerInningService.GetPlayerAsync("PID");

            //Assert
            val.Result.Should().BeEquivalentTo(PlayerInning);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Player ID is null")]
        [TestCategory("UnitTest")]
        public void GetPlayerAsync_EmptyPlayerId()
        {
            //Act
            var val = PlayerInningService.GetPlayerAsync("");

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void GetTeamPlayersAsync()
        {
            //Act
            var val = PlayerInningService.GetTeamPlayersAsync();

            //Assert
            val.Result.Should().BeEquivalentTo(new List<Player> { new Player { PlayerName = "PlayerName" } });
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "No teams are added")]
        [TestCategory("UnitTest")]
        public void GetUserTeamPlayersAsync_NullUserTeamRepo()
        {
            //Act
            var val = PlayerInningService.GetUserTeamPlayersAsync(null);

            //Assert
            val.Result.Should().BeEquivalentTo(PlayerInning);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void GetUserTeamPlayersAsync()
        {
            //Arrange
            var userteams = new List<Team> { new Team { TeamName = "Chennai Super Kings", TeamId = "TeamId" } };

            //Act
            var val = PlayerInningService.GetUserTeamPlayersAsync(userteams);

            //Assert
            val.Result.Should().BeEquivalentTo(PlayerInning);
        }       


        [TestMethod]
        [TestCategory("UnitTest")]
        public void DeleteAllPlayersAsync()
        {
            //Act
            var val = PlayerInningService.DeleteAllPlayersAsync();

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void DeletePlayerAsync()
        {
            //Act
            var val = PlayerInningService.DeletePlayerAsync("PID");

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Player ID is null")]
        [TestCategory("UnitTest")]
        public void DeletePlayerAsync_EmptyPlayerId()
        {
            //Act
            var val = PlayerInningService.DeletePlayerAsync("");

            //Assert
            val.Wait();
        }
    }
}
