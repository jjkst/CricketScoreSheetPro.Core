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
using CricketScoreSheetPro.Core.Test.Extensions;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Test.UnitTest.ServicesTest
{
    [TestClass]
    public class MatchServiceTest
    {
        private static UserMatch UserMatch { get; set; }
        private static MatchService MatchService { get; set; }

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            //Arrange
            UserMatch = new UserMatch { TournamentId = "TID" };
            var matches = new List<UserMatch> { UserMatch };
            var mockRepo = new Mock<IRepository<UserMatch>>();
            mockRepo.Setup(x => x.CreateAsync(It.IsAny<UserMatch>())).ReturnsAsync(UserMatch);
            mockRepo.Setup(x => x.CreateWithIdAsync(It.IsAny<string>(), It.IsAny<UserMatch>())).Returns(Task.FromResult(0));
            mockRepo.Setup(x => x.GetListAsync()).ReturnsAsync(matches);
            mockRepo.Setup(x => x.GetItemAsync(It.IsAny<string>())).ReturnsAsync(UserMatch);
            mockRepo.Setup(x => x.DeleteAsync()).Returns(Task.FromResult(0));
            mockRepo.Setup(x => x.DeleteByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(0));
            MatchService = new MatchService(mockRepo.Object);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "UserMatchRepository is null")]
        [TestCategory("UnitTest")]
        public void MatchService_NullRepository()
        {
            //Act
            var matchService = new MatchService(null);

            //Assert
            matchService.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void AddMatchAsync()
        {
            //Act            
            var val = MatchService.AddMatchAsync(UserMatch);

            //Assert
            val.Result.Should().NotBeNull();
            val.Result.Should().BeEquivalentTo(UserMatch);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "UserMatch is null")]
        [TestCategory("UnitTest")]
        public void AddMatchAsync_Null()
        {
            //Act
            var val = MatchService.AddMatchAsync(null);

            //Assert
            val.Result.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void UpdateMatchAsync()
        {
            //Act
            var val = MatchService.UpdateMatchAsync("MID", UserMatch);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Match ID is null")]
        [TestCategory("UnitTest")]
        public void UpdateMatchAsync_EmptyMatchId()
        {
            //Act
            var val = MatchService.UpdateMatchAsync("", UserMatch);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "UserMatch is null")]
        [TestCategory("UnitTest")]
        public void UpdateMatchAsync_Null()
        {
            //Act
            var val = MatchService.UpdateMatchAsync("MID", null);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void GetMatchAsync()
        {
            //Act
            var val = MatchService.GetMatchAsync("MID");

            //Assert
            val.Result.Should().BeEquivalentTo(UserMatch);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Match ID is null")]
        [TestCategory("UnitTest")]
        public void GetMatchAsync_EmptyMatchId()
        {
            //Act
            var val = MatchService.GetMatchAsync("");

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void GetMatchesAsync()
        {
            //Act
            var val = MatchService.GetMatchesAsync();

            //Assert
            val.Result.Should().BeEquivalentTo(new List<UserMatch> { UserMatch });
        }


        [TestMethod]
        [TestCategory("UnitTest")]
        public void DeleteAllMatchesAsync()
        {
            //Act
            var val = MatchService.DeleteAllMatchesAsync();

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void DeleteMatchAsync()
        {
            //Act
            var val = MatchService.DeleteMatchAsync("MID");

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Match ID is null")]
        [TestCategory("UnitTest")]
        public void DeleteMatchAsync_EmptyMatchId()
        {
            //Act
            var val = MatchService.DeleteMatchAsync("");

            //Assert
            val.Wait();
        }
    }
}
