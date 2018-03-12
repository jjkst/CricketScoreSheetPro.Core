using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CricketScoreSheetPro.Core.Repositories.Implementations;
using Firebase.Database;
using FluentAssertions;

namespace CricketScoreSheetPro.Core.Test.UnitTest.RepositoriesTest
{
    [TestClass]
    public class TeamInningRepositoryTest
    {
        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("RepositoryTest")]
        public void TeamPlayerRepositoryCheckUrl()
        {
            //Arrange
            var baseRepo = new TeamInningRepository(new FirebaseClient("http://baseUrl"), "teamId");

            //Act
            var val = baseRepo._reference;

            //Assert
            val.BuildUrlAsync().Result.Should().Be("http://baseUrl/TeamInning/.json");
        }
    }
}
