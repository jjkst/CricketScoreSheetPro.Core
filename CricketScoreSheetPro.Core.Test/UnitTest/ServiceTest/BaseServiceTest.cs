using CricketScoreSheetPro.Core.Models;
using CricketScoreSheetPro.Core.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.UnitTest.ServiceTest
{
    [TestClass]
    public class BaseServiceTest
    {

        [TestMethod]
        [TestCategory("UnitTest")]
        public void CreateTest_Negative()
        {
            //Arrange
            var baseService = new BaseService<object>();

            //Act
            var val = baseService.Create(null).Result;

            //Assert
            val.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void CreateTest_Positive()
        {
            //Arrange
            object mockObject = new { Name = "CreateWithValidObjectTest" };
            var mockBaseService = new Mock<BaseService<object>>();

            //Act
            mockBaseService.Setup(x => x.Create(It.IsAny<object>())).ReturnsAsync(mockObject);

            //Assert
            mockBaseService.Verify();
            mockBaseService.Object.Create(mockObject).Result.Should().BeEquivalentTo(mockObject);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void CreateWithId_Null_Negative()
        {
            //Arrange
            var baseService = new BaseService<object>();

            //Act
            var val = baseService.CreateWithId("Id", null).Result;

            //Assert
            val.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void CreateWithId_EmptyId_Negative()
        {
            //Arrange
            object mockObject = new { Name = "CreateWithValidObjectTest" };
            var baseService = new BaseService<object>();

            //Act
            var val = baseService.CreateWithId("", null).Result;

            //Assert
            val.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void CreateWithIdTest_Positive()
        {
            //Arrange
            object mockObject = new { Name = "CreateWithIdTest_Positive" };
            var mockBaseService = new Mock<BaseService<object>>();

            //Act
            mockBaseService.Setup(x => x.CreateWithId("Id", It.IsAny<object>())).ReturnsAsync(mockObject);

            //Assert
            mockBaseService.Verify();
            mockBaseService.Object.CreateWithId("Id", mockObject).Result.Should().BeEquivalentTo(mockObject);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void Update_Null_Negative()
        {
            //Arrange
            var baseService = new BaseService<object>();

            //Act
            var val = baseService.Update("uid", "tid", null).Result;

            //Assert
            val.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void Update_EmptyUId_Negative()
        {
            //Arrange
            object mockObject = new { Name = "CreateWithValidObjectTest" };
            var baseService = new BaseService<object>();

            //Act
            var updated = baseService.Update("", "tid", mockObject);

            //Assert
            updated.Result.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void Update_EmptySubId_Negative()
        {
            //Arrange
            object mockObject = new { Name = "CreateWithValidObjectTest" };
            var baseService = new BaseService<object>();

            //Act
            var updated = baseService.Update("uid", "", mockObject);

            //Assert
            updated.Result.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void Update_Positive()
        {
            // Arrange
            object mockObject = new { Name = "Update_Positive" };
            var mockBaseService = new Mock<BaseService<object>>();

            //Act
            mockBaseService.Setup(m => m.Update(It.IsAny<string>(), It.IsAny<string>(), (It.IsAny<object>())))
                        .ReturnsAsync(mockObject);

            //Assert
            mockBaseService.Verify();
            mockBaseService.Object.Update("uid", "tid", mockObject).Result.Should().BeEquivalentTo(mockObject);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void GetItem_Negative()
        {
            //Arrange
            var baseService = new BaseService<object>();

            //Act
            var val = baseService.GetItem("").Result;

            //Assert
            val.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void GetItem_Positive()
        {
            // Arrange
            object mockObject = new { Name = "GetItem_Positive" };
            var mockBaseService = new Mock<BaseService<object>>();

            //Act
            mockBaseService.Setup(m => m.GetItem(It.IsAny<string>()))
                        .ReturnsAsync(mockObject);

            //Assert
            mockBaseService.Verify();
            mockBaseService.Object.GetItem("Id").Result.Should().BeEquivalentTo(mockObject);           
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void GetList_Positive()
        {
            // Arrange
            object mockObject = new { Name = "GetList_Positive" };
            var mockBaseService = new Mock<BaseService<object>>();

            //Act
            mockBaseService.Setup(m => m.GetList())
               .ReturnsAsync(new List<object> { mockObject });

            //Assert
            mockBaseService.Verify();
            mockBaseService.Object.GetList().Result.Should().BeEquivalentTo(new List<object> { mockObject });
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void Delete_Negative()
        {
            //Arrange
            var baseService = new BaseService<object>();

            //Act
            var val = baseService.Delete("").Result;

            //Assert
            val.Should().BeFalse();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void Delete_Positive()
        {
            // Arrange
            object mockObject = new { Name = "Delete_Positive" };
            var mockBaseService = new Mock<BaseService<object>>();

            //Act
            mockBaseService.Setup(m => m.Delete(It.IsAny<string>()))
               .ReturnsAsync(true);

            //Assert
            mockBaseService.Verify();
            mockBaseService.Object.Delete("Id").Result.Should().BeTrue();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void DropTable_Positive()
        {
            // Arrange
            object mockObject = new { Name = "Delete_Positive" };
            var mockBaseService = new Mock<BaseService<object>>();

            //Act
            mockBaseService.Setup(m => m.DropTable())
               .ReturnsAsync(true);

            //Assert
            mockBaseService.Verify();
            mockBaseService.Object.DropTable().Result.Should().BeTrue();
        }
    }
}
