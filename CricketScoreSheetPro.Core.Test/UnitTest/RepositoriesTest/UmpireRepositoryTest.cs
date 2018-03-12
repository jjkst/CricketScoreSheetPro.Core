using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Firebase.Database;
using CricketScoreSheetPro.Core.Repositories.Implementations;
using FluentAssertions;

namespace CricketScoreSheetPro.Core.Test.UnitTest.RepositoriesTest
{
    [TestClass]
    public class UmpireRepositoryTest
    {
        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("RepositoryTest")]
        public void UmpireRepositoryCheckUrl()
        {
            //Arrange
            var baseRepo = new UmpireRepository(new FirebaseClient("http://baseUrl"), "UUID");

            //Act
            var val = baseRepo._reference;

            //Assert
            val.BuildUrlAsync().Result.Should().Be("http://baseUrl/User/UUID/Umpire/.json");
        }
    }
}
