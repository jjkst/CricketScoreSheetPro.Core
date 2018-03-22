using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Repositories.Interfaces;
using CricketScoreSheetPro.Core.Services.Implementations;
using CricketScoreSheetPro.Core.Test.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

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
            mockTeamRepo.Setup(x => x.Create(It.IsAny<Team>())).Returns(Team);
            mockTeamRepo.Setup(x => x.GetList()).Returns(teams);
            var mockTeamDetailRepo = new Mock<IRepository<TeamDetail>>();
            mockTeamDetailRepo.Setup(x => x.GetItem(It.IsAny<string>())).Returns(TeamDetail);
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
        public void AddTeam()
        {
            //Act            
            var val = TeamService.AddTeam(Team);

            //Assert
            val.Should().NotBeNull();
            val.Should().BeEquivalentTo(Team);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Team is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void AddTeam_Null()
        {
            //Act
            var val = TeamService.AddTeam(null);

            //Assert
            val.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTeam()
        {
            //Act
            TeamService.UpdateTeam("TID", TeamDetail);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "Team ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTeam_EmptyTeamId()
        {
            //Act
            TeamService.UpdateTeam("", TeamDetail);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Team is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTeam_Null()
        {
            //Act
            TeamService.UpdateTeam("TID", null);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "Team ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTeamProperty_EmptyId()
        {
            //Act
            TeamService.UpdateTeamProperty("", "fieldname", new object());
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "Team property is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTeamProperty_EmptyFieldName()
        {
            //Act
            TeamService.UpdateTeamProperty("Id", "", new object());
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "Team property value is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTeamProperty_EmptyValue()
        {
            //Act
            TeamService.UpdateTeamProperty("Id", "fieldname", null);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTeamProperty_UpdateTournamentName()
        {
            //Act
            TeamService.UpdateTeamProperty("Id", "Name", new object());
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetTeamDetail()
        {
            //Act
            var val = TeamService.GetTeamDetail("TID");

            //Assert
            val.Should().BeEquivalentTo(TeamDetail);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "Team ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetTeamDetail_EmptyTeamId()
        {
            //Act
            var val = TeamService.GetTeamDetail("");

            //Assert
            val.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetTeams()
        {
            //Act
            var val = TeamService.GetTeams();

            //Assert
            val.Should().BeEquivalentTo(new List<Team> { Team });
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void DeleteAllTeams()
        {
            //Act
            TeamService.DeleteAllTeams();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void DeleteTeam()
        {
            //Act
            TeamService.DeleteTeam("TID");
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "Team ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void DeleteTeam_EmptyTeamId()
        {
            //Act
            TeamService.DeleteTeam("");
        }
    }
}
