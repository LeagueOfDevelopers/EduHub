using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Facades;
using EduHubLibrary.Infrastructure;
using EduHubLibrary.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EduHubTests
{
    [TestClass]
    public class GroupFacadeTests
    {
        [TestMethod]
        public void TryToChangeGroupInfoTitle_IsItPossible()
        {
            //Arrange
            InMemoryUserRepository inMemoryUserRepository = new InMemoryUserRepository();
            InMemoryGroupRepository inMemoryGroupRepository = new InMemoryGroupRepository();
            GroupFacade groupFacade = new GroupFacade(inMemoryGroupRepository, inMemoryUserRepository, new GroupSettings(3, 100, 0, 1000));
            UserFacade userFacade = new UserFacade(inMemoryUserRepository, inMemoryGroupRepository);
            
            userFacade.RegUser("Alena", new Credentials("email1", "password"), true, TypeOfUser.User, "avatar.ru");
            List<User> allUsers = userFacade.GetUsers().ToList();
            Guid userId = allUsers[0].Id;

            var title = "some group";
            var description = "some description";
            var tags = new List<string>();
            var size = 3;
            var moneyPerUser = 100.0;
            tags.Add("js");
            GroupInfo info = new GroupInfo(Guid.NewGuid(), title, description, tags, GroupType.Lecture, false, true, size, moneyPerUser);

            groupFacade.CreateGroup(userId, title, tags, description, size, moneyPerUser, false, GroupType.Lecture);
            List<Group> allGroups = groupFacade.GetGroups().ToList();
            var createdGroup = allGroups[0];

            //Act
            var expectedTitle = "new title";
            groupFacade.ChangeTitleOfGroup(createdGroup.GroupInfo.Id, userId, expectedTitle);
            var actualTitle = createdGroup.GroupInfo.Title;

            //Assert
            Assert.AreEqual(expectedTitle, actualTitle);
        }

        [ExpectedException(typeof(System.ArgumentException)), TestMethod]
        public void TryToChangeGroupInfoTitleWithEmptyValue_GetException()
        {
            //Arrange
            InMemoryUserRepository inMemoryUserRepository = new InMemoryUserRepository();
            InMemoryGroupRepository inMemoryGroupRepository = new InMemoryGroupRepository();
            GroupFacade groupFacade = new GroupFacade(inMemoryGroupRepository, inMemoryUserRepository, new GroupSettings(3, 100, 0, 1000));
            UserFacade userFacade = new UserFacade(inMemoryUserRepository, inMemoryGroupRepository);

            userFacade.RegUser("Alena", new Credentials("email1", "password"), true, TypeOfUser.User, "avatar.ru");
            List<User> allUsers = userFacade.GetUsers().ToList();
            Guid userId = allUsers[0].Id;

            var title = "some group";
            var description = "some description";
            var tags = new List<string>();
            var size = 3;
            var moneyPerUser = 100.0;
            tags.Add("js");
            GroupInfo info = new GroupInfo(Guid.NewGuid(), title, description, tags, GroupType.Lecture, false, true, size, moneyPerUser);

            groupFacade.CreateGroup(userId, title, tags, description, size, moneyPerUser, false, GroupType.Lecture);
            List<Group> allGroups = groupFacade.GetGroups().ToList();
            var createdGroup = allGroups[0];

            //Act
            groupFacade.ChangeTitleOfGroup(createdGroup.GroupInfo.Id, userId, " ");
        }

        [TestMethod]
        public void TryToChangeGroupInfoDescription_IsItPossible()
        {
            //Arrange
            InMemoryUserRepository inMemoryUserRepository = new InMemoryUserRepository();
            InMemoryGroupRepository inMemoryGroupRepository = new InMemoryGroupRepository();
            GroupFacade groupFacade = new GroupFacade(inMemoryGroupRepository, inMemoryUserRepository, new GroupSettings(3, 100, 0, 1000));
            UserFacade userFacade = new UserFacade(inMemoryUserRepository, inMemoryGroupRepository);

            userFacade.RegUser("Alena", new Credentials("email1", "password"), true, TypeOfUser.User, "avatar.ru");
            List<User> allUsers = userFacade.GetUsers().ToList();
            Guid userId = allUsers[0].Id;

            var title = "some group";
            var description = "some description";
            var tags = new List<string>();
            var size = 3;
            var moneyPerUser = 100.0;
            tags.Add("js");
            GroupInfo info = new GroupInfo(Guid.NewGuid(), title, description, tags, GroupType.Lecture, false, true, size, moneyPerUser);

            groupFacade.CreateGroup(userId, title, tags, description, size, moneyPerUser, false, GroupType.Lecture);
            List<Group> allGroups = groupFacade.GetGroups().ToList();
            var createdGroup = allGroups[0];

            //Act
            var expectedDescription = "new title";
            groupFacade.ChangeDescriptionOfGroup(createdGroup.GroupInfo.Id, userId, expectedDescription);
            var actualDescription = createdGroup.GroupInfo.Description;

            //Assert
            Assert.AreEqual(expectedDescription, actualDescription);
        }

        [TestMethod]
        public void TryToChangeGroupInfoTags_IsItPossible()
        {
            //Arrange
            InMemoryUserRepository inMemoryUserRepository = new InMemoryUserRepository();
            InMemoryGroupRepository inMemoryGroupRepository = new InMemoryGroupRepository();
            GroupFacade groupFacade = new GroupFacade(inMemoryGroupRepository, inMemoryUserRepository, new GroupSettings(3, 100, 0, 1000));
            UserFacade userFacade = new UserFacade(inMemoryUserRepository, inMemoryGroupRepository);

            userFacade.RegUser("Alena", new Credentials("email1", "password"), true, TypeOfUser.User, "avatar.ru");
            List<User> allUsers = userFacade.GetUsers().ToList();
            Guid userId = allUsers[0].Id;

            var title = "some group";
            var description = "some description";
            var tags = new List<string>();
            var size = 3;
            var moneyPerUser = 100.0;
            tags.Add("js");
            GroupInfo info = new GroupInfo(Guid.NewGuid(), title, description, tags, GroupType.Lecture, false, true, size, moneyPerUser);

            groupFacade.CreateGroup(userId, title, tags, description, size, moneyPerUser, false, GroupType.Lecture);
            List<Group> allGroups = groupFacade.GetGroups().ToList();
            var createdGroup = allGroups[0];
            
            //Act
            var expectedTags = new List<string>();
            expectedTags.Add("c#");
            groupFacade.ChangeTagsOfGroup(createdGroup.GroupInfo.Id, userId, expectedTags);
            var actualTags = createdGroup.GroupInfo.Tags;

            //Assert
            Assert.AreEqual(expectedTags, actualTags);
        }

        [TestMethod]
        public void TryToChangeGroupInfoSize_IsItPossible()
        {
            //Arrange
            InMemoryUserRepository inMemoryUserRepository = new InMemoryUserRepository();
            InMemoryGroupRepository inMemoryGroupRepository = new InMemoryGroupRepository();
            GroupFacade groupFacade = new GroupFacade(inMemoryGroupRepository, inMemoryUserRepository, new GroupSettings(3, 100, 0, 1000));
            UserFacade userFacade = new UserFacade(inMemoryUserRepository, inMemoryGroupRepository);

            userFacade.RegUser("Alena", new Credentials("email1", "password"), true, TypeOfUser.User, "avatar.ru");
            List<User> allUsers = userFacade.GetUsers().ToList();
            Guid userId = allUsers[0].Id;

            var title = "some group";
            var description = "some description";
            var tags = new List<string>();
            var size = 3;
            var moneyPerUser = 100.0;
            tags.Add("js");
            GroupInfo info = new GroupInfo(Guid.NewGuid(), title, description, tags, GroupType.Lecture, false, true, size, moneyPerUser);

            groupFacade.CreateGroup(userId, title, tags, description, size, moneyPerUser, false, GroupType.Lecture);
            List<Group> allGroups = groupFacade.GetGroups().ToList();
            var createdGroup = allGroups[0];

            //Act
            var expectedSize = 5;
            groupFacade.ChangeSizeOfGroup(createdGroup.GroupInfo.Id, userId, expectedSize);
            var actualSize = createdGroup.GroupInfo.Size;

            //Assert
            Assert.AreEqual(expectedSize, actualSize);
        }

        [TestMethod]
        public void TryToChangeGroupInfoMoneyPerUser_IsItPossible()
        {
            //Arrange
            InMemoryUserRepository inMemoryUserRepository = new InMemoryUserRepository();
            InMemoryGroupRepository inMemoryGroupRepository = new InMemoryGroupRepository();
            GroupFacade groupFacade = new GroupFacade(inMemoryGroupRepository, inMemoryUserRepository, new GroupSettings(3, 100, 0, 1000));
            UserFacade userFacade = new UserFacade(inMemoryUserRepository, inMemoryGroupRepository);

            userFacade.RegUser("Alena", new Credentials("email1", "password"), true, TypeOfUser.User, "avatar.ru");
            List<User> allUsers = userFacade.GetUsers().ToList();
            Guid userId = allUsers[0].Id;

            var title = "some group";
            var description = "some description";
            var tags = new List<string>();
            var size = 3;
            var moneyPerUser = 100.0;
            tags.Add("js");
            GroupInfo info = new GroupInfo(Guid.NewGuid(), title, description, tags, GroupType.Lecture, false, true, size, moneyPerUser);

            groupFacade.CreateGroup(userId, title, tags, description, size, moneyPerUser, false, GroupType.Lecture);
            List<Group> allGroups = groupFacade.GetGroups().ToList();
            var createdGroup = allGroups[0];

            //Act
            var expectedMoneyPerUser = 200;
            groupFacade.ChangePriceInGroup(createdGroup.GroupInfo.Id, userId, expectedMoneyPerUser);
            var actualMoneyPerUser = createdGroup.GroupInfo.MoneyPerUser;

            //Assert
            Assert.AreEqual(expectedMoneyPerUser, actualMoneyPerUser);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidGroupInfo))]
        public void TryToSetInvalidSizeOfGroup_GetException()
        {
            //Arrange
            InMemoryUserRepository inMemoryUserRepository = new InMemoryUserRepository();
            InMemoryGroupRepository inMemoryGroupRepository = new InMemoryGroupRepository();
            GroupFacade groupFacade = new GroupFacade(inMemoryGroupRepository, inMemoryUserRepository, new GroupSettings(3, 100, 0, 1000));
            UserFacade userFacade = new UserFacade(inMemoryUserRepository, inMemoryGroupRepository);

            userFacade.RegUser("Alena", new Credentials("email1", "password"), true, TypeOfUser.User, "avatar.ru");
            List<User> allUsers = userFacade.GetUsers().ToList();
            Guid userId = allUsers[0].Id;

            var title = "some group";
            var description = "some description";
            var tags = new List<string>();
            var size = 3;
            var moneyPerUser = 100.0;
            tags.Add("js");
            GroupInfo info = new GroupInfo(Guid.NewGuid(), title, description, tags, GroupType.Lecture, false, true, size, moneyPerUser);

            groupFacade.CreateGroup(userId, title, tags, description, size, moneyPerUser, false, GroupType.Lecture);
            List<Group> allGroups = groupFacade.GetGroups().ToList();
            var createdGroup = allGroups[0];

            //Act
            groupFacade.ChangeSizeOfGroup(createdGroup.GroupInfo.Id, userId, -4);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidGroupInfo))]
        public void TryToSetInvalidMoneyPerUser_GetException()
        {
            //Arrange
            InMemoryUserRepository inMemoryUserRepository = new InMemoryUserRepository();
            InMemoryGroupRepository inMemoryGroupRepository = new InMemoryGroupRepository();
            GroupFacade groupFacade = new GroupFacade(inMemoryGroupRepository, inMemoryUserRepository, new GroupSettings(3, 100, 0, 1000));
            UserFacade userFacade = new UserFacade(inMemoryUserRepository, inMemoryGroupRepository);

            userFacade.RegUser("Alena", new Credentials("email1", "password"), true, TypeOfUser.User, "avatar.ru");
            List<User> allUsers = userFacade.GetUsers().ToList();
            Guid userId = allUsers[0].Id;

            var title = "some group";
            var description = "some description";
            var tags = new List<string>();
            var size = 3;
            var moneyPerUser = 100.0;
            tags.Add("js");
            GroupInfo info = new GroupInfo(Guid.NewGuid(), title, description, tags, GroupType.Lecture, false, true, size, moneyPerUser);

            groupFacade.CreateGroup(userId, title, tags, description, size, moneyPerUser, false, GroupType.Lecture);
            List<Group> allGroups = groupFacade.GetGroups().ToList();
            var createdGroup = allGroups[0];

            //Act
            groupFacade.ChangePriceInGroup(createdGroup.GroupInfo.Id, userId, -200);
        }
    }
}
