using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Facades;
using EduHubLibrary.Facades.Views.GroupViews;
using EduHubLibrary.Infrastructure;
using EduHubLibrary.Mailing;
using EduHubLibrary.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EduHubTests
{
    [TestClass]
    public class UserFacadeTests
    {
        private Mock<IEmailSender> _emailSender;
        private IEventRepository _eventRepository;
        private IGroupRepository _groupRepository;
        private IKeysRepository _keysRepository;
        private Mock<IEventPublisher> _publisher;
        private ISanctionRepository _sanctionRepository;
        private IUserRepository _userRepository;
        private UserSettings userSettings;

        [TestInitialize]
        public void Initialize()
        {
            _userRepository = new InMemoryUserRepository();
            _keysRepository = new InMemoryKeysRepository();
            _groupRepository = new InMemoryGroupRepository();
            _sanctionRepository = new InMemorySanctionRepository();
            _eventRepository = new InMemoryEventRepository();
            _publisher = new Mock<IEventPublisher>();
            _emailSender = new Mock<IEmailSender>();
            userSettings = new UserSettings("");
        }

        [TestMethod]
        public void TryToGetAllGroupsOfUser_ReturnRightResult()
        {
            //Arrange
            var accountFacade = new AccountFacade(_keysRepository,
                _userRepository, _emailSender.Object, userSettings);
            var userFacade = new UserFacade(_userRepository, _groupRepository, _eventRepository, _publisher.Object);
            var publisher = new Mock<IEventPublisher>();
            var groupFacade = new GroupFacade(_groupRepository, _userRepository, _sanctionRepository,
                new GroupSettings(2, 10, 0, 100), publisher.Object);

            var testUserId =
                accountFacade.RegUser("Alena", new Credentials("email1", "password"), true);
            var creatorId = accountFacade.RegUser("Galya", new Credentials("email2", "password"), true);

            var createdGroupId1 = groupFacade.CreateGroup(creatorId, "Group1", new List<string> {"c#"}, "Good group", 5,
                0, false, GroupType.MasterClass);
            var createdGroupId2 = groupFacade.CreateGroup(creatorId, "Group2", new List<string> {"c#"},
                "The best group!", 7, 0, true, GroupType.Seminar);

            var createdGroup1 = groupFacade.GetGroup(createdGroupId1, creatorId);
            var createdGroup2 = groupFacade.GetGroup(createdGroupId2, creatorId);

            //Act
            groupFacade.AddMember(createdGroup1.GroupInfoView.GroupId, testUserId);
            groupFacade.AddMember(createdGroup2.GroupInfoView.GroupId, testUserId);
            var expected = new List<FullGroupView> {createdGroup1, createdGroup2};
            var groups = userFacade.GetAllGroupsOfUser(testUserId).ToList();

            //Assert
            Assert.AreEqual(groups.Count, expected.Count);
        }

        [TestMethod]
        public void TryToFindNotExistingUser_ReturnEmptyList()
        {
            //Arrange
            var accountFacade = new AccountFacade(_keysRepository,
                _userRepository, _emailSender.Object, userSettings);
            var userFacade = new UserFacade(_userRepository, _groupRepository, _eventRepository, _publisher.Object);

            accountFacade.RegUser("Alena", new Credentials("email1", "password"), true);
            accountFacade.RegUser("Galya", new Credentials("email2", "password"), true);

            //Act
            var actual = userFacade.FindUser("Grisha");

            //Assert
            Assert.AreEqual(0, actual.Count());
        }

        [TestMethod]
        public void TryToFindExistingUsers_ReturnRightListWithSorting()
        {
            //Arrange
            var accountFacade = new AccountFacade(_keysRepository,
                _userRepository, _emailSender.Object, userSettings);
            var userFacade = new UserFacade(_userRepository, _groupRepository, _eventRepository, _publisher.Object);

            var userId1 = accountFacade.RegUser("Alenka", new Credentials("email1", "password"), true);
            var userId2 = accountFacade.RegUser("Alena", new Credentials("email2", "password"), true);
            var userId3 = accountFacade.RegUser("Olena", new Credentials("email3", "password"), true);

            var expected = new List<User> {userFacade.GetUser(userId2), userFacade.GetUser(userId1)};

            //Act
            var actual = userFacade.FindUser("Alen").ToList();

            //Assert
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected.Count, actual.Count);
        }

        [TestMethod]
        public void AcceptUserInvitation_GetAddedUserToGroup()
        {
            //Arrange
            var publisher = new Mock<IEventPublisher>();
            var groupFacade = new GroupFacade(_groupRepository, _userRepository, _sanctionRepository,
                new GroupSettings(1, 100, 0, 1000), publisher.Object);
            var accountFacade = new AccountFacade(_keysRepository, _userRepository, _emailSender.Object,
                userSettings);
            var userFacade = new UserFacade(_userRepository, _groupRepository, _eventRepository, _publisher.Object);

            var creatorId = accountFacade.RegUser("Creator", new Credentials("email1", "password"), false);
            var invitedId = accountFacade.RegUser("Teacher", new Credentials("email2", "password"), true);

            var createdGroupId = groupFacade.CreateGroup(creatorId, "Some group",
                new List<string> {"c#"}, "Very interesting", 3, 100, false, GroupType.Lecture);

            userFacade.Invite(creatorId, invitedId, createdGroupId, MemberRole.Member);
            var invitation = userFacade.GetAllInvitationsForUser(invitedId).ToList()[0];

            //Act
            userFacade.ChangeInvitationStatus(invitedId, invitation.Id, InvitationStatus.Accepted);

            //Assert
            invitation = userFacade.GetAllInvitationsForUser(invitedId).ToList()[0];
            var createdGroup = _groupRepository.GetGroupById(createdGroupId);
            Assert.AreEqual(2, createdGroup.Members.Count);
            Assert.AreEqual(InvitationStatus.Accepted, invitation.Status);
        }

        [TestMethod]
        public void AcceptTeacherInvitation_GetApprovedTeacherInGroup()
        {
            //Arrange
            var publisher = new Mock<IEventPublisher>();
            var groupFacade = new GroupFacade(_groupRepository, _userRepository, _sanctionRepository,
                new GroupSettings(1, 100, 0, 1000), publisher.Object);
            var accountFacade = new AccountFacade(_keysRepository, _userRepository, _emailSender.Object,
                userSettings);
            var userFacade = new UserFacade(_userRepository, _groupRepository, _eventRepository, _publisher.Object);

            var creatorId = accountFacade.RegUser("Creator", new Credentials("email1", "password"), false);
            var teacherId = accountFacade.RegUser("Teacher", new Credentials("email2", "password"), true);

            var createdGroupId = groupFacade.CreateGroup(creatorId, "Some group",
                new List<string> {"c#"}, "Very interesting", 1, 100, false, GroupType.Lecture);

            userFacade.Invite(creatorId, teacherId, createdGroupId, MemberRole.Teacher);
            var invitation = userFacade.GetAllInvitationsForUser(teacherId).ToList()[0];

            //Act
            userFacade.ChangeInvitationStatus(teacherId, invitation.Id, InvitationStatus.Accepted);

            //Assert
            invitation = userFacade.GetAllInvitationsForUser(teacherId).ToList()[0];
            var createdGroup = _groupRepository.GetGroupById(createdGroupId);
            Assert.IsNotNull(createdGroup.Teacher);
            Assert.AreEqual(InvitationStatus.Accepted, invitation.Status);
        }

        [TestMethod]
        [ExpectedException(typeof(TeacherIsAlreadyFoundException))]
        public void TryToAcceptTeacherInvitationWithAlreadyFoundTeacher_GetException()
        {
            //Arrange
            var publisher = new Mock<IEventPublisher>();
            var groupFacade = new GroupFacade(_groupRepository, _userRepository, _sanctionRepository,
                new GroupSettings(1, 100, 0, 1000), publisher.Object);
            var accountFacade = new AccountFacade(_keysRepository, _userRepository, _emailSender.Object,
                userSettings);
            var userFacade = new UserFacade(_userRepository, _groupRepository, _eventRepository, _publisher.Object);

            var creatorId = accountFacade.RegUser("Creator", new Credentials("email1", "password"), false);
            var teacherId = accountFacade.RegUser("Teacher", new Credentials("email2", "password"), true);
            var newTeacherId = accountFacade.RegUser("Teacher", new Credentials("email3", "password"), true);

            var createdGroupId = groupFacade.CreateGroup(creatorId, "Some group",
                new List<string> {"c#"}, "Very interesting", 1, 100, false, GroupType.Lecture);

            userFacade.Invite(creatorId, newTeacherId, createdGroupId, MemberRole.Teacher);
            var invitation = userFacade.GetAllInvitationsForUser(newTeacherId).ToList()[0];

            //Act
            groupFacade.ApproveTeacher(teacherId, createdGroupId);
            userFacade.ChangeInvitationStatus(newTeacherId, invitation.Id, InvitationStatus.Accepted);
        }

        [TestMethod]
        [ExpectedException(typeof(UserAlreadyExistsException))]
        public void TryToRegUserWithExistingEmail_GetException()
        {
            //Arrange
            var accountFacade = new AccountFacade(_keysRepository,
                _userRepository, _emailSender.Object, userSettings);
            var userFacade = new UserFacade(_userRepository, _groupRepository, _eventRepository, _publisher.Object);

            //Act
            accountFacade.RegUser("Grisha", new Credentials("sokolov@mail.ru", "password1"), true);
            accountFacade.RegUser("Sasha", new Credentials("sokolov@mail.ru", "password2"), false);
        }

        [TestMethod]
        public void TryToInviteUserWithTeacherFlag_IsItPossible()
        {
            //Arrange
            var publisher = new Mock<IEventPublisher>();
            var groupFacade = new GroupFacade(_groupRepository, _userRepository, _sanctionRepository,
                new GroupSettings(1, 100, 0, 1000), publisher.Object);
            var accountFacade = new AccountFacade(_keysRepository,
                _userRepository, _emailSender.Object, userSettings);
            var userFacade = new UserFacade(_userRepository, _groupRepository, _eventRepository, _publisher.Object);

            var creatorId =
                accountFacade.RegUser("Creator", new Credentials("email1", "password"), false);
            var teacherId =
                accountFacade.RegUser("Teacher", new Credentials("email2", "password"), true);

            var createdGroupId = groupFacade.CreateGroup(creatorId, "Some group",
                new List<string> {"c#"}, "Very interesting", 1, 100, false, GroupType.Lecture);

            //Act
            userFacade.Invite(creatorId, teacherId, createdGroupId, MemberRole.Teacher);
            var invitations = userFacade.GetAllInvitationsForUser(teacherId).ToList();

            //Assert
            Assert.AreEqual(createdGroupId, invitations[0].ToGroup);
        }

        [TestMethod]
        [ExpectedException(typeof(TeacherIsAlreadyFoundException))]
        public void TryToInviteTeacherToGroupWithApprovedTeacher_GetException()
        {
            //Arrange
            var publisher = new Mock<IEventPublisher>();
            var groupFacade = new GroupFacade(_groupRepository, _userRepository, _sanctionRepository,
                new GroupSettings(1, 100, 0, 1000), publisher.Object);
            var accountFacade = new AccountFacade(_keysRepository,
                _userRepository, _emailSender.Object, userSettings);
            var userFacade = new UserFacade(_userRepository, _groupRepository, _eventRepository, _publisher.Object);

            var creatorId = accountFacade.RegUser("Creator", new Credentials("email1", "password"), false);
            var teacherId = accountFacade.RegUser("Teacher", new Credentials("email2", "password"), true);
            var anotherTeacherId =
                accountFacade.RegUser("Another teacher", new Credentials("email3", "password"), true);

            var createdGroupId = groupFacade.CreateGroup(creatorId, "Some group", new List<string> {"c#"},
                "Very interesting", 1, 100, false, GroupType.Lecture);
            var createdGroup = groupFacade.GetGroup(createdGroupId, creatorId);
            groupFacade.ApproveTeacher(userFacade.GetUser(teacherId).Id, createdGroupId);

            //Act
            userFacade.Invite(creatorId, anotherTeacherId, createdGroupId, MemberRole.Teacher);
        }

        [TestMethod]
        [ExpectedException(typeof(UserIsNotTeacher))]
        public void TryToInviteUserWithoutTeacherFlag_GetException()
        {
            //Arrange
            var publisher = new Mock<IEventPublisher>();
            var groupFacade = new GroupFacade(_groupRepository, _userRepository, _sanctionRepository,
                new GroupSettings(1, 100, 0, 1000), publisher.Object);
            var accountFacade = new AccountFacade(_keysRepository,
                _userRepository, _emailSender.Object, userSettings);
            var userFacade = new UserFacade(_userRepository, _groupRepository, _eventRepository, _publisher.Object);

            var creatorId = accountFacade.RegUser("Creator", new Credentials("email1", "password"), false);
            var pseudoTeacherId = accountFacade.RegUser("Pseudo teacher", new Credentials("email2", "password"), false);

            var createdGroupId = groupFacade.CreateGroup(creatorId, "Some group", new List<string> {"c#"},
                "Very interesting", 1, 100, false, GroupType.Lecture);

            //Act
            userFacade.Invite(creatorId, pseudoTeacherId, createdGroupId, MemberRole.Teacher);
        }
    }
}