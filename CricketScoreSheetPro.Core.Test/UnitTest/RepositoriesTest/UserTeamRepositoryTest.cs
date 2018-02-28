using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CricketScoreSheetPro.Core.Repositories.Implementations;
using Firebase.Database;
using FluentAssertions;

namespace CricketScoreSheetPro.Core.Test.UnitTest.RepositoriesTest
{
    [TestClass]
    public class UserTeamRepositoryTest
    {
        [TestMethod]
        [TestCategory("UnitTest")]
        public void UserTeamRepositoryCheckUrl_NonImport()
        {
            //Arrange
            var baseRepo = new UserTeamRepository(new FirebaseClient("http://baseUrl"), "UUID", false);

            //Act
            var val = baseRepo._reference;

            //Assert
            val.BuildUrlAsync().Result.Should().Be("http://baseUrl/Users/UUID/UserTeams/.json");
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void UserTeamRepositoryCheckUrl_Import()
        {
            //Arrange
            var baseRepo = new UserTeamRepository(new FirebaseClient("http://baseUrl"), "UUID", true);

            //Act
            var val = baseRepo._reference;

            //Assert
            val.BuildUrlAsync().Result.Should().Be("http://baseUrl/Imports/UUID/UserTeams/.json");
        }
    }
}
