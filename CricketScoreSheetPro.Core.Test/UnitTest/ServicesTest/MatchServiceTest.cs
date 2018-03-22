using CricketScoreSheetPro.Core.Repositories.Interfaces;
using CricketScoreSheetPro.Core.Services.Implementations;
using CricketScoreSheetPro.Core.Test.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
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
            mockRepo.Setup(x => x.Create(It.IsAny<Models.Match>())).Returns(Match);
            mockRepo.Setup(x => x.GetList()).Returns(matches);
            mockRepo.Setup((IRepository<Models.Match> x) => x.GetItem(It.IsAny<string>())).Returns((Models.Match)Match);
            MatchService = new MatchService(mockRepo.Object);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "MatchRepository is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void MatchService_NullRepository()
        {
            //Act
            var matchService = new MatchService(null);

            //Assert
            matchService.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void AddMatch()
        {
            //Act            
            var val = MatchService.AddMatch(Match);

            //Assert
            val.Should().NotBeNull();
            val.Should().BeEquivalentTo(Match);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Match is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void AddMatch_Null()
        {
            //Act
            var val = MatchService.AddMatch(null);

            //Assert
            val.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateMatch()
        {
            //Act
            MatchService.UpdateMatch("MID", Match);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "Match ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateMatch_EmptyMatchId()
        {
            //Act
            MatchService.UpdateMatch("", Match);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "UserMatch is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateMatch_Null()
        {
            //Act
            MatchService.UpdateMatch("MID", null);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetMatch()
        {
            //Act
            var val = MatchService.GetMatch("MID");

            //Assert
            val.Should().BeEquivalentTo(Match);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "Match ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetMatch_EmptyMatchId()
        {
            //Act
            MatchService.GetMatch("");
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetMatches()
        {
            //Act
            var val = MatchService.GetMatches();

            //Assert
            val.Should().BeEquivalentTo(new List<Models.Match> { Match });
        }


        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void DeleteAllMatches()
        {
            //Act
            MatchService.DeleteAllMatches();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void DeleteMatch()
        {
            //Act
            MatchService.DeleteMatch("MID");
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "Match ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void DeleteMatch_EmptyMatchId()
        {
            //Act
            MatchService.DeleteMatch("");
        }
    }
}
