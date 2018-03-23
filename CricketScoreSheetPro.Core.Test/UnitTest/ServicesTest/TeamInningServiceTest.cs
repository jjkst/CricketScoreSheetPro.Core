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
            mockTeamRepo.Setup(x => x.Create(It.IsAny<TeamInning>())).Returns("TeamInningId");
            mockTeamRepo.Setup(x => x.GetFilteredList(It.IsAny<string>(), It.IsAny<string>())).Returns(teams);
            mockTeamRepo.Setup(x => x.GetItem(It.IsAny<string>())).Returns(TeamInning);

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
        public void AddTeamInning_Null()
        {
            //Act            
            var val = TeamInningService.AddTeamInning(null);

            //Assert
            val.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void AddTeamInning()
        {
            //Act            
            var val = TeamInningService.AddTeamInning(TeamInning);

            //Assert
            val.Should().NotBeNull();
            val.Should().BeEquivalentTo("TeamInningId");
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTeamInning()
        {
            //Act
            TeamInningService.UpdateTeamInning("TID", TeamInning);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "TeamInning ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTeamInning_EmptyTeamInningId()
        {
            //Act
            TeamInningService.UpdateTeamInning("", TeamInning);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "TeamInning is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTeamInning_Null()
        {
            //Act
            TeamInningService.UpdateTeamInning("TID", null);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetTeanInning()
        {
            //Act
            var val = TeamInningService.GetTeamInning("TID");

            //Assert
            val.Should().BeEquivalentTo(TeamInning);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "TeamInning ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetTeamInning_EmptyPlayerId()
        {
            //Act
            TeamInningService.GetTeamInning("");
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetTeamInnings()
        {
            //Act
            var val = TeamInningService.GetTeamInnings("teamId");

            //Assert
            val.Should().BeEquivalentTo(new List<TeamInning> { TeamInning });
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "TeamID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetTeamInnings_EmptyTeamId()
        {
            //Act
            TeamInningService.GetTeamInnings(null);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetTeamInningsByTournamentId()
        {
            //Act
            var val = TeamInningService.GetTeamInningsByTournamentId("teamId", "tournamentId");

            //Assert
            val.Should().BeEquivalentTo(new List<TeamInning> { TeamInning });
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "TeamID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetTeamInningsByTournamentId_EmptyTeamId()
        {
            //Act
            var val = TeamInningService.GetTeamInningsByTournamentId("", "tournamentId");

            //Assert
            val.Should().BeEquivalentTo(new List<TeamInning> { TeamInning });
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "TournamentId is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetTeamInningsByTournamentId_EmptyTournamentId()
        {
            //Act
            var val = TeamInningService.GetTeamInningsByTournamentId("teamId", "");

            //Assert
            val.Should().BeEquivalentTo(new List<TeamInning> { TeamInning });
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void DeleteAllTeamInnings()
        {
            //Act
            TeamInningService.DeleteAllTeamInnings();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void DeletePlayer()
        {
            //Act
            TeamInningService.DeleteTeamInning("TID");
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "TeamInning ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void DeleteTeamInning_EmptyPlayerId()
        {
            //Act
            TeamInningService.DeleteTeamInning("");
        }

    }
}
