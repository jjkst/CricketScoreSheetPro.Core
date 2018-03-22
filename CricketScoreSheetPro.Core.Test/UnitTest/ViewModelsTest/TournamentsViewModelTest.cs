using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CricketScoreSheetPro.Core.Services.Interfaces;
using Moq;
using System.Collections.Generic;
using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.ViewModels;
using FluentAssertions;
using CricketScoreSheetPro.Core.Test.Extensions;

namespace CricketScoreSheetPro.Core.Test.UnitTest.ViewModelsTest
{
    [TestClass]
    public class TournamentsViewModelTest
    {
        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("ViewModelTest")]
        public void TournamentsViewModel_Tournaments()
        {
            //Arrange
            var tournament = new Tournament { Name = "IPL", Status = "Open"};
            var mockService = new Mock<ITournamentService>();
            mockService.Setup(x => x.GetTournaments()).Returns(new List<Tournament> { tournament });

            //Act
            var viewModel = new TournamentViewModel(mockService.Object);
            var val = viewModel.Tournaments;

            //Assert
            val.Should().BeEquivalentTo(new List<Tournament> { tournament });
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "TournamentService is null, cannot get tournaments.")]
        [TestCategory("UnitTest")]
        [TestCategory("ViewModelTest")]
        public void TournamentsViewModel_Null()
        {
            //Act
            var viewModel = new TournamentViewModel(null);

            //Assert
            viewModel.Tournaments.Should().BeNull();
        }
    }
}
