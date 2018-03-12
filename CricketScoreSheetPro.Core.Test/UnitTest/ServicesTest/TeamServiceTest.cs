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
        private static TeamDetail TeamDetail { get; set; }
        private static TeamService TeamService { get; set; }

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            //Arrange
            Team = new Team { Name = "TeamName" };
            TeamDetail = new TeamDetail { Name = Team.Name };
            var teams = new List<Team> { Team };
            var mockTeamRepo = new Mock<IRepository<Team>>();
            mockTeamRepo.Setup(x => x.CreateAsync(It.IsAny<Team>())).ReturnsAsync(Team);
            mockTeamRepo.Setup(x => x.CreateWithIdAsync(It.IsAny<string>(), It.IsAny<Team>())).Returns(Task.FromResult(0));     
            mockTeamRepo.Setup(x => x.GetListAsync()).ReturnsAsync(teams);
            mockTeamRepo.Setup(x => x.DeleteAsync()).Returns(Task.FromResult(0));
            mockTeamRepo.Setup(x => x.DeleteByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(0));
            var mockTeamDetailRepo = new Mock<IRepository<TeamDetail>>();
            mockTeamDetailRepo.Setup(x => x.CreateWithIdAsync(It.IsAny<string>(), It.IsAny<TeamDetail>())).Returns(Task.FromResult(0));
            mockTeamDetailRepo.Setup(x => x.GetItemAsync(It.IsAny<string>())).ReturnsAsync(TeamDetail);
            TeamService = new TeamService(mockTeamRepo.Object, mockTeamDetailRepo.Object);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "TeamRepository is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void TeamService_NullTeamRepository()
        {
            //Act
            var teamService = new TeamService(null, (new Mock<IRepository<TeamDetail>>()).Object);

            //Assert
            teamService.Should().BeNull();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "TeamDetailRepository is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void TeamService_NullTeamDetailRepository()
        {
            //Act
            var teamService = new TeamService((new Mock<IRepository<Team>>()).Object, null);

            //Assert
            teamService.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
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
        [TestCategory("ServiceTest")]
        public void AddTeamAsync_Null()
        {
            //Act
            var val = TeamService.AddTeamAsync(null);

            //Assert
            val.Result.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTeamAsync()
        {
            //Act
            var val = TeamService.UpdateTeamAsync("TID", TeamDetail);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Team ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTeamAsync_EmptyTeamId()
        {
            //Act
            var val = TeamService.UpdateTeamAsync("", TeamDetail);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Team is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTeamAsync_Null()
        {
            //Act
            var val = TeamService.UpdateTeamAsync("TID", null);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Team ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTeamPropertyAsync_EmptyId()
        {
            //Act
            var val = TeamService.UpdateTeamPropertyAsync("", "fieldname", new object());

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Team property is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTeamPropertyAsync_EmptyFieldName()
        {
            //Act
            var val = TeamService.UpdateTeamPropertyAsync("Id", "", new object());

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Team property value is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTeamPropertyAsync_EmptyValue()
        {
            //Act
            var val = TeamService.UpdateTeamPropertyAsync("Id", "fieldname", null);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTeamPropertyAsync_UpdateTournamentName()
        {
            //Act
            var val = TeamService.UpdateTeamPropertyAsync("Id", "Name", new object());

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetTeamDetailAsync()
        {
            //Act
            var val = TeamService.GetTeamDetailAsync("TID");

            //Assert
            val.Result.Should().BeEquivalentTo(TeamDetail);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Team ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetTeamDetailAsync_EmptyTeamId()
        {
            //Act
            var val = TeamService.GetTeamDetailAsync("");

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetTeamsAsync()
        {
            //Act
            var val = TeamService.GetTeamsAsync();

            //Assert
            val.Result.Should().BeEquivalentTo(new List<Team> { Team });
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void DeleteAllTeamsAsync()
        {
            //Act
            var val = TeamService.DeleteAllTeamsAsync();

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
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
        [TestCategory("ServiceTest")]
        public void DeleteTeamAsync_EmptyTeamId()
        {
            //Act
            var val = TeamService.DeleteTeamAsync("");

            //Assert
            val.Wait();
        }
    }
}
