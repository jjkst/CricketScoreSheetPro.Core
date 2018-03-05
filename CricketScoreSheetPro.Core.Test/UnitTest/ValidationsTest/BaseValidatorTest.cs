using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CricketScoreSheetPro.Core.Validations;
using Moq;
using System.Linq;
using FluentAssertions;
using CricketScoreSheetPro.Core.Models;

namespace CricketScoreSheetPro.Core.Test.UnitTest.ValidationsTest
{
    [TestClass]
    public class BaseValidatorTest
    {
        [TestMethod]
        public void IsValidString_Empty()
        {
            //Arrange 
            var validator = new Mock<BaseValidator>();

            //Act
            var val = validator.Object.IsValidString("");

            //Assert
            val.First().Message.Should().Be("Value cannot be blank.");
            val.First().ErrorType.Should().Be(ErrorTypes.Error);
        }

        [TestMethod]
        public void IsValidString_Null()
        {
            //Arrange 
            var validator = new Mock<BaseValidator>();

            //Act
            var val = validator.Object.IsValidString(null);

            //Assert
            val.First().Message.Should().Be("Value cannot be blank.");
            val.First().ErrorType.Should().Be(ErrorTypes.Error);
        }

        [TestMethod]
        public void IsValidString_Valid()
        {
            //Arrange 
            var validator = new Mock<BaseValidator>();

            //Act
            var val = validator.Object.IsValidString("V");

            //Assert
            val.Count().Should().Be(0);
        }

        [TestMethod]
        public void IsValidDate_PastDate()
        {
            //Arrange 
            var validator = new Mock<BaseValidator>();

            //Act
            var val = validator.Object.IsValidDate(DateTime.Today.AddDays(-1));

            //Assert
            val.First().Message.Should().Be("Date cannot be blank or past date.");
            val.First().ErrorType.Should().Be(ErrorTypes.Error);
        }

        [TestMethod]
        public void IsValidDate_Valid()
        {
            //Arrange 
            var validator = new Mock<BaseValidator>();

            //Act
            var val = validator.Object.IsValidDate(DateTime.Today);

            //Assert
            val.Count().Should().Be(0);
        }

        [TestMethod]
        public void IsValidMoney_Empty()
        {
            //Arrange 
            var validator = new Mock<BaseValidator>();

            //Act
            var val = validator.Object.IsValidMoney("");

            //Assert
            val.First().Message.Should().Be("Invalid money.");
            val.First().ErrorType.Should().Be(ErrorTypes.Error);
        }

        [TestMethod]
        public void IsValidMoney_InValidMoney()
        {
            //Arrange 
            var validator = new Mock<BaseValidator>();

            //Act
            var val = validator.Object.IsValidMoney("d");

            //Assert
            val.First().Message.Should().Be("Invalid money.");
            val.First().ErrorType.Should().Be(ErrorTypes.Error);
        }

        [TestMethod]
        public void IsValidMoney_Valid()
        {
            //Arrange 
            var validator = new Mock<BaseValidator>();

            //Act
            var val = validator.Object.IsValidMoney("100.00");

            //Assert
            val.Count().Should().Be(0);
        }
    }
}
