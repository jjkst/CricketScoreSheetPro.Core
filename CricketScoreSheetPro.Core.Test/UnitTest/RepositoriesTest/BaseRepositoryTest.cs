using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CricketScoreSheetPro.Core.Repositories.Interfaces;
using System.Collections.Generic;
using Firebase.Database;
using System.Collections.ObjectModel;
using FluentAssertions;
using CricketScoreSheetPro.Core.Repositories.Implementations;
using CricketScoreSheetPro.Core.Test.Extensions;
using Firebase.Database.Query;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Core.Test.UnitTest.RepositoriesTest
{
    [TestClass]
    public class BaseRepositoryTest
    {        
        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Object is null")]
        [TestCategory("UnitTest")]
        public void CreateAsync_Null()
        {
            //Arrange
            var baseRepo = new BaseRepository<object>();

            //Act
            var val = baseRepo.CreateAsync(null).Result;

            //Assert
            val.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void CreateAsync_NotNull()
        {
            //Arrange
            var obj = new { Name = "CreateWithValidObjectTest" };
            var baseRepo = new Mock<BaseRepository<object>>();

            //Act
            baseRepo.Setup(x => x.CreateAsync(It.IsAny<object>())).ReturnsAsync(obj);

            //Assert
            baseRepo.Verify();
            baseRepo.Object.CreateAsync(obj).Result.Should().BeEquivalentTo(obj);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Object is null")]
        [TestCategory("UnitTest")]
        public void CreateWithIdAsync_Null()
        {
            //Arrange
            var baseRepo = new BaseRepository<object>();

            //Act
            var val = baseRepo.CreateWithIdAsync("Id", null);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "Given ID is null")]
        [TestCategory("UnitTest")]
        public void CreateWithIdAsync_EmptyId()
        {
            //Arrange
            var baseRepo = new BaseRepository<object>();

            //Act
            var val = baseRepo.CreateWithIdAsync("", new object());

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void CreateWithIdAsync_NotNull()
        {
            //Arrange
            object obj = new { Name = "CreateWithIdAsync_NotNull" };
            var baseRepo = new Mock<BaseRepository<object>>();
            baseRepo.Setup(x => x.CreateWithIdAsync("Id", It.IsAny<object>())).Returns(Task.FromResult(0));

            //Act
            var val = baseRepo.Object.CreateWithIdAsync("Id", obj);

            //Assert
            baseRepo.Verify();
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Object is null")]
        [TestCategory("UnitTest")]
        public void UpdateAsync_Null()
        {
            //Arrange
            var baseRepo = new BaseRepository<object>();

            //Act
            var val = baseRepo.UpdateAsync("uid", "tid", null);

            //Assert
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "Given ID is null")]
        [TestCategory("UnitTest")]
        public void UpdateAsync_EmptyUid()
        {
            //Arrange
            var baseRepo = new BaseRepository<object>();

            //Act
            var updated = baseRepo.UpdateAsync("", "tid", new object());

            //Assert
            updated.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "Given ID is null")]
        [TestCategory("UnitTest")]
        public void UpdateAsync_EmptyId()
        {
            //Arrange
            var baseRepo = new BaseRepository<object>();

            //Act
            var updated = baseRepo.UpdateAsync("uid", "", new object());

            //Assert
            updated.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void UpdateAsync_NotNull()
        {
            // Arrange
            object obj = new { Name = "Update_Positive" };
            var baseRepo = new Mock<BaseRepository<object>>();
            baseRepo.Setup(m => m.UpdateAsync(It.IsAny<string>(), It.IsAny<string>(), (It.IsAny<object>()))).Returns(Task.FromResult(0));

            //Act
            var val = baseRepo.Object.UpdateAsync("uid", "tid", obj);

            //Assert
            baseRepo.Verify();
            val.Wait();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "Given ID is null")]
        [TestCategory("UnitTest")]
        public void GetItemAsync_EmptyId()
        {
            //Arrange
            var baseRepo = new BaseRepository<object>();

            //Act
            var val = baseRepo.GetItemAsync("").Result;

            //Assert
            val.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void GetItemAsync_NotNull()
        {
            // Arrange
            object obj = new { Name = "GetItem_Positive" };
            var baseRepo = new Mock<BaseRepository<object>>();

            //Act
            baseRepo.Setup(m => m.GetItemAsync(It.IsAny<string>()))
                        .ReturnsAsync(obj);

            //Assert
            baseRepo.Verify();
            baseRepo.Object.GetItemAsync("Id").Result.Should().BeEquivalentTo(obj);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void GetListAsync_NotNull()
        {
            // Arrange
            var lst = new List<object>
            {
                new { Name = "GetListAsync_NotNull" }
            };

            var baseRepo = new Mock<BaseRepository<object>>();
            baseRepo.Setup(m => m.GetListAsync())
               .ReturnsAsync(lst);

            //Act
            var val = baseRepo.Object.GetListAsync();

            //Assert
            baseRepo.Verify();
            val.Result.Should().BeEquivalentTo(lst);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "Given ID is null")]
        [TestCategory("UnitTest")]
        public void DeleteByIdAsync_EmptyId()
        {
            //Arrange
            var baseRepo = new BaseRepository<object>();

            //Act
            var val = baseRepo.DeleteByIdAsync("");

            //Assert
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void DeleteByIdAsync_NotNull()
        {
            // Arrange
            object obj = new { Name = "Delete_Positive" };
            var baseRepo = new Mock<BaseRepository<object>>();
            baseRepo.Setup(m => m.DeleteByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(0));

            //Act
            var val = baseRepo.Object.DeleteByIdAsync("Id");

            //Assert
            baseRepo.Verify();
            val.Wait();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void DeleteAsync_NotNull()
        {
            // Arrange
            object obj = new { Name = "Delete_Positive" };
            var baseRepo = new Mock<BaseRepository<object>>();
            baseRepo.Setup(m => m.DeleteAsync()).Returns(Task.FromResult(0));

            //Act
            var val = baseRepo.Object.DeleteAsync();

            //Assert
            baseRepo.Verify();
            val.Wait();
        }

    }
}
