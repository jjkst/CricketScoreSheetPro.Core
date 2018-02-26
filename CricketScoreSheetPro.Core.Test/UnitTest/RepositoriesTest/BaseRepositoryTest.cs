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
        public void GetListAsyncTest()
        {
            //Arrange                     
            var mockBaseRepository = new Mock<IRepository<object>>();

            //Act
            mockBaseRepository.Setup(m => m.GetListAsync()).ReturnsAsync(new List<FirebaseObject<object>>());

            //Assert
            mockBaseRepository.Verify();
            mockBaseRepository.Object.GetListAsync().Result.Count.Should().Be(0);
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

    }
}
