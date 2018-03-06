using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CricketScoreSheetPro.Core.Repositories.Implementations;
using Firebase.Database;
using FluentAssertions;

namespace CricketScoreSheetPro.Core.Test.UnitTest.RepositoriesTest
{
    [TestClass]
    public class PlayerInningRepositoryTest
    {
        [TestMethod]
        [TestCategory("UnitTest")]
        public void PlayerRepositoryCheckUrl()
        {
            //Arrange
            var baseRepo = new PlayerInningRepository(new FirebaseClient("http://baseUrl"));

            //Act
            var val = baseRepo._reference;

            //Assert
            val.BuildUrlAsync().Result.Should().Be("http://baseUrl/PlayerInning/.json");
        }
    }
}
