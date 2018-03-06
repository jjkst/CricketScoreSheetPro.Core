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

namespace CricketScoreSheetPro.Core.Test.UnitTest.ServicesTest
{
    [TestClass]
    public class TournamentServiceTest
    {
        private static TournamentDetail Tournament { get; set; }
        private static TournamentService TournamentService { get; set; }

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            //Arrange
            Tournament = new TournamentDetail { Name = "IPL" };
            var teams = new List<TournamentDetail> { Tournament };
            var mockTournamentRepo = new Mock<IRepository<TournamentDetail>>();
            mockTournamentRepo.Setup(x => x.CreateAsync(It.IsAny<TournamentDetail>())).ReturnsAsync(Tournament);
            mockTournamentRepo.Setup(x => x.CreateWithIdAsync(It.IsAny<string>(), It.IsAny<TournamentDetail>())).Returns(Task.FromResult(0));
            mockTournamentRepo.Setup(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>())).Returns(Task.FromResult(0));
            mockTournamentRepo.Setup(x => x.GetItemAsync(It.IsAny<string>())).ReturnsAsync(Tournament);
            mockTournamentRepo.Setup(x => x.DeleteAsync()).Returns(Task.FromResult(0));
            mockTournamentRepo.Setup(x => x.DeleteByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(0));
            var mockUserTournamentRepo = new Mock<IRepository<Tournament>>();
            mockUserTournamentRepo.Setup(x => x.CreateWithIdAsync(It.IsAny<string>(), It.IsAny<Tournament>())).Returns(Task.FromResult(0));
            mockUserTournamentRepo.Setup(x => x.GetListAsync()).ReturnsAsync(new List<Tournament> { new Tournament { TournamentName = Tournament.Name } });
            TournamentService = new TournamentService(mockTournamentRepo.Object, mockUserTournamentRepo.Object);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "TournamentRepository is null")]
        [TestCategory("UnitTest")]
        public void TournamentService_NullPlayerRepository()
        {
            //Act
            var tournamentService = new TournamentService(null, (new Mock<IRepository<Tournament>>()).Object);

            //Assert
            tournamentService.Should().BeNull();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "UserTournamentRepository is null")]
        [TestCategory("UnitTest")]
        public void TournamentService_NullTeamPlayerRepository()
        {
            //Act
            var tournamentService = new TournamentService((new Mock<IRepository<TournamentDetail>>()).Object, null);

            //Assert
            tournamentService.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
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
        public void AddTournamentAsync_Null()
        {
            //Act
            var val = TournamentService.AddTournamentAsync(null);

            //Assert
            val.Result.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void UpdateTournamentAsync()
        {
            //Act
            var val = TournamentService.UpdateTournamentAsync("TID", Tournament);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Tournament ID is null")]
        [TestCategory("UnitTest")]
        public void UpdateTournamentAsync_EmptyPlayerId()
        {
            //Act
            var val = TournamentService.UpdateTournamentAsync("", Tournament);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Tournament is null")]
        [TestCategory("UnitTest")]
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
        public void UpdateTournamentPropertyAsync_EmptyValue()
        {
            //Act
            var val = TournamentService.UpdateTournamentPropertyAsync("Id", "fieldname", null);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void UpdateTournamentPropertyAsync_UpdateTournamentName()
        {
            //Act
            var val = TournamentService.UpdateTournamentPropertyAsync("Id", "Name", new object());

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void UpdateTournamentPropertyAsync_UpdateTournamentStatus()
        {
            //Act
            var val = TournamentService.UpdateTournamentPropertyAsync("Id", "Status", new object());

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void GetTournamentAsync()
        {
            //Act
            var val = TournamentService.GetTournamentAsync("TID");

            //Assert
            val.Result.Should().BeEquivalentTo(Tournament);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Tournament ID is null")]
        [TestCategory("UnitTest")]
        public void GetTournamentAsync_EmptyTournamentId()
        {
            //Act
            var val = TournamentService.GetTournamentAsync("");

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void GetUserTournamentsAsync()
        {
            //Act
            var val = TournamentService.GetTournamentsAsync();

            //Assert
            val.Result.Should().BeEquivalentTo(new List<Tournament> { new Tournament { Name = Tournament.Name } });
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void DeleteAllTournamentsAsync()
        {
            //Act
            var val = TournamentService.DeleteAllTournamentsAsync();

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
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
        public void DeleteTournamentAsync_EmptyTournamentId()
        {
            //Act
            var val = TournamentService.DeleteTournamentAsync("");

            //Assert
            val.Wait();
        }
    }
}
