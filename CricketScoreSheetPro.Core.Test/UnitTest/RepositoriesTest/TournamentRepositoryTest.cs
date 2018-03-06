using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CricketScoreSheetPro.Core.Repositories.Implementations;
using Firebase.Database;
using FluentAssertions;

namespace CricketScoreSheetPro.Core.Test.UnitTest.RepositoriesTest
{
    [TestClass]
    public class TournamentRepositoryTest
    {
        [TestMethod]
        [TestCategory("UnitTest")]
        public void TournamentRepositoryCheckUrl()
        {
            //Arrange
            var baseRepo = new TournamentDetailRepository(new FirebaseClient("http://baseUrl"));

            //Act
            var val = baseRepo._reference;

            //Assert
            val.BuildUrlAsync().Result.Should().Be("http://baseUrl/Tournaments/.json");
        }
    }
}
