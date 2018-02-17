using System;
using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Facades;
using EduHubLibrary.Infrastructure;
using EduHubLibrary.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EduHubTests
{
    [TestClass]
    public class GroupFacadeTests
    {
        private User _groupCreator;

        private IGroupFacade _groupFacade;
        private IGroupEditFacade _groupEditFacade;
        private IUserFacade _userFacade;
        private IAuthUserFacade _authUserFacade;

        [TestInitialize]
        public void Initialize()
        {
            var inMemoryUserRepository = new InMemoryUserRepository();
            var inMemoryGroupRepository = new InMemoryGroupRepository();
            var inMemoryKeyRepository = new InMemoryKeysRepository();
            var groupSettings = new GroupSettings(2, 10, 0, 1000);
            var emailSettings = new EmailSettings("", "", "", "", "", 4);
            var emailSender = new EmailSender(emailSettings);
            _groupFacade = new GroupFacade(inMemoryGroupRepository, inMemoryUserRepository,
                new GroupSettings(3, 100, 0, 1000));
            _userFacade = new UserFacade(inMemoryUserRepository, inMemoryGroupRepository);
            _authUserFacade = new AuthUserFacade(inMemoryKeyRepository, inMemoryUserRepository,
                emailSender);
            _groupEditFacade = new GroupEditFacade(inMemoryGroupRepository, groupSettings);
            var creatorId = _authUserFacade.RegUser("Alena", new Credentials("email", "password"), true, UserType.User);
            _groupCreator = _userFacade.GetUser(creatorId);
        }

        [TestMethod]
        public void ChangeTitleInGroup_GetChangedTitle()
        {
            //Arrange
            var createdGroupId = _groupFacade.CreateGroup(_groupCreator.Id, "Some group", new List<string> {"c#"},
                "You're welcome!", 3, 100, false, GroupType.Lecture);
            var createdGroup = _groupFacade.GetGroup(createdGroupId);

            //Act
            var expectedTitle = "new title";
            _groupEditFacade.ChangeGroupTitle(createdGroupId, _groupCreator.Id, expectedTitle);
            var actualTitle = createdGroup.GroupInfo.Title;

            //Assert
            Assert.AreEqual(expectedTitle, actualTitle);
            Assert.AreEqual(expectedTitle, _groupFacade.GetGroup(createdGroupId).GroupInfo.Title);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void ChangeTitleInGroupWithEmptyValue_GetException()
        {
            //Arrange
            var createdGroupId = _groupFacade.CreateGroup(_groupCreator.Id, "Some group", new List<string> {"c#"},
                "You're welcome!", 3, 100, false, GroupType.Lecture);

            //Act
            _groupEditFacade.ChangeGroupTitle(createdGroupId, _groupCreator.Id, " ");
        }

        [TestMethod]
        public void ChangeDescriptionInGroup_GetChangedDescription()
        {
            //Arrange
            var createdGroupId = _groupFacade.CreateGroup(_groupCreator.Id, "Some group", new List<string> {"c#"},
                "You're welcome!", 3, 100, false, GroupType.Lecture);
            var createdGroup = _groupFacade.GetGroup(createdGroupId);

            //Act
            var expectedDescription = "new title";
            _groupEditFacade.ChangeGroupDescription(createdGroupId, _groupCreator.Id, expectedDescription);
            var actualDescription = createdGroup.GroupInfo.Description;

            //Assert
            Assert.AreEqual(expectedDescription, actualDescription);
            Assert.AreEqual(expectedDescription, _groupFacade.GetGroup(createdGroupId).GroupInfo.Description);
        }

        [TestMethod]
        public void ChangeTagsInGroup_GetChangedTags()
        {
            //Arrange
            var createdGroupId = _groupFacade.CreateGroup(_groupCreator.Id, "Some group", new List<string> {"c#"},
                "You're welcome!", 3, 100, false, GroupType.Lecture);
            var createdGroup = _groupFacade.GetGroup(createdGroupId);

            //Act
            var expectedTags = new List<string> {"c++"};
            _groupEditFacade.ChangeGroupTags(createdGroupId, _groupCreator.Id, expectedTags);
            var actualTags = createdGroup.GroupInfo.Tags;

            //Assert
            Assert.AreEqual(expectedTags, actualTags);
            Assert.AreEqual(expectedTags, _groupFacade.GetGroup(createdGroupId).GroupInfo.Tags);
        }

        [TestMethod]
        public void ChangeSizeInGroup_GetChangedSize()
        {
            //Arrange
            var createdGroupId = _groupFacade.CreateGroup(_groupCreator.Id, "Some group", new List<string> {"c#"},
                "You're welcome!", 3, 100, false, GroupType.Lecture);
            var createdGroup = _groupFacade.GetGroup(createdGroupId);

            //Act
            var expectedSize = 5;
            _groupEditFacade.ChangeGroupSize(createdGroupId, _groupCreator.Id, expectedSize);
            var actualSize = createdGroup.GroupInfo.Size;

            //Assert
            Assert.AreEqual(expectedSize, actualSize);
            Assert.AreEqual(expectedSize, _groupFacade.GetGroup(createdGroupId).GroupInfo.Size);
        }

        [TestMethod]
        public void ChangeMoneyPerUserInGroup_GetChangedMoneyPerUser()
        {
            //Arrange
            var createdGroupId = _groupFacade.CreateGroup(_groupCreator.Id, "Some group", new List<string> {"c#"},
                "You're welcome!", 3, 100, false, GroupType.Lecture);
            var createdGroup = _groupFacade.GetGroup(createdGroupId);

            //Act
            var expectedMoneyPerUser = 200;
            _groupEditFacade.ChangeGroupPrice(createdGroupId, _groupCreator.Id, expectedMoneyPerUser);
            var actualMoneyPerUser = createdGroup.GroupInfo.MoneyPerUser;

            //Assert
            Assert.AreEqual(expectedMoneyPerUser, actualMoneyPerUser);
            Assert.AreEqual(expectedMoneyPerUser, _groupFacade.GetGroup(createdGroupId).GroupInfo.MoneyPerUser);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ChangeSizeInGroupWithInvalidValue_GetException()
        {
            //Arrange
            var createdGroupId = _groupFacade.CreateGroup(_groupCreator.Id, "Some group", new List<string> {"c#"},
                "You're welcome!", 3, 100, false, GroupType.Lecture);

            //Act
            _groupEditFacade.ChangeGroupSize(createdGroupId, _groupCreator.Id, -4);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ChangeMoneyPerUserWithInvalidValue_GetException()
        {
            //Arrange
            var createdGroupId = _groupFacade.CreateGroup(_groupCreator.Id, "Some group", new List<string> {"c#"},
                "You're welcome!", 3, 100, false, GroupType.Lecture);

            //Act
            _groupEditFacade.ChangeGroupPrice(createdGroupId, _groupCreator.Id, -200);
        }

        [TestMethod]
        public void FindGroupUsingTags_GetRightResultWithSorting()
        {
            //Arrange
            var tags1 = new List<string> {"Java", "C++", "C#"};
            var tags2 = new List<string> {"C#", "Pascal", "PHP", "C++"};
            var tags3 = new List<string> {"Delphi", "Java"};

            var createdGroupId1 = _groupFacade.CreateGroup(_groupCreator.Id, "Some group", tags1, "You're welcome!", 3,
                100, false, GroupType.Lecture);
            var createdGroupId2 = _groupFacade.CreateGroup(_groupCreator.Id, "Some group", tags2, "You're welcome!", 3,
                100, false, GroupType.Lecture);
            var createdGroupId3 = _groupFacade.CreateGroup(_groupCreator.Id, "Some group", tags3, "You're welcome!", 3,
                100, false, GroupType.Lecture);

            var requiredTags = new List<string> {"C++", "C#"};

            //Act
            var foundGroups = _groupFacade.FindByTags(requiredTags).ToList();

            //Assert
            Assert.AreEqual(createdGroupId1, foundGroups[0].GroupInfo.Id);
            Assert.AreEqual(createdGroupId2, foundGroups[1].GroupInfo.Id);
        }
    }
}