using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CricketScoreSheetPro.Core.Services;
using FluentAssertions;

namespace CricketScoreSheetPro.Core.Test.DataAccessTest.ServiceTest
{
    [TestClass]
    public class BaseServiceTest
    {
        public class TestObject
        {
            public string Name { get; set; }
        }

        private TestObject TestObj { get; set; }

        public BaseServiceTest()
        {
            TestObj = new TestObject
            {
                Name = "TEST"
            };
        }

        [TestMethod]
        [TestCategory("DataAccessTest")]
        public void CreateValidObjectTest()
        {
            //Arrange
            var baseService = new BaseService<TestObject>()
            {
                _reference = BaseService<TestObject>.Client.Child("Test")
            };

            //Act
            var create = baseService.Create(TestObj).Result;

            //Assert
            TestObj.Name.Should().Be(create.Name);
        }

        [TestMethod]
        [TestCategory("DataAccessTest")]
        public void CreateWithIdValidObjectTest()
        {
            var baseService = new BaseService<TestObject>()
            {
                _reference = BaseService<TestObject>.Client.Child("Test")
            };

            //Act
            var create = baseService.CreateWithId("Id", TestObj).Result;

            //Assert
            TestObj.Name.Should().Be(create.Name);
        }

    }
}
