using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CricketScoreSheetPro.Core.Repositories.Implementations;
using Firebase.Database;
using FluentAssertions;

namespace CricketScoreSheetPro.Core.Test.UnitTest.RepositoriesTest
{
    [TestClass]
    public class TeamPlayerRepositoryTest
    {
        [TestMethod]
        [TestCategory("UnitTest")]
        public void TeamPlayerRepositoryCheckUrl()
        {
            //Arrange
            var baseRepo = new TeamPlayerRepository(new FirebaseClient("http://baseUrl"), "teamId");

            //Act
            var val = baseRepo._reference;

            //Assert
            val.BuildUrlAsync().Result.Should().Be("http://baseUrl/Teams/teamId/Players/.json");
        }
    }
}
