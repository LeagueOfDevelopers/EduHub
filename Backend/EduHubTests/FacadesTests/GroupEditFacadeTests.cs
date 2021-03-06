﻿using System;
using System.Collections.Generic;
using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Facades;
using EduHubLibrary.Infrastructure;
using EduHubLibrary.Mailing;
using EduHubLibrary.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EduHubTests
{
    [TestClass]
    public class GroupEditFacadeTests
    {
        private IAccountFacade _accountFacade;
        private int _groupCreatorId;
        private IGroupEditFacade _groupEditFacade;
        private IGroupFacade _groupFacade;
        private UserSettings userSettings;

        [TestInitialize]
        public void Initialize()
        {
            var inMemoryUserRepository = new InMemoryUserRepository();
            var inMemoryGroupRepository = new InMemoryGroupRepository();
            var inMemoryKeyRepository = new InMemoryKeysRepository();
            var inMemorySanctionRepository = new InMemorySanctionRepository();
            var groupSettings = new GroupSettings(2, 10, 0, 1000);
            var emailSender = new Mock<IEmailSender>();
            var publisher = new Mock<IEventPublisher>();
            userSettings = new UserSettings("");

            _groupFacade = new GroupFacade(inMemoryGroupRepository, inMemoryUserRepository, inMemorySanctionRepository,
                new GroupSettings(3, 100, 0, 1000), publisher.Object);
            _accountFacade = new AccountFacade(inMemoryKeyRepository, inMemoryUserRepository,
                emailSender.Object, userSettings);
            _groupEditFacade = new GroupEditFacade(inMemoryGroupRepository, groupSettings, publisher.Object);
            _groupCreatorId =
                _accountFacade.RegUser("Alena", new Credentials("email", "password"), true);
        }

        [TestMethod]
        public void ChangeTitleInGroup_GetChangedTitle()
        {
            //Arrange
            var createdGroupId = _groupFacade.CreateGroup(_groupCreatorId, "Some group", new List<string> {"c#"},
                "You're welcome!", 3, 100, false, GroupType.Lecture);

            //Act
            var expectedTitle = "new title";
            _groupEditFacade.ChangeGroupTitle(createdGroupId, _groupCreatorId, expectedTitle);
            var createdGroup = _groupFacade.GetGroup(createdGroupId, _groupCreatorId);
            var actualTitle = createdGroup.GroupInfoView.Title;

            //Assert
            Assert.AreEqual(expectedTitle, actualTitle);
            Assert.AreEqual(expectedTitle, _groupFacade.GetGroup(createdGroupId, _groupCreatorId).GroupInfoView.Title);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void ChangeTitleInGroupWithEmptyValue_GetException()
        {
            //Arrange
            var createdGroupId = _groupFacade.CreateGroup(_groupCreatorId, "Some group", new List<string> {"c#"},
                "You're welcome!", 3, 100, false, GroupType.Lecture);

            //Act
            _groupEditFacade.ChangeGroupTitle(createdGroupId, _groupCreatorId, " ");
        }

        [TestMethod]
        public void ChangeDescriptionInGroup_GetChangedDescription()
        {
            //Arrange
            var createdGroupId = _groupFacade.CreateGroup(_groupCreatorId, "Some group", new List<string> {"c#"},
                "You're welcome!", 3, 100, false, GroupType.Lecture);

            //Act
            var expectedDescription = "new title";
            _groupEditFacade.ChangeGroupDescription(createdGroupId, _groupCreatorId, expectedDescription);
            var createdGroup = _groupFacade.GetGroup(createdGroupId, _groupCreatorId);
            var actualDescription = createdGroup.GroupInfoView.Description;

            //Assert
            Assert.AreEqual(expectedDescription, actualDescription);
            Assert.AreEqual(expectedDescription, _groupFacade.GetGroup(createdGroupId, _groupCreatorId).GroupInfoView.Description);
        }

        [TestMethod]
        public void ChangeTagsInGroup_GetChangedTags()
        {
            //Arrange
            var createdGroupId = _groupFacade.CreateGroup(_groupCreatorId, "Some group", new List<string> {"c#"},
                "You're welcome!", 3, 100, false, GroupType.Lecture);

            //Act
            var expectedTags = new List<string> {"c++"};
            _groupEditFacade.ChangeGroupTags(createdGroupId, _groupCreatorId, expectedTags);
            var createdGroup = _groupFacade.GetGroup(createdGroupId, _groupCreatorId);
            var actualTags = createdGroup.GroupInfoView.Tags;

            //Assert
            Assert.AreEqual(expectedTags, actualTags);
            Assert.AreEqual(expectedTags, _groupFacade.GetGroup(createdGroupId, _groupCreatorId).GroupInfoView.Tags);
        }

        [TestMethod]
        public void ChangeSizeInGroup_GetChangedSize()
        {
            //Arrange
            var createdGroupId = _groupFacade.CreateGroup(_groupCreatorId, "Some group", new List<string> {"c#"},
                "You're welcome!", 3, 100, false, GroupType.Lecture);

            //Act
            var expectedSize = 5;
            _groupEditFacade.ChangeGroupSize(createdGroupId, _groupCreatorId, expectedSize);
            var createdGroup = _groupFacade.GetGroup(createdGroupId, _groupCreatorId);
            var actualSize = createdGroup.GroupInfoView.Size;

            //Assert
            Assert.AreEqual(expectedSize, actualSize);
            Assert.AreEqual(expectedSize, _groupFacade.GetGroup(createdGroupId, _groupCreatorId).GroupInfoView.Size);
        }

        [TestMethod]
        public void ChangeMoneyPerUserInGroup_GetChangedMoneyPerUser()
        {
            //Arrange
            var createdGroupId = _groupFacade.CreateGroup(_groupCreatorId, "Some group", new List<string> {"c#"},
                "You're welcome!", 3, 100, false, GroupType.Lecture);

            //Act
            var expectedMoneyPerUser = 200;
            _groupEditFacade.ChangeGroupPrice(createdGroupId, _groupCreatorId, expectedMoneyPerUser);
            var createdGroup = _groupFacade.GetGroup(createdGroupId, _groupCreatorId);
            var actualMoneyPerUser = createdGroup.GroupInfoView.Price;

            //Assert
            Assert.AreEqual(expectedMoneyPerUser, actualMoneyPerUser);
            Assert.AreEqual(expectedMoneyPerUser, _groupFacade.GetGroup(createdGroupId, _groupCreatorId).GroupInfoView.Price);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ChangeSizeInGroupWithInvalidValue_GetException()
        {
            //Arrange
            var createdGroupId = _groupFacade.CreateGroup(_groupCreatorId, "Some group", new List<string> {"c#"},
                "You're welcome!", 3, 100, false, GroupType.Lecture);

            //Act
            _groupEditFacade.ChangeGroupSize(createdGroupId, _groupCreatorId, -4);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TryToChangeGroupSizeWithValueThatLessThanMembersCount_GetException()
        {
            //Arrange
            var createdGroupId = _groupFacade.CreateGroup(_groupCreatorId, "Some group", new List<string> {"c#"},
                "You're welcome!", 2, 100, false, GroupType.Lecture);
            var memberId = _accountFacade.RegUser("member", Credentials.FromRawData("email", "password"), true);
            _groupFacade.AddMember(createdGroupId, memberId);

            //Act
            _groupEditFacade.ChangeGroupSize(createdGroupId, _groupCreatorId, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ChangeMoneyPerUserWithInvalidValue_GetException()
        {
            //Arrange
            var createdGroupId = _groupFacade.CreateGroup(_groupCreatorId, "Some group", new List<string> {"c#"},
                "You're welcome!", 3, 100, false, GroupType.Lecture);

            //Act
            _groupEditFacade.ChangeGroupPrice(createdGroupId, _groupCreatorId, -200);
        }
    }
}