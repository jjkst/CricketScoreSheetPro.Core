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
        private static Models.Match Match { get; set; }
        private static MatchService MatchService { get; set; }

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            //Arrange
            Match = new Models.Match { TournamentId = "TID" };
            var matches = new List<Models.Match> { Match };
            var mockRepo = new Mock<IRepository<Models.Match>>();
            mockRepo.Setup(x => x.CreateAsync(It.IsAny<Models.Match>())).ReturnsAsync(Match);
            mockRepo.Setup((IRepository<Models.Match> x) => x.CreateWithIdAsync(It.IsAny<string>(), It.IsAny<Models.Match>())).Returns(Task.FromResult(0));
            mockRepo.Setup(x => x.GetListAsync()).ReturnsAsync(matches);
            mockRepo.Setup((IRepository<Models.Match> x) => x.GetItemAsync(It.IsAny<string>())).ReturnsAsync((Models.Match)Match);
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
            var val = MatchService.AddMatchAsync((Models.Match)Match);

            //Assert
            val.Result.Should().NotBeNull();
            val.Result.Should().BeEquivalentTo((Models.Match)Match);
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
            var val = MatchService.UpdateMatchAsync("MID", (Models.Match)Match);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Match ID is null")]
        [TestCategory("UnitTest")]
        public void UpdateMatchAsync_EmptyMatchId()
        {
            //Act
            var val = MatchService.UpdateMatchAsync("", (Models.Match)Match);

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
            val.Result.Should().BeEquivalentTo((Models.Match)Match);
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
            val.Result.Should().BeEquivalentTo(new List<Models.Match> { Match });
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
