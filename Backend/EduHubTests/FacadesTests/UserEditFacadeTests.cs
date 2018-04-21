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
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.NotificationService;
using Moq;

namespace EduHubTests
{
    [TestClass]
    public class UserEditFacadeTests
    {
        private int _testUserId;
        private int _adminId;
        private IUserRepository _userRepository;
        private ISanctionRepository _sanctionRepository;
        private IUserEditFacade _userEditFacade;
        private IUserFacade _userFacade;
        private Mock<IEventPublisher> _publisher;

        [TestInitialize]
        public void Initialize()
        {
            var emailSender = new Mock<IEmailSender>();
            var keysRepository = new InMemoryKeysRepository();
            var groupRepository = new InMemoryGroupRepository();
            _userRepository = new InMemoryUserRepository();
            var fileRepository = new InMemoryFileRepository();
            _sanctionRepository = new InMemorySanctionRepository();
            var adminKey = new Key("adminEmail", KeyAppointment.BecomeModerator);
            keysRepository.AddKey(adminKey);
            var accountFacade = new AccountFacade(keysRepository, _userRepository, emailSender.Object);

            _publisher = new Mock<IEventPublisher>();
            _userEditFacade = new UserEditFacade(_userRepository, fileRepository, _sanctionRepository);
            _userFacade = new UserFacade(_userRepository, groupRepository, keysRepository, _publisher.Object);

            _adminId = accountFacade.RegUser("admin", Credentials.FromRawData("adminEmail", "password"), false, adminKey.Value);
            _testUserId = accountFacade.RegUser("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false);
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
        public void EditAvatarLinkOfUserWithNotExistingLink_GetException()
        {
            //Arrange
            var testUser = _userFacade.GetUser(_testUserId);
            var newAvatarLink = "new avatar";

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

        [TestMethod]
        public void EditContactsOfUserWithEmptyList_GetEmptyContacts()
        {
            //Arrange
            var newContacts = new List<string>();

            //Act
            _userEditFacade.EditContacts(_testUserId, newContacts);

            //Assert
            Assert.AreEqual(0, _userFacade.GetUser(_testUserId).UserProfile.Contacts.Count);
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
        
        [TestMethod]
        [ExpectedException(typeof(ActionIsNotAllowWithSanctionsException))]
        public void TryToEditNameWithSanctions_GetException()
        {
            //Arrange
            var sanctionFacade = new SanctionFacade(_sanctionRepository, _userRepository, _publisher.Object);
            sanctionFacade.AddSanction("some rule", _testUserId, _adminId, SanctionType.NotAllowToEditProfile);

            //Act
            _userEditFacade.EditName(_testUserId, "new");
        }

        [TestMethod]
        [ExpectedException(typeof(ActionIsNotAllowWithSanctionsException))]
        public void TryToEditAboutUserWithSanctions_GetException()
        {
            //Arrange
            var sanctionFacade = new SanctionFacade(_sanctionRepository, _userRepository, _publisher.Object);
            sanctionFacade.AddSanction("some rule", _testUserId, _adminId, SanctionType.NotAllowToEditProfile);

            //Act
            _userEditFacade.EditAboutUser(_testUserId, "new");
        }

        [TestMethod]
        [ExpectedException(typeof(ActionIsNotAllowWithSanctionsException))]
        public void TryToEditGenderWithSanctions_GetException()
        {
            //Arrange
            var sanctionFacade = new SanctionFacade(_sanctionRepository, _userRepository, _publisher.Object);
            sanctionFacade.AddSanction("some rule", _testUserId, _adminId, SanctionType.NotAllowToEditProfile);

            //Act
            _userEditFacade.EditGender(_testUserId, Gender.Woman);
        }

        [TestMethod]
        [ExpectedException(typeof(ActionIsNotAllowWithSanctionsException))]
        public void TryToEditAvatarLinkWithSanctions_GetException()
        {
            //Arrange
            var sanctionFacade = new SanctionFacade(_sanctionRepository, _userRepository, _publisher.Object);
            sanctionFacade.AddSanction("some rule", _testUserId, _adminId, SanctionType.NotAllowToEditProfile);

            //Act
            _userEditFacade.EditAvatarLink(_testUserId, "new");
        }

        [TestMethod]
        [ExpectedException(typeof(ActionIsNotAllowWithSanctionsException))]
        public void TryToEditBirthYearWithSanctions_GetException()
        {
            //Arrange
            var sanctionFacade = new SanctionFacade(_sanctionRepository, _userRepository, _publisher.Object);
            sanctionFacade.AddSanction("some rule", _testUserId, _adminId, SanctionType.NotAllowToEditProfile);

            //Act
            _userEditFacade.EditBirthYear(_testUserId, 1990);
        }

        [TestMethod]
        [ExpectedException(typeof(ActionIsNotAllowWithSanctionsException))]
        public void TryToEditContactsWithSanctions_GetException()
        {
            //Arrange
            var sanctionFacade = new SanctionFacade(_sanctionRepository, _userRepository, _publisher.Object);
            sanctionFacade.AddSanction("some rule", _testUserId, _adminId, SanctionType.NotAllowToEditProfile);

            //Act
            _userEditFacade.EditContacts(_testUserId, new List<string> { "new" });
        }

        [TestMethod]
        [ExpectedException(typeof(ActionIsNotAllowWithSanctionsException))]
        public void TryToBecomeTeacherWithSanctions_GetException()
        {
            //Arrange
            var sanctionFacade = new SanctionFacade(_sanctionRepository, _userRepository, _publisher.Object);
            sanctionFacade.AddSanction("some rule", _testUserId, _adminId, SanctionType.NotAllowToTeach);

            //Act
            _userEditFacade.BecomeTeacher(_testUserId);
        }

        [TestMethod]
        public void EditProfileWithValidValues_GetEditedProfile()
        {
            //Arrange
            var testUser = _userFacade.GetUser(_testUserId);
            var newName = "NewName";
            var newAbout = "NewAbout";
            var newGender = Gender.Woman;
            var newAvatarLink = "";
            var newContactData = new List<string> { "new1", "new2" };
            var newBirthYear = 1998;

            //Act
            _userEditFacade.EditProfile(_testUserId, newName, newAbout, newGender, newAvatarLink, newContactData, newBirthYear);

            //Assert
            Assert.AreEqual(newName, testUser.UserProfile.Name);
            Assert.AreEqual(newAbout, testUser.UserProfile.AboutUser);
            Assert.AreEqual(newGender, testUser.UserProfile.Gender);
            Assert.AreEqual(newContactData.Count, testUser.UserProfile.Contacts.Count);
            Assert.AreEqual(newBirthYear, testUser.UserProfile.BirthYear);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EditProfileWithInvalidContactData_GetException()
        {
            //Arrange
            var newContactData = new List<string> { "new", " " };

            //Act
            _userEditFacade.EditProfile(_testUserId, "new", "new", Gender.Man, "", newContactData, 1998);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void EditProfileWithInvalidBirthYear_GetException()
        {
            //Act
            _userEditFacade.EditProfile(_testUserId, "new", "new", Gender.Man, "", new List<string> { "new" }, 1899);
        }

        [TestMethod]
        public void ConfigureTeacherProfile_GetEditedSkills()
        {
            //Arrange
            var newSkills = new List<string> { "skill1", "skill2", "skill3" };
            _userEditFacade.BecomeTeacher(_testUserId);

            //Act
            _userEditFacade.EditTeacherProfile(_testUserId, newSkills);

            //Assert
            var testUser = _userFacade.GetUser(_testUserId);
            Assert.AreEqual(newSkills, testUser.TeacherProfile.Skills);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConfigureTeacherProfileWithInvalidValue_GetException()
        {
            //Arrange
            var newSkills = new List<string> { "skill1", " ", "" };
            _userEditFacade.BecomeTeacher(_testUserId);

            //Act
            _userEditFacade.EditTeacherProfile(_testUserId, newSkills);
        }

        [TestMethod]
        [ExpectedException(typeof(UserIsNotTeacher))]
        public void ConfigureTeacherProfileAtNotTeacher_GetException()
        {
            //Arrange
            var newSkills = new List<string> { "skill1", "skill2", "skill3" };
            _userEditFacade.BecomeTeacher(_testUserId);
            _userEditFacade.StopToBeTeacher(_testUserId);

            //Act
            _userEditFacade.EditTeacherProfile(_testUserId, newSkills);
        }
    }
}