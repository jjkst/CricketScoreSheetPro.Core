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
            mockRepo.Setup(x => x.Create(It.IsAny<Models.Umpire>())).Returns(Umpire);
            mockRepo.Setup(x => x.GetList()).Returns(matches);
            mockRepo.Setup((IRepository<Umpire> x) => x.GetItem(It.IsAny<string>())).Returns((Umpire)Umpire);
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
        public void AddUmpire()
        {
            //Act            
            var val = UmpireService.AddUmpire(Umpire);

            //Assert
            val.Should().NotBeNull();
            val.Should().BeEquivalentTo(Umpire);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Umpire is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void AddUmpire_Null()
        {
            //Act
            var val = UmpireService.AddUmpire(null);

            //Assert
            val.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateUmpire()
        {
            //Act
            UmpireService.UpdateUmpire("ID", Umpire);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Umpire ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateUmpire_EmptyMatchId()
        {
            //Act
            UmpireService.UpdateUmpire("", Umpire);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Umpire is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void UpdateUmpire_Null()
        {
            //Act
            UmpireService.UpdateUmpire("ID", null);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetUmpire()
        {
            //Act
            var val = UmpireService.GetUmpire("ID");

            //Assert
            val.Should().BeEquivalentTo(Umpire);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Umpire ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetUmpire_EmptyUmpireId()
        {
            //Act
            UmpireService.GetUmpire("");
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void GetUmpires()
        {
            //Act
            var val = UmpireService.GetUmpires();

            //Assert
            val.Should().BeEquivalentTo(new List<Umpire> { Umpire });
        }


        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void DeleteAllUmpires()
        {
            //Act
            UmpireService.DeleteAllUmpires();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void DeleteUmpire()
        {
            //Act
            UmpireService.DeleteUmpire("ID");
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Umpire ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("ServiceTest")]
        public void DeleteUmpire_EmptyUmpireId()
        {
            //Act
            UmpireService.DeleteUmpire("");
        }
    }
}
