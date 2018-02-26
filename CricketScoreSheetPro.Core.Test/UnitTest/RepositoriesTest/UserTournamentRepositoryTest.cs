﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CricketScoreSheetPro.Core.Repositories.Implementations;
using Firebase.Database;
using FluentAssertions;

namespace CricketScoreSheetPro.Core.Test.UnitTest.RepositoriesTest
{
    [TestClass]
    public class UserTournamentRepositoryTest
    {
        [TestMethod]
        [TestCategory("UnitTest")]
        public void CheckUrl_NonImport()
        {
            //Arrange
            var baseRepo = new UserTournamentRepository(new FirebaseClient("http://baseUrl"), "UUID", false);

            //Act
            var val = baseRepo._reference;

            //Assert
            val.BuildUrlAsync().Result.Should().Be("http://baseUrl/Users/UUID/UserTournaments/.json");
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void CheckUrl_Import()
        {
            //Arrange
            var baseRepo = new UserTournamentRepository(new FirebaseClient("http://baseUrl"), "UUID", true);

            //Act
            var val = baseRepo._reference;

            //Assert
            val.BuildUrlAsync().Result.Should().Be("http://baseUrl/Imports/UUID/UserTournaments/.json");
        }
    }
}
