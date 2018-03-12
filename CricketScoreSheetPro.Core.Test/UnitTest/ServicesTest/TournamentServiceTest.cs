using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CricketScoreSheetPro.Core.Services.Implementations;
using CricketScoreSheetPro.Core.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using CricketScoreSheetPro.Core.Repositories.Interfaces;
using Moq;
using CricketScoreSheetPro.Core.Test.Extensions;
using FluentAssertions;
using CricketScoreSheetPro.Core.Repositories.Implementations;
using Firebase.Database;

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
            mockTournamentRepo.Setup(x => x.CreateAsync(It.IsAny<Tournament>())).ReturnsAsync(Tournament);
            mockTournamentRepo.Setup(x => x.CreateWithIdAsync(It.IsAny<string>(), It.IsAny<Tournament>())).Returns(Task.FromResult(0));
            mockTournamentRepo.Setup(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>())).Returns(Task.FromResult(0));
            mockTournamentRepo.Setup(x => x.GetListAsync()).ReturnsAsync(new List<Tournament> { new Tournament { Name = Tournament.Name } });
            mockTournamentRepo.Setup(x => x.GetItemAsync(It.IsAny<string>())).ReturnsAsync(Tournament);
            mockTournamentRepo.Setup(x => x.DeleteAsync()).Returns(Task.FromResult(0));
            mockTournamentRepo.Setup(x => x.DeleteByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(0));

            var mockTournamentDetailRepo = new Mock<IRepository<TournamentDetail>>();            
            mockTournamentDetailRepo.Setup(x => x.CreateWithIdAsync(It.IsAny<string>(), It.IsAny<TournamentDetail>())).Returns(Task.FromResult(0));
            mockTournamentDetailRepo.Setup(x => x.GetItemAsync(It.IsAny<string>())).ReturnsAsync(TournamentDetail);

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
        public void AddTournamentAsync()
        {
            //Act            
            var val = TournamentService.AddTournamentAsync(Tournament);

            //Assert
            val.Result.Should().NotBeNull();
            val.Result.Should().BeEquivalentTo(Tournament);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Tournament is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void AddTournamentAsync_Null()
        {
            //Act
            var val = TournamentService.AddTournamentAsync(null);

            //Assert
            val.Result.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTournamentAsync()
        {
            //Act
            var val = TournamentService.UpdateTournamentAsync("TID", TournamentDetail);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Tournament ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTournamentAsync_EmptyPlayerId()
        {
            //Act
            var val = TournamentService.UpdateTournamentAsync("", TournamentDetail);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Tournament is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTournamentAsync_Null()
        {
            //Act
            var val = TournamentService.UpdateTournamentAsync("TID", null);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Tournament ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTournamentPropertyAsync_EmptyId()
        {
            //Act
            var val = TournamentService.UpdateTournamentPropertyAsync("","fieldname", new object());

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Tournament property is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTournamentPropertyAsync_EmptyFieldName()
        {
            //Act
            var val = TournamentService.UpdateTournamentPropertyAsync("Id", "", new object());

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Tournament property value is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTournamentPropertyAsync_EmptyValue()
        {
            //Act
            var val = TournamentService.UpdateTournamentPropertyAsync("Id", "fieldname", null);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTournamentPropertyAsync_UpdateTournamentName()
        {
            //Act
            var val = TournamentService.UpdateTournamentPropertyAsync("Id", "Name", new object());

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateTournamentPropertyAsync_UpdateTournamentStatus()
        {
            //Act
            var val = TournamentService.UpdateTournamentPropertyAsync("Id", "Status", new object());

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetTournamentAsync()
        {
            //Act
            var val = TournamentService.GetTournamentDetailAsync("TID");

            //Assert
            val.Result.Should().BeEquivalentTo(TournamentDetail);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Tournament ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetTournamentAsync_EmptyTournamentId()
        {
            //Act
            var val = TournamentService.GetTournamentDetailAsync("");

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetUserTournamentsAsync()
        {
            //Act
            var val = TournamentService.GetTournamentsAsync();

            //Assert
            val.Result.Should().BeEquivalentTo(new List<Tournament> { new Tournament { Name = Tournament.Name } });
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void DeleteAllTournamentsAsync()
        {
            //Act
            var val = TournamentService.DeleteAllTournamentsAsync();

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void DeleteTournamentAsync()
        {
            //Act
            var val = TournamentService.DeleteTournamentAsync("TID");

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Tournament ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void DeleteTournamentAsync_EmptyTournamentId()
        {
            //Act
            var val = TournamentService.DeleteTournamentAsync("");

            //Assert
            val.Wait();
        }
    }
}
