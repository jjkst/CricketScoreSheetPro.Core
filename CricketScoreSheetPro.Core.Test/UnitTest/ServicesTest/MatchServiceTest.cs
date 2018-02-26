using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CricketScoreSheetPro.Core.Services.Implementations;
using CricketScoreSheetPro.Core.Repositories.Interfaces;
using Moq;
using CricketScoreSheetPro.Core.Models;
using FluentAssertions;
using Firebase.Database;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Test.UnitTest.ServicesTest
{
    [TestClass]
    public class MatchServiceTest
    {
        [TestMethod]
        public void AddMatchAsync()
        {
            //Arrange
            var newUserMatch = new UserMatch { TournamentId = "TID" };
            var fo = new Mock<FirebaseObject<UserMatch>>();
            fo.Setup(x => x.Key).Returns("Id");
            fo.Setup(x => x.Object).Returns(newUserMatch);

            var mockRepo = new Mock<IRepository<UserMatch>>();
            mockRepo.Setup(x => x.CreateAsync(It.IsAny<UserMatch>())).ReturnsAsync(fo.Object);


            //Act
            var matchService = new MatchService(mockRepo.Object);
            var val = matchService.AddMatchAsync(newUserMatch);

            //Assert
            val.Should().NotBeNull();
        }

        [TestMethod]
        public void GetMatchesAsync()
        {
            ////Arrange
            //var newUserMatch = new UserMatch { TournamentId = "TID" };
            //var f = new Dictionary<string, UserMatch> { { "MID", newUserMatch } };

            //var fo = new List<FirebaseObject<UserMatch>>
            //{
            //    Convert.ChangeType(f, typeof(FirebaseObject<UserMatch>))
            //};

            //var mockRepo = new Mock<IRepository<UserMatch>>();
            //mockRepo.Setup(x => x.GetListAsync()).ReturnsAsync(fo);


            ////Act
            //var matchService = new MatchService(mockRepo.Object);
            //var val = matchService.AddMatchAsync(newUserMatch);

            ////Assert
            //val.Should().NotBeNull();
        }
    }
}
