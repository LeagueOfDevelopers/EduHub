using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Facades;
using EduHubLibrary.Infrastructure;
using EduHubLibrary.Mailing;
using EduHubLibrary.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EduHubTests
{
    [TestClass]
    public class UserFacadeTests
    {
        private EmailSender _emailSender;
        private IGroupRepository _groupRepository;
        private IKeysRepository _keysRepository;
        private IUserRepository _userRepository;

        [TestInitialize]
        public void Initialize()
        {
            _userRepository = new InMemoryUserRepository();
            _keysRepository = new InMemoryKeysRepository();
            _groupRepository = new InMemoryGroupRepository();

            var emailSettings = new EmailSettings("", "", "", "", "", 4);
            _emailSender = new EmailSender(emailSettings);
        }

        [TestMethod]
        public void AddNewUser_UserAdded()
        {
            //Arrange
            var authUserFacade = new AuthUserFacade(_keysRepository,
                _userRepository, _emailSender);
            var userFacade = new UserFacade(_userRepository, _groupRepository);
            var expectedName = "yaroslav";
            var expectedPass = "123123";
            var expectedEmail = "bus.yaroslav@gmail.com";
            var expectedType = UserType.User;
            var expectedStatus = false;

            //Act
            var userId = authUserFacade.RegUser(expectedName, Credentials.FromRawData(expectedEmail, expectedPass),
                expectedStatus, expectedType);
            var currentUser = userFacade.GetUser(userId);

            //Assert
            Assert.AreEqual(currentUser.UserProfile.Name, expectedName);
            Assert.AreEqual(currentUser.UserProfile.IsTeacher, expectedStatus);
        }

        [TestMethod]
        public void TryToGetAllGroupsOfUser_ReturnRightResult()
        {
            //Arrange
            var authUserFacade = new AuthUserFacade(_keysRepository,
                _userRepository, _emailSender);
            var userFacade = new UserFacade(_userRepository, _groupRepository);
            var groupFacade = new GroupFacade(_groupRepository, _userRepository,
                new GroupSettings(2, 10, 0, 100), new TagsManager());

            var testUserId =
                authUserFacade.RegUser("Alena", new Credentials("email1", "password"), true, UserType.User);
            var creatorId = authUserFacade.RegUser("Galya", new Credentials("email2", "password"), true, UserType.User);

            var createdGroupId1 = groupFacade.CreateGroup(creatorId, "Group1", new List<string> {"c#"}, "Good group", 5,
                0, false, GroupType.MasterClass);
            var createdGroupId2 = groupFacade.CreateGroup(creatorId, "Group2", new List<string> {"c#"},
                "The best group!", 7, 0, true, GroupType.Seminar);

            var createdGroup1 = groupFacade.GetGroup(createdGroupId1);
            var createdGroup2 = groupFacade.GetGroup(createdGroupId2);

            //Act
            createdGroup1.AddMember(testUserId);
            createdGroup2.AddMember(testUserId);
            var expected = new List<Group> {createdGroup1, createdGroup2};
            var groups = userFacade.GetAllGroupsOfUser(testUserId).ToList();

            //Assert
            Assert.AreEqual(true, expected.SequenceEqual(groups));
        }

        [TestMethod]
        public void TryToFindNotExistingUser_ReturnEmptyList()
        {
            //Arrange
            var authUserFacade = new AuthUserFacade(_keysRepository,
                _userRepository, _emailSender);
            var userFacade = new UserFacade(_userRepository, _groupRepository);

            authUserFacade.RegUser("Alena", new Credentials("email1", "password"), true, UserType.User);
            authUserFacade.RegUser("Galya", new Credentials("email2", "password"), true, UserType.User);

            //Act
            var actual = userFacade.FindByName("Grisha");

            //Assert
            Assert.AreEqual(0, actual.Count());
        }

        [TestMethod]
        public void TryToFindExistingUsers_ReturnRightListWithSorting()
        {
            //Arrange
            var authUserFacade = new AuthUserFacade(_keysRepository,
                _userRepository, _emailSender);
            var userFacade = new UserFacade(_userRepository, _groupRepository);

            var userId1 = authUserFacade.RegUser("Alenka", new Credentials("email1", "password"), true, UserType.User);
            var userId2 = authUserFacade.RegUser("Alena", new Credentials("email2", "password"), true, UserType.User);
            var userId3 = authUserFacade.RegUser("Olena", new Credentials("email3", "password"), true, UserType.User);

            var expected = new List<User> {userFacade.GetUser(userId2), userFacade.GetUser(userId1)};

            //Act
            var actual = userFacade.FindByName("Alen").ToList();

            //Assert
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected.Count, actual.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(UserAlreadyExistsException))]
        public void TryToRegUserWithExistingEmail_GetException()
        {
            //Arrange
            var authUserFacade = new AuthUserFacade(_keysRepository,
                _userRepository, _emailSender);
            var userFacade = new UserFacade(_userRepository, _groupRepository);

            //Act
            authUserFacade.RegUser("Grisha", new Credentials("sokolov@mail.ru", "password1"), true, UserType.User);
            authUserFacade.RegUser("Sasha", new Credentials("sokolov@mail.ru", "password2"), false, UserType.User);
        }

        [TestMethod]
        public void TryToInviteUserWithTeacherFlag_IsItPossible()
        {
            //Arrange
            var groupFacade = new GroupFacade(_groupRepository, _userRepository,
                new GroupSettings(1, 100, 0, 1000), new TagsManager());
            var authUserFacade = new AuthUserFacade(_keysRepository,
                _userRepository, _emailSender);
            var userFacade = new UserFacade(_userRepository, _groupRepository);

            var creatorId =
                authUserFacade.RegUser("Creator", new Credentials("email1", "password"), false, UserType.User);
            var teacherId =
                authUserFacade.RegUser("Teacher", new Credentials("email2", "password"), true, UserType.User);

            var createdGroupId = groupFacade.CreateGroup(creatorId, "Some group",
                new List<string> {"c#"}, "Very interesting", 1, 100, false, GroupType.Lecture);

            //Act
            userFacade.Invite(creatorId, teacherId, createdGroupId, MemberRole.Teacher);
            var invitations = userFacade.GetAllInvitationsForUser(teacherId).ToList();

            //Assert
            Assert.AreEqual(createdGroupId, invitations[0].GroupId);
        }

        [TestMethod]
        [ExpectedException(typeof(TeacherIsAlreadyFoundException))]
        public void TryToInviteTeacherToGroupWithApprovedTeacher_GetException()
        {
            //Arrange
            var groupFacade = new GroupFacade(_groupRepository, _userRepository,
                new GroupSettings(1, 100, 0, 1000), new TagsManager());
            var authUserFacade = new AuthUserFacade(_keysRepository,
                _userRepository, _emailSender);
            var userFacade = new UserFacade(_userRepository, _groupRepository);

            var creatorId =
                authUserFacade.RegUser("Creator", new Credentials("email1", "password"), false, UserType.User);
            var teacherId =
                authUserFacade.RegUser("Teacher", new Credentials("email2", "password"), true, UserType.User);
            var anotherTeacherId = authUserFacade.RegUser("Another teacher", new Credentials("email3", "password"),
                true, UserType.User);

            var createdGroupId = groupFacade.CreateGroup(creatorId, "Some group",
                new List<string> {"c#"}, "Very interesting", 1, 100, false, GroupType.Lecture);
            var createdGroup = groupFacade.GetGroup(createdGroupId);
            createdGroup.ApproveTeacher(userFacade.GetUser(teacherId));

            //Act
            userFacade.Invite(creatorId, anotherTeacherId, createdGroupId, MemberRole.Teacher);
        }

        [TestMethod]
        [ExpectedException(typeof(UserIsNotTeacher))]
        public void TryToInviteUserWithoutTeacherFlag_GetException()
        {
            //Arrange
            var groupFacade = new GroupFacade(_groupRepository, _userRepository,
                new GroupSettings(1, 100, 0, 1000), new TagsManager());
            var authUserFacade = new AuthUserFacade(_keysRepository,
                _userRepository, _emailSender);
            var userFacade = new UserFacade(_userRepository, _groupRepository);

            var creatorId =
                authUserFacade.RegUser("Creator", new Credentials("email1", "password"), false, UserType.User);
            var pseudoTeacherId = authUserFacade.RegUser("Pseudo teacher", new Credentials("email2", "password"), false,
                UserType.User);

            var createdGroupId = groupFacade.CreateGroup(creatorId, "Some group", new List<string> {"c#"},
                "Very interesting", 1, 100, false, GroupType.Lecture);

            //Act
            userFacade.Invite(creatorId, pseudoTeacherId, createdGroupId, MemberRole.Teacher);
        }
    }
}