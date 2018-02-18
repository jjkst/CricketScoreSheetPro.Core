using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CricketScoreSheetPro.Core.UnitTest.ServiceTest
{
    /// <summary>
    /// Test all methods in TournamentService
    /// </summary>
    [TestClass]
    public class TournamentServiceTest
    {
        private TournamentService _service { get; set; }

        public TournamentServiceTest()
        {
            _service = new TournamentService();
        }

        [TestCleanup]
        public void MethodCleanup()
        {
            var cleanup = _service.DropTable().Result;
        }

        [TestMethod]
        [TestCategory("DatabaseAccessTest")]
        public void AddTournamentTest()
        {
            //Arrange
            var model = new Tournament()
            {
                Name = "AddTournamentTest",
                Sponsor = "IPL",
                Status = "Open",
                StartDate = DateTime.Today.AddDays(15),
                EntryFee = 100,
                Prizes = { "Winner prize : $250", "RunnerUp prize : $100" },
                Facilities = { "Water", "Two tennies balls" },
                Venues = { "School A", "School B" }
            };

            //Act    
            var val = _service.Create(model).Result;

            //Assert
            val.Should().NotBeNull();
        }

        [TestMethod]
        [TestCategory("DatabaseAccessTest")]
        public void UpdateTournamentTest()
        {
            //Arrange            
            var model = new Tournament()
            {
                Name = "UpdateTournamentTest"
            };

            var val = _service.Create(model).Result;
            var data = _service.GetItem(val.Id).Result;

            //Act
            data.Name = "UpdateTournamentTest Edited";           
            data.Sponsor = "IPL Edited";
            data.Status = "Complete";
            data.StartDate = DateTime.Today.AddDays(5);            
            data.EntryFee = 150;
            data.Prizes.Add("Price Edited");
            data.Facilities.Add("Facilities Edited");
            data.Venues.Add("Venues Edited");

            var update = _service.Update(data.Id, "Name", data.Name).Result;
            update.Should().NotBeNull();
            var bulkupdate = _service.CreateWithId(data.Id, data).Result == data;

            //Assert
            bulkupdate.Should().BeTrue();
            var updated_data = _service.GetItem(data.Id).Result;
            updated_data.Name.Should().Be(data.Name);
            updated_data.Sponsor.Should().Be(data.Sponsor);
            updated_data.Status.Should().Be(data.Status);
            updated_data.StartDate.Should().Be(data.StartDate);
            updated_data.EntryFee.Should().Be(data.EntryFee);
            updated_data.Prizes.Should().BeEquivalentTo(data.Prizes);
            updated_data.Facilities.Should().BeEquivalentTo(data.Facilities);
            updated_data.Venues.Should().BeEquivalentTo(data.Venues);
        }

        [TestMethod]
        [TestCategory("DatabaseAccessTest")]
        public void GetTournamentListTest()
        {
            //Arrange
            var beforeCount = _service.GetList().Result.Count;
            AddTournamentTest();

            //Act    
            var afterCount = _service.GetList().Result.Count;

            //Assert
            afterCount.Should().Be(beforeCount + 1);
        }

        [TestMethod]
        [TestCategory("DatabaseAccessTest")]
        public void GetTournamentTest()
        {
            //Arrange
            var model = new Tournament()
            {
                Name = "GetTournamentTest"
            };
            var val = _service.Create(model).Result;

            //Act    
            var item = _service.GetItem(val.Id).Result;

            //Assert
            item.Name.Should().Be(model.Name);
        }

        [TestMethod]
        [TestCategory("DatabaseAccessTest")]
        public void DeleteTournamentTest()
        {
            //Arrange
            var model = new Tournament()
            {
                Name = "GetTournamentTest"
            };
            var val = _service.Create(model).Result;

            //Act    
            var item = _service.Delete(val.Id).Result;

            //Assert
            item.Should().BeTrue();
            _service.GetItem(val.Id).Result.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("DatabaseAccessTest")]
        public void DropTournamentTableTest()
        {
            //Act    
            var item = _service.DropTable().Result;

            //Assert
            item.Should().BeTrue();
            _service.GetList().Result.Count.Should().Be(0);
        }
    }
}
