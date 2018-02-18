using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CricketScoreSheetPro.Core.Services;
using CricketScoreSheetPro.Core.Models;
using FluentAssertions;
using System.Linq;

namespace CricketScoreSheetPro.Core.UnitTest.ServiceTest
{
	[TestClass]
	public class UserTournamentServiceTest
	{
        private UserTournamentService _service { get; set; }

        public UserTournamentServiceTest()
        {
            _service = new UserTournamentService("UniqueUserId", false);
        }

        [TestCleanup]
        public void MethodCleanup()
        {
            var cleanup = _service.DropTable().Result;
        }

        [TestMethod]
        [TestCategory("DatabaseAccessTest")]
        public void AddUserTournamentTest()
        {
            //Arrange
            var model = new UserTournament()
            {
                TournamentId = "UniqueTournamentId",
                TournamentName = "TournamentName",
                Status = "Open"
            };

            //Act 
            var val = _service.CreateWithId(model.TournamentId, model).Result;

            //Assert
            val.Should().NotBeNull();
        }

        [TestMethod]
        [TestCategory("DatabaseAccessTest")]
        public void ImportUserTournamentTest()
        {
            //Arrange
            var model = new UserTournament()
            {
                TournamentId = "UniqueTournamentId",
                TournamentName = "TournamentName",
                Status = "Open"
            };

            //Act 
            var service = new UserTournamentService("UniqueUserId", true);
            var val = service.CreateWithId(model.TournamentId, model).Result;


            //Assert
            val.Should().NotBeNull();
            var drop = service.DropTable().Result;
        }

        [TestMethod]
        [TestCategory("DatabaseAccessTest")]
        public void CheckExistingTournamentTest()
        {
            //Arrange
            var model = new UserTournament()
            {
                TournamentId = "UniqueTournamentId",
                TournamentName = "TournamentName",
                Status = "Open"
            };
            var val = _service.CreateWithId(model.TournamentId, model).Result;

            //Act 
            var teams = _service.ExistingUserTournaments;


            //Assert
            teams.Should().NotBeNull();
            teams.First().Should().BeEquivalentTo(model);
        }
    }
}
