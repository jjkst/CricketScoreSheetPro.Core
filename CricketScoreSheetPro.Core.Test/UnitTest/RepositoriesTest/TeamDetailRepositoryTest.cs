using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Firebase.Database;
using CricketScoreSheetPro.Core.Repositories.Implementations;
using FluentAssertions;

namespace CricketScoreSheetPro.Core.Test.UnitTest.RepositoriesTest
{
    [TestClass]
    public class TeamDetailRepositoryTest
    {
        [TestMethod]
        [TestCategory("UnitTest")]
        public void TeamRepositoryCheckUrl()
        {
            //Arrange
            var baseRepo = new TeamDetailRepository(new FirebaseClient("http://baseUrl"));

            //Act
            var val = baseRepo._reference;

            //Assert
            val.BuildUrlAsync().Result.Should().Be("http://baseUrl/TeamDetail/.json");
        }
    }
}
