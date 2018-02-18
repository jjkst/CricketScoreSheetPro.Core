using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CricketScoreSheetPro.Core.Services;
using CricketScoreSheetPro.Core.Models;
using FluentAssertions;
using System.Linq;

namespace CricketScoreSheetPro.Core.UnitTest.ServiceTest
{
    [TestClass]
    public class UserTeamServiceTest
    {
        private UserTeamService _service { get; set; }

        public UserTeamServiceTest()
        {
            _service = new UserTeamService("UniqueUserId", false);
        }

        [TestCleanup]
        public void MethodCleanup()
        {
            var cleanup = _service.DropTable().Result;
        }

        [TestMethod]
        [TestCategory("DatabaseAccessTest")]
        public void CheckExistingTeamTest()
        {
            //Arrange
            var model = new UserTeam()
            {
                TournamentId = "UniqueTournamentId",
                TeamName = "TeamName",
                TeamId = "UniqueTeamId"
            };
            var val = _service.CreateWithId(model.TournamentId, model).Result;

            //Act 
            var teams = _service.ExistingUserTeams;


            //Assert
            teams.Should().NotBeNull();
            teams.First().Should().BeEquivalentTo(model);
        }
    }
}
