using System;
using System.Collections.Generic;
using EduHubLibrary.Common;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Domain.Tools;
using EduHubLibrary.Facades;
using EduHubLibrary.Infrastructure;
using EduHubLibrary.Mailing;
using EduHubLibrary.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EduHubTests
{
    [TestClass]
    public class UserEditFacadeTests
    {
        private Guid _testUserId;
        private IUserEditFacade _userEditFacade;
        private IUserFacade _userFacade;

        [TestInitialize]
        public void Initialize()
        {
            var emailSettings = new EmailSettings("", "", "", "", "", 4);
            var emailSender = new EmailSender(emailSettings);
            var keysRepository = new InMemoryKeysRepository();
            var groupRepository = new InMemoryGroupRepository();
            var userRepository = new InMemoryUserRepository();
            var fileRepository = new InMemoryFileRepository();
            var authUserFacade = new AuthUserFacade(keysRepository, userRepository, emailSender);

            _userEditFacade = new UserEditFacade(userRepository, fileRepository);
            _userFacade = new UserFacade(userRepository, groupRepository);

            _testUserId = authUserFacade.RegUser("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false,
                UserType.User);
        }

        [TestMethod]
        public void EditName_GetUserWithEditedName()
        {
            //Arrange
            var testUser = _userFacade.GetUser(_testUserId);
            var newName = "Nikolai";

            //Act
            _userEditFacade.EditName(_testUserId, newName);
            var actualName = testUser.UserProfile.Name;

            //Assert
            Assert.AreEqual(newName, actualName);
            Assert.AreEqual(newName, _userFacade.GetUser(_testUserId).UserProfile.Name);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void EditNameWithEmptyValue_GetException()
        {
            //Arrange
            var newName = "";

            //Act
            _userEditFacade.EditName(_testUserId, newName);
        }

        [TestMethod]
        public void EditAboutUser_GetUserWithEditedAboutInfo()
        {
            //Arrange
            var testUser = _userFacade.GetUser(_testUserId);
            var newAbout = "I'm student";

            //Act
            _userEditFacade.EditAboutUser(_testUserId, newAbout);
            var actualAbout = testUser.UserProfile.AboutUser;

            //Assert
            Assert.AreEqual(newAbout, actualAbout);
            Assert.AreEqual(newAbout, _userFacade.GetUser(_testUserId).UserProfile.AboutUser);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void EditAboutInfoWithEmptyValue_GetException()
        {
            //Arrange
            var newAbout = "";

            //Act
            _userEditFacade.EditAboutUser(_testUserId, newAbout);
        }

        [TestMethod]
        public void EditUserGender_GetUserWithEditedGender()
        {
            //Arrange
            var testUser = _userFacade.GetUser(_testUserId);
            var newGender = Gender.Man;

            //Act
            _userEditFacade.EditGender(_testUserId, newGender);
            var actualGender = testUser.UserProfile.Gender;

            //Assert
            Assert.AreEqual(newGender, actualGender);
            Assert.AreEqual(newGender, _userFacade.GetUser(_testUserId).UserProfile.Gender);
        }

        [TestMethod]
        [ExpectedException(typeof(FileDoesNotExistException))]
        public void EditAvatarLinkOfUser_GetUserWithEditedAvatarLink()
        {
            //Arrange
            var testUser = _userFacade.GetUser(_testUserId);
            var newAvatarLink = "new avatar";

            //Act
            _userEditFacade.EditAvatarLink(_testUserId, newAvatarLink);
            var actualAvatarLink = testUser.UserProfile.AvatarLink;

            //Assert
            Assert.AreEqual(newAvatarLink, actualAvatarLink);
            Assert.AreEqual(newAvatarLink, _userFacade.GetUser(_testUserId).UserProfile.AvatarLink);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void EditAvatarLinkOfUserWithEmptyValue_GetException()
        {
            //Arrange
            var newAvatarLink = "";

            //Act
            _userEditFacade.EditAvatarLink(_testUserId, newAvatarLink);
        }

        [TestMethod]
        public void EditContactsOfUser_GetUserWithEditedContacts()
        {
            //Arrange
            var testUser = _userFacade.GetUser(_testUserId);
            var newContacts = new List<string> {"friend", "best friend"};

            //Act
            _userEditFacade.EditContacts(_testUserId, newContacts);
            var actualContacts = testUser.UserProfile.Contacts;

            //Assert
            Assert.AreEqual(newContacts, actualContacts);
            Assert.AreEqual(newContacts, _userFacade.GetUser(_testUserId).UserProfile.Contacts);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void EditContactsOfUserWithEmptyList_GetException()
        {
            //Arrange
            var newContacts = new List<string>();

            //Act
            _userEditFacade.EditContacts(_testUserId, newContacts);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void EditContactsOfUserWithListWithEmptyValue_GetException()
        {
            //Arrange
            var newContacts = new List<string> {" ", "friend"};

            //Act
            _userEditFacade.EditContacts(_testUserId, newContacts);
        }

        [TestMethod]
        public void EditUserWithValidBirthday_GetUserWithEditedBirthday()
        {
            //Arrange
            var testUser = _userFacade.GetUser(_testUserId);
            var newBirthday = 1998;

            //Act
            _userEditFacade.EditBirthYear(_testUserId, newBirthday);
            var actualBirthYear = testUser.UserProfile.BirthYear;

            //Assert
            Assert.AreEqual(newBirthday, actualBirthYear);
            Assert.AreEqual(newBirthday, _userFacade.GetUser(_testUserId).UserProfile.BirthYear);
        }

        [ExpectedException(typeof(IndexOutOfRangeException))]
        [TestMethod]
        public void EditUserWithInValidBirthday_GetException()
        {
            //Arrange
            var newBirthday = 101998;

            //Act
            _userEditFacade.EditBirthYear(_testUserId, newBirthday);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void EditUserBirthdayWithEmptyValue_GetException()
        {
            //Arrange
            var newBirthYear = "";

            //Act
            _userEditFacade.EditAvatarLink(_testUserId, newBirthYear);
        }

        [TestMethod]
        public void BecomeTeacher_IsItPossible()
        {
            //Arrange
            var testUser = _userFacade.GetUser(_testUserId);

            //Act
            _userEditFacade.BecomeTeacher(_testUserId);

            //Assert
            Assert.AreEqual(true, testUser.UserProfile.IsTeacher);
            Assert.AreEqual(true, _userFacade.GetUser(_testUserId).UserProfile.IsTeacher);
        }

        [TestMethod]
        public void StopToTeacher_IsItPossible()
        {
            //Arrange
            var testUser = _userFacade.GetUser(_testUserId);

            //Act
            _userEditFacade.StopToBeTeacher(_testUserId);

            //Assert
            Assert.AreEqual(false, testUser.UserProfile.IsTeacher);
            Assert.AreEqual(false, _userFacade.GetUser(_testUserId).UserProfile.IsTeacher);
        }
    }
}