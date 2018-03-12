using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CricketScoreSheetPro.Core.Repositories.Implementations;
using Firebase.Database;
using FluentAssertions;

namespace CricketScoreSheetPro.Core.Test.UnitTest.RepositoriesTest
{
    [TestClass]
    public class TeamRepositoryTest
    {
        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("RepositoryTest")]
        public void UserTeamRepositoryCheckUrl()
        {
            //Arrange
            var baseRepo = new TeamRepository(new FirebaseClient("http://baseUrl"), "UUID");

            //Act
            var val = baseRepo._reference;

            //Assert
            val.BuildUrlAsync().Result.Should().Be("http://baseUrl/User/UUID/Team/.json");
        }
    }
}
