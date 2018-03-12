using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CricketScoreSheetPro.Core.Repositories.Implementations;
using Firebase.Database;
using FluentAssertions;

namespace CricketScoreSheetPro.Core.Test.UnitTest.RepositoriesTest
{
    [TestClass]
    public class TournamentDetailRepositoryTest
    {
        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("RepositoryTest")]
        public void TournamentRepositoryCheckUrl()
        {
            //Arrange
            var baseRepo = new TournamentDetailRepository(new FirebaseClient("http://baseUrl"));

            //Act
            var val = baseRepo._reference;

            //Assert
            val.BuildUrlAsync().Result.Should().Be("http://baseUrl/TournamentDetail/.json");
        }
    }
}
