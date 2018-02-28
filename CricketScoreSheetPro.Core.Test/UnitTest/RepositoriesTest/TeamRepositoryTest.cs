using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Firebase.Database;
using CricketScoreSheetPro.Core.Repositories.Implementations;
using FluentAssertions;

namespace CricketScoreSheetPro.Core.Test.UnitTest.RepositoriesTest
{
    [TestClass]
    public class TeamRepositoryTest
    {
        [TestMethod]
        [TestCategory("UnitTest")]
        public void TeamRepositoryCheckUrl()
        {
            //Arrange
            var baseRepo = new TeamRepository(new FirebaseClient("http://baseUrl"));

            //Act
            var val = baseRepo._reference;

            //Assert
            val.BuildUrlAsync().Result.Should().Be("http://baseUrl/Teams/.json");
        }
    }
}
