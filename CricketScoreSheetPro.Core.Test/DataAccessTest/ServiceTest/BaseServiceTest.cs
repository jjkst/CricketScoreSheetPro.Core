using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CricketScoreSheetPro.Core.Services;
using FluentAssertions;

namespace CricketScoreSheetPro.Core.Test.DataAccessTest.ServiceTest
{
    [TestClass]
    public class BaseServiceTest
    {

        [TestMethod]
        [TestCategory("DataAccessTest")]
        public void CreateValidObjectTest()
        {
            //Arrange
            var mockObject = new { Name = "CreateWithValidObjectTest" };
            var baseService = new BaseService<dynamic>()
            {
                _reference = BaseService<dynamic>.Client.Child("Test")
            };

            //Act
            var create = baseService.Create(mockObject).Result;

            //Assert
            mockObject.Name.Should().Be(create.Name);
        }

        [TestMethod]
        [TestCategory("DataAccessTest")]
        public void CreateWithIdValidObjectTest()
        {
            var mockObject = new { Name = "CreateWithValidObjectTest" };
            var baseService = new BaseService<dynamic>()
            {
                _reference = BaseService<dynamic>.Client.Child("Test")
            };

            //Act
            var create = baseService.CreateWithId("Id", mockObject).Result;

            //Assert
            mockObject.Name.Should().Be(create.Name);
        }

    }
}
