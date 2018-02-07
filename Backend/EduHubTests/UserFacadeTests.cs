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
        private InMemoryUserRepository _inMemoryUserRepository;
        private InMemoryGroupRepository _inMemoryGroupRepository;

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
            UserType expectedType = UserType.User;
            var expectedStatus = false;
            
            //Act
            Guid userId = userFacade.RegUser(expectedName, Credentials.FromRawData(expectedEmail, expectedPass), expectedStatus, expectedType);
            User currentUser = userFacade.GetUser(userId);
            
            //Assert
            Assert.AreEqual(currentUser.UserProfile.Name, expectedName);
            Assert.AreEqual(currentUser.UserProfile.IsTeacher, expectedStatus);
        }

        [TestMethod]
        public void TryToGetAllGroupsOfUser_ReturnRightResult()
        {
            //Arrange
            UserFacade userFacade = new UserFacade(_inMemoryUserRepository, _inMemoryGroupRepository);
            GroupFacade groupFacade = new GroupFacade(_inMemoryGroupRepository, _inMemoryUserRepository, new GroupSettings(2, 10, 0, 100));

            Guid testUserId = userFacade.RegUser("Alena", new Credentials("email1", "password"), true, UserType.User);
            Guid creatorId = userFacade.RegUser("Galya", new Credentials("email2", "password"), true, UserType.User);
            
            Guid createdGroupId1 = groupFacade.CreateGroup(creatorId, "Group1", new List<string> { "c#" }, "Good group", 5, 0, false, GroupType.MasterClass);
            Guid createdGroupId2 = groupFacade.CreateGroup(creatorId, "Group2", new List<string> { "c#" }, "The best group!", 7, 0, true, GroupType.Seminar);

            Group createdGroup1 = groupFacade.GetGroup(createdGroupId1);
            Group createdGroup2 = groupFacade.GetGroup(createdGroupId2);
            
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
            UserFacade userFacade = new UserFacade(_inMemoryUserRepository, _inMemoryGroupRepository);

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
            UserFacade userFacade = new UserFacade(_inMemoryUserRepository, _inMemoryGroupRepository);

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
            UserFacade userFacade = new UserFacade(_inMemoryUserRepository, _inMemoryGroupRepository);

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
            UserFacade userFacade = new UserFacade(_inMemoryUserRepository, _inMemoryGroupRepository);

            Guid userId1 = userFacade.RegUser("Alenka", new Credentials("email1", "password"), true, UserType.User);
            Guid userId2 = userFacade.RegUser("Alena", new Credentials("email2", "password"), true, UserType.User);
            Guid userId3 = userFacade.RegUser("Olena", new Credentials("email3", "password"), true, UserType.User);

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
            UserFacade userFacade = new UserFacade(_inMemoryUserRepository, _inMemoryGroupRepository);

            //Act
            userFacade.RegUser("Grisha", new Credentials("sokolov@mail.ru", "password1"), true, UserType.User);
            userFacade.RegUser("Sasha", new Credentials("sokolov@mail.ru", "password2"), false, UserType.User);
        }

        [TestMethod]
        public void TryToInviteUserWithTeacherFlag_IsItPossible()
        {
            //Arrange
            GroupFacade groupFacade = new GroupFacade(_inMemoryGroupRepository, _inMemoryUserRepository, new GroupSettings(1, 100, 0, 1000));
            UserFacade userFacade = new UserFacade(_inMemoryUserRepository, _inMemoryGroupRepository);

            Guid creatorId = userFacade.RegUser("Creator", new Credentials("email1", "password"), false, UserType.User);
            Guid teacherId = userFacade.RegUser("Teacher", new Credentials("email2", "password"), true, UserType.User);

            Guid createdGroupId = groupFacade.CreateGroup(creatorId, "Some group", 
                new List<string> { "c#" }, "Very interesting", 1, 100, false, GroupType.Lecture);

            //Act
            userFacade.Invite(creatorId, teacherId, createdGroupId, MemberRole.Teacher);
            List <Invitation> invitations = userFacade.GetAllInvitationsForUser(teacherId).ToList();

            //Assert
            Assert.AreEqual(createdGroupId, invitations[0].GroupId);
        }

        [TestMethod]
        [ExpectedException(typeof(UserIsNotTeacher))]
        public void TryToInviteUserWithoutTeacherFlag_GetException()
        {
            //Arrange
            GroupFacade groupFacade = new GroupFacade(_inMemoryGroupRepository, _inMemoryUserRepository, new GroupSettings(1, 100, 0, 1000));
            UserFacade userFacade = new UserFacade(_inMemoryUserRepository, _inMemoryGroupRepository);

            Guid creatorId = userFacade.RegUser("Creator", new Credentials("email1", "password"), false, UserType.User);
            Guid pseudoTeacherId = userFacade.RegUser("Pseudo teacher", new Credentials("email2", "password"), false, UserType.User);

            Guid createdGroupId = groupFacade.CreateGroup(creatorId, "Some group", new List<string> { "c#" }, "Very interesting", 1, 100, false, GroupType.Lecture);
           
            //Act
            userFacade.Invite(creatorId, pseudoTeacherId, createdGroupId, MemberRole.Teacher);
        }

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
    }
}
