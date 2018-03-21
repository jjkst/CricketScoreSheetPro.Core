using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Services.Implementations;
using CricketScoreSheetPro.Core.Repositories.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using Moq;
using CricketScoreSheetPro.Core.Test.Extensions;
using FluentAssertions;

namespace CricketScoreSheetPro.Core.Test.UnitTest.ServicesTest
{
    [TestClass]
    public class TeamInningServiceTest
    {
        private static TeamInning TeamInning { get; set; }
        private static TeamInningService TeamInningService { get; set; }

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            //Arrange
            TeamInning = new TeamInning { TeamName = "TeamName", TeamId = "TeamId" };
            var teams = new List<TeamInning> { TeamInning };

            var mockTeamRepo = new Mock<IRepository<TeamInning>>();
            mockTeamRepo.Setup(x => x.CreateAsync(It.IsAny<TeamInning>())).ReturnsAsync(TeamInning);
            mockTeamRepo.Setup(x => x.CreateWithIdAsync(It.IsAny<string>(), It.IsAny<TeamInning>())).Returns(Task.FromResult(0));
            mockTeamRepo.Setup(x => x.GetFilteredListAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(teams);
            mockTeamRepo.Setup(x => x.GetItemAsync(It.IsAny<string>())).ReturnsAsync(TeamInning);
            mockTeamRepo.Setup(x => x.DeleteAsync()).Returns(Task.FromResult(0));
            mockTeamRepo.Setup(x => x.DeleteByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(0));

            TeamInningService = new TeamInningService(mockTeamRepo.Object);
        }


        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "TeamInningRepository is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void TeamInningService_NullTeamPlayerRepository()
        {
            //Act
            var teaminningService = new TeamInningService(null);

            //Assert
            teaminningService.Should().BeNull();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "New TeamInning is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void AddTeamInningAsync_Null()
        {
            //Act            
            var val = TeamInningService.AddTeamInningAsync(null);

            //Assert
            val.Result.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void AddTeamInningAsync()
        {
            //Act            
            var val = TeamInningService.AddTeamInningAsync(TeamInning);

            //Assert
            val.Result.Should().NotBeNull();
            val.Result.Should().BeEquivalentTo(TeamInning);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTeamInningAsync()
        {
            //Act
            var val = TeamInningService.UpdateTeamInningAsync("TID", TeamInning);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "TeamInning ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTeamInningAsync_EmptyTeamInningId()
        {
            //Act
            var val = TeamInningService.UpdateTeamInningAsync("", TeamInning);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "TeamInning is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTeamInningAsync_Null()
        {
            //Act
            var val = TeamInningService.UpdateTeamInningAsync("TID", null);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetTeanInningAsync()
        {
            //Act
            var val = TeamInningService.GetTeamInningAsync("TID");

            //Assert
            val.Result.Should().BeEquivalentTo(TeamInning);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "TeamInning ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetTeamInningAsync_EmptyPlayerId()
        {
            //Act
            var val = TeamInningService.GetTeamInningAsync("");

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetTeamInningsAsync()
        {
            //Act
            var val = TeamInningService.GetTeamInningsAsync("teamId");

            //Assert
            val.Result.Should().BeEquivalentTo(new List<TeamInning> { TeamInning });
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "TeamID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetTeamInningsAsync_EmptyTeamId()
        {
            //Act
            var val = TeamInningService.GetTeamInningsAsync(null);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetTeamInningsByTournamentIdAsync()
        {
            //Act
            var val = TeamInningService.GetTeamInningsByTournamentIdAsync("teamId", "tournamentId");

            //Assert
            val.Result.Should().BeEquivalentTo(new List<TeamInning> { TeamInning });
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "TeamID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetTeamInningsByTournamentIdAsync_EmptyTeamId()
        {
            //Act
            var val = TeamInningService.GetTeamInningsByTournamentIdAsync("", "tournamentId");

            //Assert
            val.Result.Should().BeEquivalentTo(new List<TeamInning> { TeamInning });
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "TournamentId is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetTeamInningsByTournamentIdAsync_EmptyTournamentId()
        {
            //Act
            var val = TeamInningService.GetTeamInningsByTournamentIdAsync("teamId", "");

            //Assert
            val.Result.Should().BeEquivalentTo(new List<TeamInning> { TeamInning });
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void DeleteAllTeamInningsAsync()
        {
            //Act
            var val = TeamInningService.DeleteAllTeamInningsAsync();

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void DeletePlayerAsync()
        {
            //Act
            var val = TeamInningService.DeleteTeamInningAsync("TID");

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "TeamInning ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void DeletePlayerAsync_EmptyPlayerId()
        {
            //Act
            var val = TeamInningService.DeleteTeamInningAsync("");

            //Assert
            val.Wait();
        }

    }
}
