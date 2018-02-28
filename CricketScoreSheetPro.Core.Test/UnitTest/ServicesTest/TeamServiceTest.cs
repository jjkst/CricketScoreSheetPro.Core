using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CricketScoreSheetPro.Core.Models;
using System.Collections.Generic;
using Moq;
using CricketScoreSheetPro.Core.Repositories.Interfaces;
using CricketScoreSheetPro.Core.Services.Implementations;
using System.Threading.Tasks;
using CricketScoreSheetPro.Core.Test.Extensions;
using FluentAssertions;

namespace CricketScoreSheetPro.Core.Test.UnitTest.ServicesTest
{
    [TestClass]
    public class TeamServiceTest
    {
        private static Team Team { get; set; }
        private static TeamService TeamService { get; set; }

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            //Arrange
            Team = new Team { Name = "TeamName" };
            var teams = new List<Team> { Team };
            var mockTeamRepo = new Mock<IRepository<Team>>();
            mockTeamRepo.Setup(x => x.CreateAsync(It.IsAny<Team>())).ReturnsAsync(Team);
            mockTeamRepo.Setup(x => x.CreateWithIdAsync(It.IsAny<string>(), It.IsAny<Team>())).Returns(Task.FromResult(0));     
            mockTeamRepo.Setup(x => x.GetItemAsync(It.IsAny<string>())).ReturnsAsync(Team);
            mockTeamRepo.Setup(x => x.DeleteAsync()).Returns(Task.FromResult(0));
            mockTeamRepo.Setup(x => x.DeleteByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(0));
            var mockUserTeamRepo = new Mock<IRepository<UserTeam>>();
            mockUserTeamRepo.Setup(x => x.CreateWithIdAsync(It.IsAny<string>(), It.IsAny<UserTeam>())).Returns(Task.FromResult(0));
            mockUserTeamRepo.Setup(x => x.GetListAsync()).ReturnsAsync(new List<UserTeam> { new UserTeam { TeamName = Team.Name } });
            TeamService = new TeamService(mockTeamRepo.Object, mockUserTeamRepo.Object);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "TeamRepository is null")]
        [TestCategory("UnitTest")]
        public void TeamService_NullPlayerRepository()
        {
            //Act
            var teamService = new TeamService(null, (new Mock<IRepository<UserTeam>>()).Object);

            //Assert
            teamService.Should().BeNull();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "UserTeamRepository is null")]
        [TestCategory("UnitTest")]
        public void TeamService_NullTeamPlayerRepository()
        {
            //Act
            var teamService = new TeamService((new Mock<IRepository<Team>>()).Object, null);

            //Assert
            teamService.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void AddTeamAsync()
        {
            //Act            
            var val = TeamService.AddTeamAsync(Team);

            //Assert
            val.Result.Should().NotBeNull();
            val.Result.Should().BeEquivalentTo(Team);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Team is null")]
        [TestCategory("UnitTest")]
        public void AddTeamAsync_Null()
        {
            //Act
            var val = TeamService.AddTeamAsync(null);

            //Assert
            val.Result.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void UpdateTeamAsync()
        {
            //Act
            var val = TeamService.UpdateTeamAsync("TID", Team);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Team ID is null")]
        [TestCategory("UnitTest")]
        public void UpdateTeamAsync_EmptyPlayerId()
        {
            //Act
            var val = TeamService.UpdateTeamAsync("", Team);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Team is null")]
        [TestCategory("UnitTest")]
        public void UpdateTeamAsync_Null()
        {
            //Act
            var val = TeamService.UpdateTeamAsync("TID", null);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void GetTeamAsync()
        {
            //Act
            var val = TeamService.GetTeamAsync("TID");

            //Assert
            val.Result.Should().BeEquivalentTo(Team);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Team ID is null")]
        [TestCategory("UnitTest")]
        public void GetTeamAsync_EmptyTeamId()
        {
            //Act
            var val = TeamService.GetTeamAsync("");

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void GetUserTeamsAsync()
        {
            //Act
            var val = TeamService.GetUserTeamsAsync();

            //Assert
            val.Result.Should().BeEquivalentTo(new List<UserTeam> { new UserTeam { TeamName = "TeamName" } });
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "UserTeamRepo is null")]
        [TestCategory("UnitTest")]
        public void GetImportTeamsAsync_NullUserTeamRepo()
        {
            //Act
            var val = TeamService.GetImportTeamsAsync(null);

            //Assert
            val.Result.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void GetImportTeamsAsync()
        {
            //Arrange
            var mockUserTeamRepo = new Mock<IRepository<UserTeam>>();
            mockUserTeamRepo.Setup(x => x.GetListAsync()).ReturnsAsync(new List<UserTeam> { new UserTeam { TeamName = Team.Name } });

            //Act
            var val = TeamService.GetImportTeamsAsync(mockUserTeamRepo.Object);

            //Assert
            val.Result.Should().BeEquivalentTo(new List<UserTeam> { new UserTeam { TeamName = Team.Name } });
        }


        [TestMethod]
        [TestCategory("UnitTest")]
        public void DeleteAllTeamsAsync()
        {
            //Act
            var val = TeamService.DeleteAllTeamsAsync();

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void DeleteTeamAsync()
        {
            //Act
            var val = TeamService.DeleteTeamAsync("TID");

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Team ID is null")]
        [TestCategory("UnitTest")]
        public void DeleteTeamAsync_EmptyTeamId()
        {
            //Act
            var val = TeamService.DeleteTeamAsync("");

            //Assert
            val.Wait();
        }
    }
}
