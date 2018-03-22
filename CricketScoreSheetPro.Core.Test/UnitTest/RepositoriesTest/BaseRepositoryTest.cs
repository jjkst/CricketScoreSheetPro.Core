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
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Object to create is null")]
        [TestCategory("UnitTest")]
        [TestCategory("RepositoryTest")]
        public void Create_Null()
        {
            //Arrange
            var baseRepo = new BaseRepository<object>();

            //Act
            var val = baseRepo.Create(null);

            //Assert
            val.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("RepositoryTest")]
        public void Create()
        {
            //Arrange
            var obj = new { Name = "CreateWithValidObjectTest" };
            var baseRepo = new Mock<BaseRepository<object>>();

            //Act
            baseRepo.Setup(x => x.Create(It.IsAny<object>())).Returns(obj);

            //Assert
            baseRepo.Verify();
            baseRepo.Object.Create(obj).Should().BeEquivalentTo(obj);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Object to create is null")]
        [TestCategory("UnitTest")]
        [TestCategory("RepositoryTest")]
        public void CreateWithId_Null()
        {
            //Arrange
            var baseRepo = new BaseRepository<object>();

            //Act
            baseRepo.CreateWithId("Id", null);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "Given ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("RepositoryTest")]
        public void CreateWithId_EmptyId()
        {
            //Arrange
            var baseRepo = new BaseRepository<object>();

            //Act
            baseRepo.CreateWithId("", new object());
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("RepositoryTest")]
        public void CreateWithId()
        {
            //Arrange
            object obj = new { Name = "CreateWithId_NotNull" };
            var baseRepo = new Mock<BaseRepository<object>>();

            //Act
            baseRepo.Object.CreateWithId("Id", obj);

            //Assert
            baseRepo.Verify();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentNullException), "Object is null")]
        [TestCategory("UnitTest")]
        [TestCategory("RepositoryTest")]
        public void Update_Null()
        {
            //Arrange
            var baseRepo = new BaseRepository<object>();

            //Act
            baseRepo.Update("uid", "tid", null);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "Given fieldname or id is invalid")]
        [TestCategory("UnitTest")]
        [TestCategory("RepositoryTest")]
        public void Update_EmptyUid()
        {
            //Arrange
            var baseRepo = new BaseRepository<object>();

            //Act
            baseRepo.Update("", "tid", new object());
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "Given fieldname or id is invalid")]
        [TestCategory("UnitTest")]
        [TestCategory("RepositoryTest")]
        public void Update_EmptyId()
        {
            //Arrange
            var baseRepo = new BaseRepository<object>();

            //Act
            baseRepo.Update("uid", "", new object());
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("RepositoryTest")]
        public void Update()
        {
            // Arrange
            object obj = new { Name = "Update_Positive" };
            var baseRepo = new Mock<BaseRepository<object>>();            

            //Act
            baseRepo.Object.Update("uid", "tid", obj);

            //Assert
            baseRepo.Verify();
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "Given ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("RepositoryTest")]
        public void GetItem_EmptyId()
        {
            //Arrange
            var baseRepo = new BaseRepository<object>();

            //Act
            var val = baseRepo.GetItem("");

            //Assert
            val.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("RepositoryTest")]
        public void GetItem()
        {
            // Arrange
            object obj = new { Name = "GetItem_Positive" };
            var baseRepo = new Mock<BaseRepository<object>>();

            //Act
            baseRepo.Setup(m => m.GetItem(It.IsAny<string>()))
                        .Returns(obj);

            //Assert
            baseRepo.Verify();
            baseRepo.Object.GetItem("Id").Should().BeEquivalentTo(obj);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("RepositoryTest")]
        public void GetList()
        {
            // Arrange
            var lst = new List<object>
            {
                new { Name = "GetList_NotNull" }
            };

            var baseRepo = new Mock<BaseRepository<object>>();
            baseRepo.Setup(m => m.GetList())
               .Returns(lst);

            //Act
            var val = baseRepo.Object.GetList();

            //Assert
            baseRepo.Verify();
            val.Should().BeEquivalentTo(lst);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("RepositoryTest")]
        public void GetFilteredList()
        {
            // Arrange
            var lst = new List<object>
            {
                new { Name = "GetFilteredList" }
            };

            var baseRepo = new Mock<BaseRepository<object>>();
            baseRepo.Setup(m => m.GetFilteredList(It.IsAny<string>(), It.IsAny<string>()))
               .Returns(lst);

            //Act
            var val = baseRepo.Object.GetFilteredList("fieldname", "value");

            //Assert
            baseRepo.Verify();
            val.Should().BeEquivalentTo(lst);
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "FieldName is null")]
        [TestCategory("UnitTest")]
        [TestCategory("RepositoryTest")]
        public void GetFilteredList_EmptyFieldName()
        {
            // Arrange
            var baseRepo = new BaseRepository<object>();

            //Act
            var val = baseRepo.GetFilteredList("", "value");

            //Assert
            ;
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "Value is null")]
        [TestCategory("UnitTest")]
        [TestCategory("RepositoryTest")]
        public void GetFilteredList_EmptyValue()
        {
            // Arrange
            var baseRepo = new BaseRepository<object>();

            //Act
            var val = baseRepo.GetFilteredList("fieldname", "");

            //Assert
            ;
        }

        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "Given ID is null")]
        [TestCategory("UnitTest")]
        [TestCategory("RepositoryTest")]
        public void DeleteById_EmptyId()
        {
            //Arrange
            var baseRepo = new BaseRepository<object>();

            //Act
            baseRepo.DeleteById("");
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("RepositoryTest")]
        public void DeleteById()
        {
            // Arrange
            object obj = new { Name = "Delete_Positive" };
            var baseRepo = new Mock<BaseRepository<object>>();

            //Act
            baseRepo.Object.DeleteById("Id");

            //Assert
            baseRepo.Verify();
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("RepositoryTest")]
        public void Delete_NotNull()
        {
            // Arrange
            object obj = new { Name = "Delete_Positive" };
            var baseRepo = new Mock<BaseRepository<object>>();

            //Act
            baseRepo.Object.Delete();

            //Assert
            baseRepo.Verify();
        }

    }
}
