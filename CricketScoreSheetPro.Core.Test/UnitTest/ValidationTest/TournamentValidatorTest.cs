using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Services;
using CricketScoreSheetPro.Core.Validations;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace CricketScoreSheetPro.Core.UnitTest.ValidationTest
{
    [TestClass]
    public class TournamentValidatorTest
    {
        [TestMethod]
        [TestCategory("UnitTest")]
        public void ValidateDuplicateTournamentName()
        {
            //Arrange
            var t = new Tournament() { Name = "ValidateDuplicateTournamentNameTest" };
            var ut = new UserTournament() { TournamentName = "ValidateDuplicateTournamentNameTest" };
            var mockService = new Mock<IService<UserTournament>>();
            mockService.Setup(m => m.GetList()).ReturnsAsync(new List<UserTournament> { ut });

            //Act
            var validator = new TournamentValidator(mockService.Object, t);
            var val = validator.IsValid();

            //Assert
            val.Any(et => et.Message.Contains("Tournament name already exists.") && et.Type == ErrorTypes.Error);
        }
    }
}
