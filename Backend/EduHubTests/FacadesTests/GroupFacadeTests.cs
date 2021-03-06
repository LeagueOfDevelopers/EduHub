﻿using System;
using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Facades;
using EduHubLibrary.Infrastructure;
using EduHubLibrary.Interators;
using EduHubLibrary.Mailing;
using EduHubLibrary.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EduHubTests
{
    [TestClass]
    public class GroupFacadeTests
    {
        private IAccountFacade _accountFacade;
        private User _groupCreator;
        private IGroupFacade _groupFacade;
        private ISanctionFacade _sanctionFacade;
        private IUserFacade _userFacade;
        private UserSettings userSettings;

        [TestInitialize]
        public void Initialize()
        {
            var inMemoryUserRepository = new InMemoryUserRepository();
            var inMemoryGroupRepository = new InMemoryGroupRepository();
            var inMemoryKeyRepository = new InMemoryKeysRepository();
            var inMemorySanctionRepository = new InMemorySanctionRepository();
            var inMemoryEventRepository = new InMemoryEventRepository();
            var groupSettings = new GroupSettings(2, 10, 0, 1000);
            var emailSender = new Mock<IEmailSender>();
            var publisher = new Mock<IEventPublisher>();
            userSettings = new UserSettings("");

            var adminKey = new Key("email", KeyAppointment.BecomeAdmin);
            inMemoryKeyRepository.AddKey(adminKey);

            _sanctionFacade = new SanctionFacade(inMemorySanctionRepository, inMemoryUserRepository, publisher.Object);
            _groupFacade = new GroupFacade(inMemoryGroupRepository, inMemoryUserRepository, inMemorySanctionRepository,
                new GroupSettings(3, 100, 0, 1000), publisher.Object);
            _userFacade = new UserFacade(inMemoryUserRepository, inMemoryGroupRepository, inMemoryEventRepository,
                publisher.Object);
            _accountFacade = new AccountFacade(inMemoryKeyRepository, inMemoryUserRepository,
                emailSender.Object, userSettings);
            var creatorId = _accountFacade.RegUser("Alena", new Credentials("email", "password"), true, adminKey.Value);
            _groupCreator = _userFacade.GetUser(creatorId);
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
            Assert.AreEqual(2, foundGroups.Count);
        }

        [TestMethod]
        public void CreateGroupByGroupFacadeWithValidValues_GroupWasCreated()
        {
            //Arrange
            var creatorId = _accountFacade.RegUser("Alena", Credentials.FromRawData("Email", "Password"), false);
            var title = "some group";
            var description = "some description";
            var tags = new List<string> {"c#"};
            var size = 3;
            var moneyPerUser = 100.0;

            //Act
            var groupId = _groupFacade.CreateGroup(creatorId, title, tags, description, size, moneyPerUser,
                false, GroupType.Seminar);

            //Assert
            Assert.IsNotNull(_groupFacade.GetGroup(groupId, creatorId));
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void TryToCreateGroupByNotExistingUser_GetException()
        {
            //Arrange
            var invalidUserId = IntIterator.GetNextId();

            //Act
            _groupFacade.CreateGroup(invalidUserId, "Some group", new List<string> {"c#"},
                "You're welcome!", 3, 100, false, GroupType.Lecture);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TryToCreateGroupWithInvalidSettings_GetException()
        {
            //Arrange
            var creatorId = _accountFacade.RegUser("Alena", Credentials.FromRawData("Email", "Password"), false);

            //Act
            _groupFacade.CreateGroup(creatorId, "Some group", new List<string> {"c#"},
                "You're welcome!", 2, 2000, false, GroupType.Lecture);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void TryToAddNotExistingUserToGroup_GetException()
        {
            //Arrange
            var createdGroupId = _groupFacade.CreateGroup(_groupCreator.Id, "Some group", new List<string> {"c#"},
                "You're welcome!", 3, 20, false, GroupType.Lecture);

            //Act
            _groupFacade.AddMember(createdGroupId, IntIterator.GetNextId());
        }

        [TestMethod]
        public void DeleteTeacherFromGroup_TeacherWasDeleted()
        {
            //Arrange
            var createdGroupId = _groupFacade.CreateGroup(_groupCreator.Id, "Some group", new List<string> {"c#"},
                "You're welcome!", 3, 20, false, GroupType.Lecture);
            var teacherId = _accountFacade.RegUser("Teacher", Credentials.FromRawData("email2", "password"), true);
            _groupFacade.ApproveTeacher(teacherId, createdGroupId);
            var expected = 1;
            //Act
            _groupFacade.DeleteTeacher(createdGroupId, _groupCreator.Id);

            //Assert
            Assert.AreEqual(expected, _groupFacade.GetGroup(createdGroupId, _groupCreator.Id).GroupMemberInfo.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ActionIsNotAllowWithSanctionsException))]
        public void TryToJoinTheGroupWithSanctions_GetException()
        {
            //Arrange
            var createdGroupId = _groupFacade.CreateGroup(_groupCreator.Id, "Some group", new List<string> {"c#"},
                "You're welcome!", 3, 20, false, GroupType.Lecture);
            var testUserId = _accountFacade.RegUser("Alena", Credentials.FromRawData("some email", "password"), false);
            _sanctionFacade.AddSanction("some rule", testUserId, _groupCreator.Id, SanctionType.NotAllowToJoinGroup);

            //Act
            _groupFacade.AddMember(createdGroupId, testUserId);
        }

        [TestMethod]
        public void AddNewMemberWithSanctionWithInvitation_MemberWasAdded()
        {
            //Arrange
            var createdGroupId = _groupFacade.CreateGroup(_groupCreator.Id, "Some group", new List<string> {"c#"},
                "You're welcome!", 3, 20, false, GroupType.Lecture);
            var testUserId = _accountFacade.RegUser("Alena", Credentials.FromRawData("some email", "password"), false);
            _sanctionFacade.AddSanction("some rule", testUserId, _groupCreator.Id, SanctionType.NotAllowToJoinGroup);

            _userFacade.Invite(_groupCreator.Id, testUserId, createdGroupId, MemberRole.Member);
            var invitationId = _userFacade.GetAllInvitationsForUser(testUserId).ToList()[0].Id;

            //Act
            _userFacade.ChangeInvitationStatus(testUserId, invitationId, InvitationStatus.Declined);

            //Assert
            Assert.AreEqual(1, _groupFacade.GetGroup(createdGroupId, _groupCreator.Id).GroupMemberInfo.Count());
        }
    }
}