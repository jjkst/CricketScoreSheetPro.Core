﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Validations;
using System.Collections.Generic;
using CricketScoreSheetPro.Core.Services.Interfaces;
using Moq;
using System.Linq;
using FluentAssertions;

namespace CricketScoreSheetPro.Core.Test.UnitTest.ValidationsTest
{
    [TestClass]
    public class TournamentValidatorTest
    {
        [TestMethod]
        [TestCategory("UnitTest")]
        public void IsValidName_Duplicate()
        {
            //Arrange
            var ut = new Tournament() { Name = "ValidateDuplicateTournamentNameTest" };
            var mockService = new Mock<ITournamentService>();
            mockService.Setup(m => m.GetTournaments()).Returns(new List<Tournament> { ut });

            //Act
            var validator = new TournamentValidator(mockService.Object);
            var val = validator.IsTournamentNameExist("ValidateDuplicateTournamentNameTest");

            //Assert
            val.First().Message.Should().Be("Tournament name already exists.");
            val.First().ErrorType.Should().Be(ErrorTypes.Error);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void IsValidName_Valid()
        {
            //Arrange
            var ut = new Tournament() { Name = "ValidateDuplicateTournamentNameTest" };
            var mockService = new Mock<ITournamentService>();
            mockService.Setup(m => m.GetTournaments()).Returns(new List<Tournament> { ut });

            //Act
            var validator = new TournamentValidator(mockService.Object);
            var val = validator.IsTournamentNameExist("Valid");

            //Assert
            val.Count().Should().Be(0);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void IsValidStatus_NotValid()
        {
            //Arrange
            var mockService = new Mock<ITournamentService>();
            mockService.Setup(m => m.GetTournaments()).Returns(new List<Tournament> { new Tournament() });

            //Act
            var validator = new TournamentValidator(mockService.Object);
            var val = validator.IsValidStatus("lol");

            //Assert
            //Assert
            val.First().Message.Should().Be("Status can only be open, inprogress or complete.");
            val.First().ErrorType.Should().Be(ErrorTypes.Error);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void IsValidStatus_Valid()
        {
            //Arrange
            var mockService = new Mock<ITournamentService>();
            mockService.Setup(m => m.GetTournaments()).Returns(new List<Tournament> { new Tournament() });

            //Act
            var validator = new TournamentValidator(mockService.Object);
            var val = validator.IsValidStatus("inprogress");

            //Assert
            val.Count().Should().Be(0);
        }
    }
}
