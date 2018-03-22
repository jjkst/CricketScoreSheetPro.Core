using CricketScoreSheetPro.Core.Models;
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
    public class TournamentServiceTest
    {
        private static Tournament Tournament { get; set; }
        private static TournamentDetail TournamentDetail { get; set; }
        private static TournamentService TournamentService { get; set; }

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            //Arrange
            Tournament = new Tournament { Name = "IPL" };
            TournamentDetail = new TournamentDetail { Name = Tournament.Name };
            var teams = new List<Tournament> { Tournament };

            var mockTournamentRepo = new Mock<IRepository<Tournament>>();
            mockTournamentRepo.Setup(x => x.Create(It.IsAny<Tournament>())).Returns(Tournament);
            mockTournamentRepo.Setup(x => x.GetList()).Returns(new List<Tournament> { new Tournament { Name = Tournament.Name } });
            mockTournamentRepo.Setup(x => x.GetItem(It.IsAny<string>())).Returns(Tournament);

            var mockTournamentDetailRepo = new Mock<IRepository<TournamentDetail>>();            
            mockTournamentDetailRepo.Setup(x => x.GetItem(It.IsAny<string>())).Returns(TournamentDetail);

            TournamentService = new TournamentService(mockTournamentRepo.Object, mockTournamentDetailRepo.Object);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "TournamentRepository is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void TournamentService_NullTournamentRepository()
        {
            //Act
            var tournamentService = new TournamentService(null, new Mock<IRepository<TournamentDetail>>().Object);

            //Assert
            tournamentService.Should().BeNull();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "TournamentDetailRepository is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void TournamentService_NullTournamentDetailRepository()
        {
            //Act
            var tournamentService = new TournamentService(new Mock<IRepository<Tournament>>().Object, null);

            //Assert
            tournamentService.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void AddTournament()
        {
            //Act            
            var val = TournamentService.AddTournament(Tournament);

            //Assert
            val.Should().NotBeNull();
            val.Should().BeEquivalentTo(Tournament);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Tournament is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void AddTournament_Null()
        {
            //Act
            var val = TournamentService.AddTournament(null);

            //Assert
            val.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTournament()
        {
            //Act
            TournamentService.UpdateTournament("TID", TournamentDetail);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "Tournament ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTournament_EmptyPlayerId()
        {
            //Act
            TournamentService.UpdateTournament("", TournamentDetail);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Tournament is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTournament_Null()
        {
            //Act
            TournamentService.UpdateTournament("TID", null);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "Tournament ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTournamentProperty_EmptyId()
        {
            //Act
            TournamentService.UpdateTournamentProperty("","fieldname", new object());  
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "Tournament property is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTournamentProperty_EmptyFieldName()
        {
            //Act
            TournamentService.UpdateTournamentProperty("Id", "", new object());
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "Tournament property value is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTournamentProperty_EmptyValue()
        {
            //Act
            TournamentService.UpdateTournamentProperty("Id", "fieldname", null);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTournamentProperty_UpdateTournamentName()
        {
            //Act
            TournamentService.UpdateTournamentProperty("Id", "Name", new object());
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTournamentProperty_UpdateTournamentStatus()
        {
            //Act
            TournamentService.UpdateTournamentProperty("Id", "Status", new object());
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetTournament()
        {
            //Act
            var val = TournamentService.GetTournamentDetail("TID");

            //Assert
            val.Should().BeEquivalentTo(TournamentDetail);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "Tournament ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetTournament_EmptyTournamentId()
        {
            //Act
            TournamentService.GetTournamentDetail("");
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetUserTournaments()
        {
            //Act
            var val = TournamentService.GetTournaments();

            //Assert
            val.Should().BeEquivalentTo(new List<Tournament> { new Tournament { Name = Tournament.Name } });
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void DeleteAllTournaments()
        {
            //Act
            TournamentService.DeleteAllTournaments();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void DeleteTournament()
        {
            //Act
            TournamentService.DeleteTournament("TID");
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "Tournament ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void DeleteTournament_EmptyTournamentId()
        {
            //Act
            TournamentService.DeleteTournament("");
        }
    }
}
