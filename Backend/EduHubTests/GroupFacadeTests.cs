using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Domain.NotificationService;
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
            
            userFacade.RegUser("Alena", new Credentials("email1", "password"), true, UserType.User);
            List<User> allUsers = userFacade.GetUsers().ToList();
            Guid userId = allUsers[0].Id;

            var tags = new List<string> { "c#" };

            groupFacade.CreateGroup(userId, "Some group", tags, "You're welcome!", 3, 100, false, GroupType.Lecture);
            List<Group> allGroups = groupFacade.GetGroups().ToList();
            var createdGroup = allGroups[0];

            //Act
            var expectedTitle = "new title";
            groupFacade.ChangeGroupTitle(createdGroup.GroupInfo.Id, userId, expectedTitle);
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

            userFacade.RegUser("Alena", new Credentials("email1", "password"), true, UserType.User);
            List<User> allUsers = userFacade.GetUsers().ToList();
            Guid userId = allUsers[0].Id;

            var tags = new List<string> { "c#" };

            groupFacade.CreateGroup(userId, "Some group", tags, "You're welcome!", 3, 100, false, GroupType.Lecture);
            List<Group> allGroups = groupFacade.GetGroups().ToList();
            var createdGroup = allGroups[0];

            //Act
            groupFacade.ChangeGroupTitle(createdGroup.GroupInfo.Id, userId, " ");
        }

        [TestMethod]
        public void TryToChangeGroupInfoDescription_IsItPossible()
        {
            //Arrange
            InMemoryUserRepository inMemoryUserRepository = new InMemoryUserRepository();
            InMemoryGroupRepository inMemoryGroupRepository = new InMemoryGroupRepository();
            GroupFacade groupFacade = new GroupFacade(inMemoryGroupRepository, inMemoryUserRepository, new GroupSettings(3, 100, 0, 1000));
            UserFacade userFacade = new UserFacade(inMemoryUserRepository, inMemoryGroupRepository);

            userFacade.RegUser("Alena", new Credentials("email1", "password"), true, UserType.User);
            List<User> allUsers = userFacade.GetUsers().ToList();
            Guid userId = allUsers[0].Id;

            var tags = new List<string> { "c#" };

            groupFacade.CreateGroup(userId, "Some group", tags, "You're welcome!", 3, 100, false, GroupType.Lecture);
            List<Group> allGroups = groupFacade.GetGroups().ToList();
            var createdGroup = allGroups[0];

            //Act
            var expectedDescription = "new title";
            groupFacade.ChangeGroupDescription(createdGroup.GroupInfo.Id, userId, expectedDescription);
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

            userFacade.RegUser("Alena", new Credentials("email1", "password"), true, UserType.User);
            List<User> allUsers = userFacade.GetUsers().ToList();
            Guid userId = allUsers[0].Id;

            var tags = new List<string> { "c#" };

            groupFacade.CreateGroup(userId, "Some group", tags, "You're welcome!", 3, 100, false, GroupType.Lecture);
            List<Group> allGroups = groupFacade.GetGroups().ToList();
            var createdGroup = allGroups[0];
            
            //Act
            var expectedTags = new List<string>();
            expectedTags.Add("c#");
            groupFacade.ChangeGroupTags(createdGroup.GroupInfo.Id, userId, expectedTags);
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

            userFacade.RegUser("Alena", new Credentials("email1", "password"), true, UserType.User);
            List<User> allUsers = userFacade.GetUsers().ToList();
            Guid userId = allUsers[0].Id;

            var tags = new List<string> { "c#" };

            groupFacade.CreateGroup(userId, "Some group", tags, "You're welcome!", 3, 100, false, GroupType.Lecture);
            List<Group> allGroups = groupFacade.GetGroups().ToList();
            var createdGroup = allGroups[0];

            //Act
            var expectedSize = 5;
            groupFacade.ChangeGroupSize(createdGroup.GroupInfo.Id, userId, expectedSize);
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

            userFacade.RegUser("Alena", new Credentials("email1", "password"), true, UserType.User);
            List<User> allUsers = userFacade.GetUsers().ToList();
            Guid userId = allUsers[0].Id;

            var tags = new List<string> { "c#" };

            groupFacade.CreateGroup(userId, "Some group", tags, "You're welcome!", 3, 100, false, GroupType.Lecture);
            List<Group> allGroups = groupFacade.GetGroups().ToList();
            var createdGroup = allGroups[0];

            //Act
            var expectedMoneyPerUser = 200;
            groupFacade.ChangeGroupPrice(createdGroup.GroupInfo.Id, userId, expectedMoneyPerUser);
            var actualMoneyPerUser = createdGroup.GroupInfo.MoneyPerUser;

            //Assert
            Assert.AreEqual(expectedMoneyPerUser, actualMoneyPerUser);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TryToSetInvalidSizeOfGroup_GetException()
        {
            //Arrange
            InMemoryUserRepository inMemoryUserRepository = new InMemoryUserRepository();
            InMemoryGroupRepository inMemoryGroupRepository = new InMemoryGroupRepository();
            GroupFacade groupFacade = new GroupFacade(inMemoryGroupRepository, inMemoryUserRepository, new GroupSettings(3, 100, 0, 1000));
            UserFacade userFacade = new UserFacade(inMemoryUserRepository, inMemoryGroupRepository);

            userFacade.RegUser("Alena", new Credentials("email1", "password"), true, UserType.User);
            List<User> allUsers = userFacade.GetUsers().ToList();
            Guid userId = allUsers[0].Id;

            var tags = new List<string> { "c#" };

            groupFacade.CreateGroup(userId, "Some group", tags, "You're welcome!", 3, 100, false, GroupType.Lecture);
            List<Group> allGroups = groupFacade.GetGroups().ToList();
            var createdGroup = allGroups[0];

            //Act
            groupFacade.ChangeGroupSize(createdGroup.GroupInfo.Id, userId, -4);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TryToSetInvalidMoneyPerUser_GetException()
        {
            //Arrange
            InMemoryUserRepository inMemoryUserRepository = new InMemoryUserRepository();
            InMemoryGroupRepository inMemoryGroupRepository = new InMemoryGroupRepository();
            GroupFacade groupFacade = new GroupFacade(inMemoryGroupRepository, inMemoryUserRepository, new GroupSettings(3, 100, 0, 1000));
            UserFacade userFacade = new UserFacade(inMemoryUserRepository, inMemoryGroupRepository);

            userFacade.RegUser("Alena", new Credentials("email1", "password"), true, UserType.User);
            List<User> allUsers = userFacade.GetUsers().ToList();
            Guid userId = allUsers[0].Id;

            var tags = new List<string> { "c#" };

            groupFacade.CreateGroup(userId, "Some group", tags, "You're welcome!", 3, 100, false, GroupType.Lecture);
            List<Group> allGroups = groupFacade.GetGroups().ToList();
            var createdGroup = allGroups[0];

            //Act
            groupFacade.ChangeGroupPrice(createdGroup.GroupInfo.Id, userId, -200);
        }

        [TestMethod]
        public void TryToFindGroupUsingTags_GetRightResult()
        {
            //Arrange
            InMemoryUserRepository inMemoryUserRepository = new InMemoryUserRepository();
            InMemoryGroupRepository inMemoryGroupRepository = new InMemoryGroupRepository();
            GroupFacade groupFacade = new GroupFacade(inMemoryGroupRepository, inMemoryUserRepository, new GroupSettings(3, 100, 0, 1000));
            UserFacade userFacade = new UserFacade(inMemoryUserRepository, inMemoryGroupRepository);

            userFacade.RegUser("Alena", new Credentials("email1", "password"), true, UserType.User);
            List<User> allUsers = userFacade.GetUsers().ToList();
            Guid userId = allUsers[0].Id;

            var tags1 = new List<string> { "Java", "C++", "C#" };
            var tags2 = new List<string> { "C#", "Pascal", "PHP", "C++" };
            var tags3 = new List<string> { "Delphi", "Java" };

            groupFacade.CreateGroup(userId, "Some group", tags1, "You're welcome!", 3, 100, false, GroupType.Lecture);
            groupFacade.CreateGroup(userId, "Some group", tags2, "You're welcome!", 3, 100, false, GroupType.Lecture);
            groupFacade.CreateGroup(userId, "Some group", tags3, "You're welcome!", 3, 100, false, GroupType.Lecture);
            List<Group> allGroups = groupFacade.GetGroups().ToList();

            List<string> requiredTags = new List<string> { "C++", "C#" };

            //Act
            List<Group> foundGroups = groupFacade.FindByTags(requiredTags).ToList();

            //Assert
            List<Group> expectedGroups = new List<Group> { allGroups[0], allGroups[1] };
            Assert.AreEqual(expectedGroups[0].GroupInfo.Id, foundGroups[0].GroupInfo.Id);
            Assert.AreEqual(expectedGroups[1].GroupInfo.Id, foundGroups[1].GroupInfo.Id);
        }
    }
}
