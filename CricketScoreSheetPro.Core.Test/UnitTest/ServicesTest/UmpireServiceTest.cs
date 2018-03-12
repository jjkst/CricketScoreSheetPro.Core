using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Services.Implementations;
using System.Collections.Generic;
using CricketScoreSheetPro.Core.Repositories.Interfaces;
using Moq;
using System.Threading.Tasks;
using CricketScoreSheetPro.Core.Test.Extensions;
using FluentAssertions;

namespace CricketScoreSheetPro.Core.Test.UnitTest.ServicesTest
{
    [TestClass]
    public class UmpireServiceTest
    {
        private static Umpire Umpire { get; set; }
        private static UmpireService UmpireService { get; set; }

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            //Arrange
            Umpire = new Umpire { Id = "ID" };
            var matches = new List<Umpire> { Umpire };
            var mockRepo = new Mock<IRepository<Models.Umpire>>();
            mockRepo.Setup(x => x.CreateAsync(It.IsAny<Models.Umpire>())).ReturnsAsync(Umpire);
            mockRepo.Setup((IRepository<Umpire> x) => x.CreateWithIdAsync(It.IsAny<string>(), It.IsAny<Umpire>())).Returns(Task.FromResult(0));
            mockRepo.Setup(x => x.GetListAsync()).ReturnsAsync(matches);
            mockRepo.Setup((IRepository<Umpire> x) => x.GetItemAsync(It.IsAny<string>())).ReturnsAsync((Umpire)Umpire);
            mockRepo.Setup(x => x.DeleteAsync()).Returns(Task.FromResult(0));
            mockRepo.Setup(x => x.DeleteByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(0));
            UmpireService = new UmpireService(mockRepo.Object);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "UmpireRepository is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UmpireService_NullRepository()
        {
            //Act
            var umpireService = new UmpireService(null);

            //Assert
            umpireService.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void AddUmpireAsync()
        {
            //Act            
            var val = UmpireService.AddUmpireAsync(Umpire);

            //Assert
            val.Result.Should().NotBeNull();
            val.Result.Should().BeEquivalentTo(Umpire);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Umpire is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void AddUmpireAsync_Null()
        {
            //Act
            var val = UmpireService.AddUmpireAsync(null);

            //Assert
            val.Result.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateUmpireAsync()
        {
            //Act
            var val = UmpireService.UpdateUmpireAsync("ID", Umpire);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Umpire ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateUmpireAsync_EmptyMatchId()
        {
            //Act
            var val = UmpireService.UpdateUmpireAsync("", Umpire);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Umpire is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateUmpireAsync_Null()
        {
            //Act
            var val = UmpireService.UpdateUmpireAsync("ID", null);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetUmpireAsync()
        {
            //Act
            var val = UmpireService.GetUmpireAsync("ID");

            //Assert
            val.Result.Should().BeEquivalentTo(Umpire);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Umpire ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetUmpireAsync_EmptyUmpireId()
        {
            //Act
            var val = UmpireService.GetUmpireAsync("");

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetUmpiresAsync()
        {
            //Act
            var val = UmpireService.GetUmpiresAsync();

            //Assert
            val.Result.Should().BeEquivalentTo(new List<Umpire> { Umpire });
        }


        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void DeleteAllUmpiresAsync()
        {
            //Act
            var val = UmpireService.DeleteAllUmpiresAsync();

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void DeleteUmpireAsync()
        {
            //Act
            var val = UmpireService.DeleteUmpireAsync("ID");

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Umpire ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void DeleteUmpireAsync_EmptyUmpireId()
        {
            //Act
            var val = UmpireService.DeleteUmpireAsync("");

            //Assert
            val.Wait();
        }
    }
}
