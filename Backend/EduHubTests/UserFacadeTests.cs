using System;
using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Domain.Tools;
using EduHubLibrary.Facades;
using EduHubLibrary.Infrastructure;
using EduHubLibrary.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EduHubTests
{
    [TestClass]
    public class UserFacadeTests
    {
        private IGroupRepository _groupRepository;
        private IUserRepository _userRepository;
        private IKeysRepository _keysRepository;
        private EmailSender emailSender;
        private EmailSettings emailSettings;

        [TestInitialize]
        public void Initialize()
        {
            _userRepository = new InMemoryUserRepository();
            _keysRepository = new InMemoryKeysRepository();
            _groupRepository = new InMemoryGroupRepository();
            emailSettings = new EmailSettings("", "", "", "", "", 4);
            emailSender = new EmailSender(emailSettings);
        }

        [TestMethod]
        public void AddNewUser_UserAdded()
        {
            //Arrange
            var authUserFacade = new AuthUserFacade(_keysRepository, 
                _userRepository, emailSender);
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
        public void EditName_GetUserWithEditedName()
        {
            //Arrange
            var authUserFacade = new AuthUserFacade(_keysRepository,
                _userRepository, emailSender);
            var userFacade = new UserFacade(_userRepository, _groupRepository);
            var testUserId = authUserFacade.RegUser("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false,
                UserType.User);
            var testUser = userFacade.GetUser(testUserId);
            var newName = "Nikolai";

            //Act
            userFacade.EditName(testUserId, newName);
            var actualName = testUser.UserProfile.Name;

            //Assert
            Assert.AreEqual(newName, actualName);
            Assert.AreEqual(newName, _userRepository.GetUserById(testUserId).UserProfile.Name);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void EditNameWithEmptyValue_GetException()
        {
            //Arrange
            var authUserFacade = new AuthUserFacade(_keysRepository,
                _userRepository, emailSender);
            var userFacade = new UserFacade(_userRepository, _groupRepository);
            var testUserId = authUserFacade.RegUser("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false,
                UserType.User);
            var newName = "";

            //Act
            userFacade.EditName(testUserId, newName);
        }

        [TestMethod]
        public void EditAboutUser_GetUserWithEditedAboutInfo()
        {
            //Arrange
            var authUserFacade = new AuthUserFacade(_keysRepository,
                _userRepository, emailSender);
            var userFacade = new UserFacade(_userRepository, _groupRepository);
            var testUserId = authUserFacade.RegUser("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false,
                UserType.User);
            var testUser = userFacade.GetUser(testUserId);
            var newAbout = "I'm student";

            //Act
            userFacade.EditAboutUser(testUserId, newAbout);
            var actualAbout = testUser.UserProfile.AboutUser;

            //Assert
            Assert.AreEqual(newAbout, actualAbout);
            Assert.AreEqual(newAbout, _userRepository.GetUserById(testUserId).UserProfile.AboutUser);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void EditAboutInfoWithEmptyValue_GetException()
        {
            //Arrange
            var authUserFacade = new AuthUserFacade(_keysRepository,
                _userRepository, emailSender);
            var userFacade = new UserFacade(_userRepository, _groupRepository);
            var testUserId = authUserFacade.RegUser("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false,
                UserType.User);
            var newAbout = "";

            //Act
            userFacade.EditAboutUser(testUserId, newAbout);
        }

        [TestMethod]
        public void EditUserGender_GetUserWithEditedGender()
        {
            //Arrange
            var authUserFacade = new AuthUserFacade(_keysRepository,
                _userRepository, emailSender);
            var userFacade = new UserFacade(_userRepository, _groupRepository);
            var testUserId = authUserFacade.RegUser("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false,
                UserType.User);
            var testUser = userFacade.GetUser(testUserId);
            var newGender = Gender.Man;

            //Act
            userFacade.EditGender(testUserId, newGender);
            var actualGender = testUser.UserProfile.Gender;

            //Assert
            Assert.AreEqual(newGender, actualGender);
            Assert.AreEqual(newGender, _userRepository.GetUserById(testUserId).UserProfile.Gender);
        }

        [TestMethod]
        public void EditAvatarLinkOfUser_GetUserWithEditedAvatarLink()
        {
            //Arrange
            var authUserFacade = new AuthUserFacade(_keysRepository,
                _userRepository, emailSender);
            var userFacade = new UserFacade(_userRepository, _groupRepository);
            var testUserId = authUserFacade.RegUser("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false,
                UserType.User);
            var testUser = userFacade.GetUser(testUserId);
            var newAvatarLink = "new avatar";

            //Act
            userFacade.EditAvatarLink(testUserId, newAvatarLink);
            var actualAvatarLink = testUser.UserProfile.AvatarLink;

            //Assert
            Assert.AreEqual(newAvatarLink, actualAvatarLink);
            Assert.AreEqual(newAvatarLink, _userRepository.GetUserById(testUserId).UserProfile.AvatarLink);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void EditAvatarLinkOfUserWithEmptyValue_GetException()
        {
            //Arrange
            var authUserFacade = new AuthUserFacade(_keysRepository,
                _userRepository, emailSender);
            var userFacade = new UserFacade(_userRepository, _groupRepository);
            var testUserId = authUserFacade.RegUser("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false,
                UserType.User);
            var newAvatarLink = "";

            //Act
            userFacade.EditAvatarLink(testUserId, newAvatarLink);
        }

        [TestMethod]
        public void EditContactsOfUser_GetUserWithEditedContacts()
        {
            //Arrange
            var authUserFacade = new AuthUserFacade(_keysRepository,
                _userRepository, emailSender);
            var userFacade = new UserFacade(_userRepository, _groupRepository);
            var testUserId = authUserFacade.RegUser("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false,
                UserType.User);
            var testUser = userFacade.GetUser(testUserId);
            var newContacts = new List<string> {"friend", "best friend"};

            //Act
            userFacade.EditContacts(testUserId, newContacts);
            var actualContacts = testUser.UserProfile.Contacts;

            //Assert
            Assert.AreEqual(newContacts, actualContacts);
            Assert.AreEqual(newContacts, _userRepository.GetUserById(testUserId).UserProfile.Contacts);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void EditContactsOfUserWithEmptyList_GetException()
        {
            //Arrange
            var authUserFacade = new AuthUserFacade(_keysRepository,
                _userRepository, emailSender);
            var userFacade = new UserFacade(_userRepository, _groupRepository);
            var testUserId = authUserFacade.RegUser("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false,
                UserType.User);
            var newContacts = new List<string>();

            //Act
            userFacade.EditContacts(testUserId, newContacts);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void EditContactsOfUserWithListWithEmptyValue_GetException()
        {
            //Arrange
            var authUserFacade = new AuthUserFacade(_keysRepository,
                _userRepository, emailSender);
            var userFacade = new UserFacade(_userRepository, _groupRepository);
            var testUserId = authUserFacade.RegUser("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false,
                UserType.User);
            var newContacts = new List<string> {" ", "friend"};

            //Act
            userFacade.EditContacts(testUserId, newContacts);
        }

        [TestMethod]
        public void EditUserWithValidBirthday_GetUserWithEditedBirthday()
        {
            //Arrange
            var authUserFacade = new AuthUserFacade(_keysRepository,
                _userRepository, emailSender);
            var userFacade = new UserFacade(_userRepository, _groupRepository);
            var testUserId = authUserFacade.RegUser("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false,
                UserType.User);
            var testUser = userFacade.GetUser(testUserId);
            var newBirthday = 1998;

            //Act
            userFacade.EditBirthYear(testUserId, newBirthday);
            var actualBirthYear = testUser.UserProfile.BirthYear;

            //Assert
            Assert.AreEqual(newBirthday, actualBirthYear);
            Assert.AreEqual(newBirthday, _userRepository.GetUserById(testUserId).UserProfile.BirthYear);
        }

        [ExpectedException(typeof(IndexOutOfRangeException))]
        [TestMethod]
        public void EditUserWithInValidBirthday_GetException()
        {
            //Arrange
            var authUserFacade = new AuthUserFacade(_keysRepository,
                _userRepository, emailSender);
            var userFacade = new UserFacade(_userRepository, _groupRepository);
            var testUserId = authUserFacade.RegUser("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false,
                UserType.User);
            var testUser = userFacade.GetUser(testUserId);
            var newBirthday = 101998;

            //Act
            userFacade.EditBirthYear(testUserId, newBirthday);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void EditUserBirthdayWithEmptyValue_GetException()
        {
            //Arrange
            var authUserFacade = new AuthUserFacade(_keysRepository,
                _userRepository, emailSender);
            var userFacade = new UserFacade(_userRepository, _groupRepository);
            var testUserId = authUserFacade.RegUser("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false,
                UserType.User);
            var newBirthYear = "";

            //Act
            userFacade.EditAvatarLink(testUserId, newBirthYear);
        }

        [TestMethod]
        public void BecomeTeacher_IsItPossible()
        {
            //Arrange
            var authUserFacade = new AuthUserFacade(_keysRepository,
                _userRepository, emailSender);
            var userFacade = new UserFacade(_userRepository, _groupRepository);
            var testUserId = authUserFacade.RegUser("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false,
                UserType.User);
            var testUser = userFacade.GetUser(testUserId);

            //Act
            userFacade.BecomeTeacher(testUserId);

            //Assert
            Assert.AreEqual(true, testUser.UserProfile.IsTeacher);
            Assert.AreEqual(true, _userRepository.GetUserById(testUserId).UserProfile.IsTeacher);
        }

        [TestMethod]
        public void StopToTeacher_IsItPossible()
        {
            //Arrange
            var authUserFacade = new AuthUserFacade(_keysRepository,
                _userRepository, emailSender);
            var userFacade = new UserFacade(_userRepository, _groupRepository);
            var testUserId = authUserFacade.RegUser("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false,
                UserType.User);
            var testUser = userFacade.GetUser(testUserId);

            //Act
            userFacade.StopToBeTeacher(testUserId);

            //Assert
            Assert.AreEqual(false, testUser.UserProfile.IsTeacher);
            Assert.AreEqual(false, _userRepository.GetUserById(testUserId).UserProfile.IsTeacher);
        }

        [TestMethod]
        public void TryToGetAllGroupsOfUser_ReturnRightResult()
        {
            //Arrange
            var authUserFacade = new AuthUserFacade(_keysRepository,
                _userRepository, emailSender);
            var userFacade = new UserFacade(_userRepository, _groupRepository);
            var groupFacade = new GroupFacade(_groupRepository, _userRepository,
                new GroupSettings(2, 10, 0, 100));

            var testUserId = authUserFacade.RegUser("Alena", new Credentials("email1", "password"), true, UserType.User);
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
                _userRepository, emailSender);
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
                _userRepository, emailSender);
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

        [ExpectedException(typeof(UserAlreadyExistsException))]
        [TestMethod]
        public void TryToRegUserWithExistingEmail_GetException()
        {
            //Arrange
            var authUserFacade = new AuthUserFacade(_keysRepository,
                _userRepository, emailSender);
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
                new GroupSettings(1, 100, 0, 1000));
            var authUserFacade = new AuthUserFacade(_keysRepository,
                _userRepository, emailSender);
            var userFacade = new UserFacade(_userRepository, _groupRepository);

            var creatorId = authUserFacade.RegUser("Creator", new Credentials("email1", "password"), false, UserType.User);
            var teacherId = authUserFacade.RegUser("Teacher", new Credentials("email2", "password"), true, UserType.User);

            var createdGroupId = groupFacade.CreateGroup(creatorId, "Some group",
                new List<string> {"c#"}, "Very interesting", 1, 100, false, GroupType.Lecture);

            //Act
            userFacade.Invite(creatorId, teacherId, createdGroupId, MemberRole.Teacher);
            var invitations = userFacade.GetAllInvitationsForUser(teacherId).ToList();

            //Assert
            Assert.AreEqual(createdGroupId, invitations[0].GroupId);
        }

        [TestMethod]
        [ExpectedException(typeof(UserIsNotTeacher))]
        public void TryToInviteUserWithoutTeacherFlag_GetException()
        {
            //Arrange
            var groupFacade = new GroupFacade(_groupRepository, _userRepository,
                new GroupSettings(1, 100, 0, 1000));
            var authUserFacade = new AuthUserFacade(_keysRepository,
                _userRepository, emailSender);
            var userFacade = new UserFacade(_userRepository, _groupRepository);

            var creatorId = authUserFacade.RegUser("Creator", new Credentials("email1", "password"), false, UserType.User);
            var pseudoTeacherId = authUserFacade.RegUser("Pseudo teacher", new Credentials("email2", "password"), false,
                UserType.User);

            var createdGroupId = groupFacade.CreateGroup(creatorId, "Some group", new List<string> {"c#"},
                "Very interesting", 1, 100, false, GroupType.Lecture);

            //Act
            userFacade.Invite(creatorId, pseudoTeacherId, createdGroupId, MemberRole.Teacher);
        }
    }
}