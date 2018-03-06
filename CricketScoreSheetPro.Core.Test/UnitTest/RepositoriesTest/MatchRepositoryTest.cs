using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CricketScoreSheetPro.Core.Repositories.Implementations;
using Firebase.Database;
using FluentAssertions;

namespace CricketScoreSheetPro.Core.Test.UnitTest.RepositoriesTest
{
    [TestClass]
    public class MatchRepositoryTest
    {
        [TestMethod]
        [TestCategory("UnitTest")]
        public void MatchRepositoryCheckUrl()
        {
            //Arrange
            var baseRepo = new MatchRepository(new FirebaseClient("http://baseUrl"), "UUID");

            //Act
            var val = baseRepo._reference;

            //Assert
            val.BuildUrlAsync().Result.Should().Be("http://baseUrl/Users/UUID/Match/.json");
        }
    }
}
