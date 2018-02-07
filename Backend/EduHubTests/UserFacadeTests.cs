using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EduHubLibrary.Facades;
using EduHubLibrary.Infrastructure;
using EduHubLibrary.Domain;

using System.Linq;
using EduHubLibrary.Common;
using EduHubLibrary.Domain.Tools;
using EduHubLibrary.Settings;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Domain.NotificationService;

namespace EduHubTests
{
    [TestClass]
    public class UserFacadeTests
    {
        [TestInitialize]
        public void Initialize()
        {
            _inMemoryUserRepository = new InMemoryUserRepository();
            _inMemoryGroupRepository = new InMemoryGroupRepository();
        }

        [TestMethod]
        public void AddNewUser_UserAdded()
        {
            //Arrange
            UserFacade userFacade = new UserFacade(_inMemoryUserRepository, _inMemoryGroupRepository);

            var expectedName = "yaroslav";
            var expectedPass = "123123";
            var expectedEmail = "bus.yaroslav@gmail.com";
            var expectedType = UserType.User;
            var expectedStatus = false;
            
            //Act
            var userId = userFacade.RegUser(expectedName, Credentials.FromRawData(expectedEmail, expectedPass), expectedStatus, expectedType);
            var currentUser = userFacade.GetUser(userId);
            
            //Assert
            Assert.AreEqual(currentUser.UserProfile.Name, expectedName);
            Assert.AreEqual(currentUser.UserProfile.IsTeacher, expectedStatus);
        }

        [TestMethod]
        public void EditName_GetUserWithEditedName()
        {
            //Arrange
            var userFacade = new UserFacade(_inMemoryUserRepository, _inMemoryGroupRepository);
            var testUserId = userFacade.RegUser("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false, UserType.User);
            var testUser = userFacade.GetUser(testUserId);
            var newName = "Nikolai";

            //Act
            userFacade.EditName(testUserId, newName);
            var actualName = testUser.UserProfile.Name;

            //Assert
            Assert.AreEqual(newName, actualName);
        }

        [ExpectedException(typeof(System.ArgumentException)), TestMethod]
        public void EditNameWithEmptyValue_GetException()
        {
            //Arrange
            var userFacade = new UserFacade(_inMemoryUserRepository, _inMemoryGroupRepository);
            var testUserId = userFacade.RegUser("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false, UserType.User);
            var newName = "";

            //Act
            userFacade.EditName(testUserId, newName);
        }

        [TestMethod]
        public void EditAboutUser_GetUserWithEditedAboutInfo()
        {
            //Arrange
            var userFacade = new UserFacade(_inMemoryUserRepository, _inMemoryGroupRepository);
            var testUserId = userFacade.RegUser("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false, UserType.User);
            var testUser = userFacade.GetUser(testUserId);
            var newAbout = "I'm student";

            //Act
            userFacade.EditAboutUser(testUserId, newAbout);
            var actualAbout = testUser.UserProfile.AboutUser;

            //Assert
            Assert.AreEqual(newAbout, actualAbout);
        }

        [ExpectedException(typeof(System.ArgumentException)), TestMethod]
        public void EditAboutInfoWithEmptyValue_GetException()
        {
            //Arrange
            var userFacade = new UserFacade(_inMemoryUserRepository, _inMemoryGroupRepository);
            var testUserId = userFacade.RegUser("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false, UserType.User);
            var newAbout = "";

            //Act
            userFacade.EditAboutUser(testUserId, newAbout);
        }

        [TestMethod]
        public void EditUserGender_GetUserWithEditedGender()
        {
            //Arrange
            var userFacade = new UserFacade(_inMemoryUserRepository, _inMemoryGroupRepository);
            var testUserId = userFacade.RegUser("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false, UserType.User);
            var testUser = userFacade.GetUser(testUserId);
            var newGender = false;

            //Act
            userFacade.EditGender(testUserId, newGender);
            var actualGender = testUser.UserProfile.IsMan;

            //Assert
            Assert.AreEqual(newGender, actualGender);
        }

        [TestMethod]
        public void EditAvatarLinkOfUser_GetUserWithEditedAvatarLink()
        {
            //Arrange
            var userFacade = new UserFacade(_inMemoryUserRepository, _inMemoryGroupRepository);
            var testUserId = userFacade.RegUser("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false, UserType.User);
            var testUser = userFacade.GetUser(testUserId);
            var newAvatarLink = "new avatar";

            //Act
            userFacade.EditAvatarLink(testUserId, newAvatarLink);
            var actualAvatarLink = testUser.UserProfile.AvatarLink;

            //Assert
            Assert.AreEqual(newAvatarLink, actualAvatarLink);
        }

        [ExpectedException(typeof(System.ArgumentException)), TestMethod]
        public void EditAvatarLinkOfUserWithEmptyValue_GetException()
        {
            //Arrange
            var userFacade = new UserFacade(_inMemoryUserRepository, _inMemoryGroupRepository);
            var testUserId = userFacade.RegUser("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false, UserType.User);
            var newAvatarLink = "";

            //Act
            userFacade.EditAvatarLink(testUserId, newAvatarLink);
        }

        [TestMethod]
        public void EditContactsOfUser_GetUserWithEditedContacts()
        {
            //Arrange
            var userFacade = new UserFacade(_inMemoryUserRepository, _inMemoryGroupRepository);
            var testUserId = userFacade.RegUser("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false, UserType.User);
            var testUser = userFacade.GetUser(testUserId);
            List<string> newContacts = new List<string> { "friend", "best friend" };

            //Act
            userFacade.EditContacts(testUserId, newContacts);
            var actualContacts = testUser.UserProfile.Contacts;

            //Assert
            Assert.AreEqual(newContacts, actualContacts);
        }

        [ExpectedException(typeof(System.ArgumentException)), TestMethod]
        public void EditContactsOfUserWithEmptyList_GetException()
        {
            //Arrange
            var userFacade = new UserFacade(_inMemoryUserRepository, _inMemoryGroupRepository);
            var testUserId = userFacade.RegUser("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false, UserType.User);
            List<string> newContacts = new List<string>();

            //Act
            userFacade.EditContacts(testUserId, newContacts);
        }

        /*
        [ExpectedException(typeof(System.ArgumentException)), TestMethod]
        public void EditContactsOfUserWithListWithEmptyValue_GetException()
        {
            //Arrange
            var userFacade = new UserFacade(_inMemoryUserRepository, _inMemoryGroupRepository);
            var testUserId = userFacade.RegUser("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false, UserType.User);
            List<string> newContacts = new List<string> { " ", "friend" };

            //Act
            userFacade.EditContacts(testUserId, newContacts);
        }
        */

        [TestMethod]
        public void EditUserBirthday_GetUserWithEditedBirthday()
        {
            //Arrange
            var userFacade = new UserFacade(_inMemoryUserRepository, _inMemoryGroupRepository);
            var testUserId = userFacade.RegUser("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false, UserType.User);
            var testUser = userFacade.GetUser(testUserId);
            var newBirthday = "10.10.1998";

            //Act
            userFacade.EditBirthYear(testUserId, newBirthday);
            var actualBirthYear = testUser.UserProfile.BirthYear;

            //Assert
            Assert.AreEqual(newBirthday, actualBirthYear);
        }

        [ExpectedException(typeof(System.ArgumentException)), TestMethod]
        public void EditUserBirthdayWithEmptyValue_GetException()
        {
            //Arrange
            var userFacade = new UserFacade(_inMemoryUserRepository, _inMemoryGroupRepository);
            var testUserId = userFacade.RegUser("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false, UserType.User);
            var newBirthYear = "";

            //Act
            userFacade.EditAvatarLink(testUserId, newBirthYear);
        }

        [TestMethod]
        public void BecomeTeacher_IsItPossible()
        {
            //Arrange
            var userFacade = new UserFacade(_inMemoryUserRepository, _inMemoryGroupRepository);
            var testUserId = userFacade.RegUser("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false, UserType.User);
            var testUser = userFacade.GetUser(testUserId);

            //Act
            userFacade.BecomeTeacher(testUserId);

            //Assert
            Assert.AreEqual(true, testUser.UserProfile.IsTeacher);
        }

        [TestMethod]
        public void StopToTeacher_IsItPossible()
        {
            //Arrange
            var userFacade = new UserFacade(_inMemoryUserRepository, _inMemoryGroupRepository);
            var testUserId = userFacade.RegUser("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false, UserType.User);
            var testUser = userFacade.GetUser(testUserId);

            //Act
            userFacade.StopToBeTeacher(testUserId);

            //Assert
            Assert.AreEqual(false, testUser.UserProfile.IsTeacher);
        }

        [TestMethod]
        public void TryToGetAllGroupsOfUser_ReturnRightResult()
        {
            //Arrange
            var userFacade = new UserFacade(_inMemoryUserRepository, _inMemoryGroupRepository);
            var groupFacade = new GroupFacade(_inMemoryGroupRepository, _inMemoryUserRepository, new GroupSettings(2, 10, 0, 100));

            var testUserId = userFacade.RegUser("Alena", new Credentials("email1", "password"), true, UserType.User);
            var creatorId = userFacade.RegUser("Galya", new Credentials("email2", "password"), true, UserType.User);
            
            var createdGroupId1 = groupFacade.CreateGroup(creatorId, "Group1", new List<string> { "c#" }, "Good group", 5, 0, false, GroupType.MasterClass);
            var createdGroupId2 = groupFacade.CreateGroup(creatorId, "Group2", new List<string> { "c#" }, "The best group!", 7, 0, true, GroupType.Seminar);

            var createdGroup1 = groupFacade.GetGroup(createdGroupId1);
            var createdGroup2 = groupFacade.GetGroup(createdGroupId2);
            
            //Act
            createdGroup1.AddMember(testUserId);
            createdGroup2.AddMember(testUserId);
            List<Group> expected = new List<Group> { createdGroup1, createdGroup2 };
            List<Group> groups = userFacade.GetAllGroupsOfUser(testUserId).ToList();

            //Assert
            Assert.AreEqual(true, expected.SequenceEqual(groups));
        }

        [TestMethod]
        public void TryToFindAnyExistingUserViaFullName_GetRightResult()
        {
            //Arrange
            var userFacade = new UserFacade(_inMemoryUserRepository, _inMemoryGroupRepository);

            userFacade.RegUser("Alena", new Credentials("email1", "password"), true, UserType.User);
            userFacade.RegUser("Galya", new Credentials("email2", "password"), true, UserType.User);

            //Act
            var actual = userFacade.DoesUserExist("Alena");
            var expected = true;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TryToFindAnyExistingUserViaPartOfName_ReturnTrue()
        {
            //Arrange
            var userFacade = new UserFacade(_inMemoryUserRepository, _inMemoryGroupRepository);

            userFacade.RegUser("Alena", new Credentials("email1", "password"), true, UserType.User);
            userFacade.RegUser("Galya", new Credentials("email2", "password"), true, UserType.User);

            //Act
            var actual = userFacade.DoesUserExist("Gal");
            var expected = true;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TryToFindNotExistingUser_ReturnFalse()
        {
            //Arrange
            var userFacade = new UserFacade(_inMemoryUserRepository, _inMemoryGroupRepository);

            userFacade.RegUser("Alena", new Credentials("email1", "password"), true, UserType.User);
            userFacade.RegUser("Galya", new Credentials("email2", "password"), true, UserType.User);

            //Act
            var actual = userFacade.DoesUserExist("Grisha");
            var expected = false;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TryToGetFoundUsers_ReturnRightListWithSorting()
        {
            //Arrange
            var userFacade = new UserFacade(_inMemoryUserRepository, _inMemoryGroupRepository);

            var userId1 = userFacade.RegUser("Alenka", new Credentials("email1", "password"), true, UserType.User);
            var userId2 = userFacade.RegUser("Alena", new Credentials("email2", "password"), true, UserType.User);
            var userId3 = userFacade.RegUser("Olena", new Credentials("email3", "password"), true, UserType.User);

            List<User> expected = new List<User> { userFacade.GetUser(userId2), userFacade.GetUser(userId1) };

            //Act
            List<User> actual = userFacade.FindByName("Alen").ToList();

            //Assert
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected.Count, actual.Count);
        }
        
        [ExpectedException(typeof(UserAlreadyExistsException)), TestMethod]
        public void TryToRegUserWithExistingEmail_GetException()
        {
            //Arrange
            var userFacade = new UserFacade(_inMemoryUserRepository, _inMemoryGroupRepository);

            //Act
            userFacade.RegUser("Grisha", new Credentials("sokolov@mail.ru", "password1"), true, UserType.User);
            userFacade.RegUser("Sasha", new Credentials("sokolov@mail.ru", "password2"), false, UserType.User);
        }

        [TestMethod]
        public void TryToInviteUserWithTeacherFlag_IsItPossible()
        {
            //Arrange
            var groupFacade = new GroupFacade(_inMemoryGroupRepository, _inMemoryUserRepository, new GroupSettings(1, 100, 0, 1000));
            var userFacade = new UserFacade(_inMemoryUserRepository, _inMemoryGroupRepository);

            var creatorId = userFacade.RegUser("Creator", new Credentials("email1", "password"), false, UserType.User);
            var teacherId = userFacade.RegUser("Teacher", new Credentials("email2", "password"), true, UserType.User);

            var createdGroupId = groupFacade.CreateGroup(creatorId, "Some group", 
                new List<string> { "c#" }, "Very interesting", 1, 100, false, GroupType.Lecture);

            //Act
            userFacade.Invite(creatorId, teacherId, createdGroupId, MemberRole.Teacher);
            List<Invitation> invitations = userFacade.GetAllInvitationsForUser(teacherId).ToList();

            //Assert
            Assert.AreEqual(createdGroupId, invitations[0].GroupId);
        }

        [TestMethod]
        [ExpectedException(typeof(UserIsNotTeacher))]
        public void TryToInviteUserWithoutTeacherFlag_GetException()
        {
            //Arrange
            var groupFacade = new GroupFacade(_inMemoryGroupRepository, _inMemoryUserRepository, new GroupSettings(1, 100, 0, 1000));
            var userFacade = new UserFacade(_inMemoryUserRepository, _inMemoryGroupRepository);

            var creatorId = userFacade.RegUser("Creator", new Credentials("email1", "password"), false, UserType.User);
            var pseudoTeacherId = userFacade.RegUser("Pseudo teacher", new Credentials("email2", "password"), false, UserType.User);

            var createdGroupId = groupFacade.CreateGroup(creatorId, "Some group", new List<string> { "c#" }, "Very interesting", 1, 100, false, GroupType.Lecture);
           
            //Act
            userFacade.Invite(creatorId, pseudoTeacherId, createdGroupId, MemberRole.Teacher);
        }

        //This function has not yet been implemented

        /*
        [TestMethod]
        public void TryToInviteUser_GetAddedInvitationInGroup()
        {
            //Arrange
            InMemoryUserRepository inMemoryUserRepository = new InMemoryUserRepository();
            InMemoryGroupRepository inMemoryGroupRepository = new InMemoryGroupRepository();
            GroupFacade groupFacade = new GroupFacade(inMemoryGroupRepository, inMemoryUserRepository, new GroupSettings(1, 100, 0, 1000));
            UserFacade userFacade = new UserFacade(inMemoryUserRepository, inMemoryGroupRepository);

            userFacade.RegUser("Creator", new Credentials("email1", "password"), false, UserType.User);
            userFacade.RegUser("Invited", new Credentials("email2", "password"), false, UserType.User);
            List<User> allUsers = userFacade.GetUsers().ToList();
            Guid creatorId = allUsers[0].Id;
            Guid invitedId = allUsers[1].Id;

            var tags = new List<string>();
            tags.Add("js");

            Guid groupId = groupFacade.CreateGroup(creatorId, "Some group", tags, "Very interesting", 1, 100, false, GroupType.Lecture);
            List<Group> allGroups = groupFacade.GetGroups().ToList();
            var createdGroupId = allGroups[0].GroupInfo.Id;

            //Act
            userFacade.Invite(creatorId, invitedId, createdGroupId, MemberRole.Member);

            //Assert
            Assert.AreEqual(groupFacade.GetGroup(groupId).GetAllInvitation().Count, 1);
        }
        */

        private InMemoryUserRepository _inMemoryUserRepository;
        private InMemoryGroupRepository _inMemoryGroupRepository;
    }
}
