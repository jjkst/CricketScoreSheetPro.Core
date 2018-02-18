using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CricketScoreSheetPro.Core.Models;
using System.Collections.Generic;
using CricketScoreSheetPro.Core.Services;
using CricketScoreSheetPro.Core.Validations;
using Moq;
using System.Linq;

namespace CricketScoreSheetPro.Core.UnitTest.ValidationTest
{
    [TestClass]
    public class TeamValidatorTest
    {
        [TestMethod]
        [TestCategory("UnitTest")]
        public void ValidateDuplicateTeamName()
        {
            //Arrange
            var t = new Team() { Name = "ValidateDuplicateTeamNameTest" };
            var ut = new UserTeam() { TeamName = "ValidateDuplicateTeamNameTest" };
            var mockService = new Mock<IService<UserTeam>>();
            mockService.Setup(m => m.GetList()).ReturnsAsync(new List<UserTeam> { ut });

            //Act
            var validator = new TeamValidator(mockService.Object, t);
            var val = validator.IsValid();

            //Assert
            val.Any(et=>et.Message.Contains("Team name already exists.") && et.Type == ErrorTypes.Error);
        }
    }
}
