using System;
using System.Collections.Generic;
using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Facades;
using EduHubLibrary.Infrastructure;
using EduHubLibrary.Mailing;
using EduHubLibrary.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EduHubTests
{
    [TestClass]
    public class GroupEditFacadeTests
    {
        private IAuthUserFacade _authUserFacade;

        private Guid _groupCreatorId;
        private IGroupEditFacade _groupEditFacade;

        private IGroupFacade _groupFacade;

        [TestInitialize]
        public void Initialize()
        {
            var inMemoryUserRepository = new InMemoryUserRepository();
            var inMemoryGroupRepository = new InMemoryGroupRepository();
            var inMemoryKeyRepository = new InMemoryKeysRepository();
            var tagsManager = new TagsManager();
            var groupSettings = new GroupSettings(2, 10, 0, 1000);
            var emailSettings = new EmailSettings("", "", "", "", "", 4);
            var emailSender = new EmailSender(emailSettings);
            _groupFacade = new GroupFacade(inMemoryGroupRepository, inMemoryUserRepository,
                new GroupSettings(3, 100, 0, 1000), tagsManager);
            _authUserFacade = new AuthUserFacade(inMemoryKeyRepository, inMemoryUserRepository,
                emailSender);
            _groupEditFacade = new GroupEditFacade(inMemoryGroupRepository, groupSettings, tagsManager);
            _groupCreatorId =
                _authUserFacade.RegUser("Alena", new Credentials("email", "password"), true, UserType.User);
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
            var createdGroup = _groupFacade.GetGroup(createdGroupId);
            var actualTitle = createdGroup.GroupInfoView.Title;

            //Assert
            Assert.AreEqual(expectedTitle, actualTitle);
            Assert.AreEqual(expectedTitle, _groupFacade.GetGroup(createdGroupId).GroupInfoView.Title);
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
            var createdGroup = _groupFacade.GetGroup(createdGroupId);
            var actualDescription = createdGroup.GroupInfoView.Description;

            //Assert
            Assert.AreEqual(expectedDescription, actualDescription);
            Assert.AreEqual(expectedDescription, _groupFacade.GetGroup(createdGroupId).GroupInfoView.Description);
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
            var createdGroup = _groupFacade.GetGroup(createdGroupId);
            var actualTags = createdGroup.GroupInfoView.Tags;

            //Assert
            Assert.AreEqual(expectedTags, actualTags);
            Assert.AreEqual(expectedTags, _groupFacade.GetGroup(createdGroupId).GroupInfoView.Tags);
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
            var createdGroup = _groupFacade.GetGroup(createdGroupId);
            var actualSize = createdGroup.GroupInfoView.Size;

            //Assert
            Assert.AreEqual(expectedSize, actualSize);
            Assert.AreEqual(expectedSize, _groupFacade.GetGroup(createdGroupId).GroupInfoView.Size);
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
            var createdGroup = _groupFacade.GetGroup(createdGroupId);
            var actualMoneyPerUser = createdGroup.GroupInfoView.Price;

            //Assert
            Assert.AreEqual(expectedMoneyPerUser, actualMoneyPerUser);
            Assert.AreEqual(expectedMoneyPerUser, _groupFacade.GetGroup(createdGroupId).GroupInfoView.Price);
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