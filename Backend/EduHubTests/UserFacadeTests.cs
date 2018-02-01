﻿using System;
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

namespace EduHubTests
{
    [TestClass]
    public class UserFacadeTests
    {
        [TestMethod]
        public void AddNewUser_HasItAdded()
        {
            //Arrange
            InMemoryUserRepository inMemoryUserRepository = new InMemoryUserRepository();
            InMemoryGroupRepository inMemoryGroupRepository = new InMemoryGroupRepository();

            UserFacade userFacade = new UserFacade(inMemoryUserRepository, inMemoryGroupRepository);
            var expectedName = "yaroslav";
            var expectedPass = "123123";
            var expectedEmail = "bus.yaroslav@gmail.com";
            var expectedLink = "avatar.ru";
            UserType expectedType = UserType.User;
            var expectedStatus = false;
            
            //Act
            userFacade.RegUser(expectedName, Credentials.FromRawData(expectedEmail, expectedPass), expectedStatus, expectedType, expectedLink);
            List<User> listOfUsers = userFacade.GetUsers().ToList();
            User currentUser = listOfUsers[0];
            
            //Assert
            Assert.AreEqual(currentUser.UserProfile.Name, expectedName);
            Assert.AreEqual(currentUser.UserProfile.IsTeacher, expectedStatus);
        }

        [TestMethod]
        public void TryToGetAllGroupsOfUser_ReturnRightResult()
        {
            //Arrange
            InMemoryUserRepository inMemoryUserRepository = new InMemoryUserRepository();
            InMemoryGroupRepository inMemoryGroupRepository = new InMemoryGroupRepository();

            UserFacade userFacade = new UserFacade(inMemoryUserRepository, inMemoryGroupRepository);
            GroupFacade groupFacade = new GroupFacade(inMemoryGroupRepository, inMemoryUserRepository, new GroupSettings(2, 10, 0, 100));

            userFacade.RegUser("Alena", new Credentials("email1", "password"), true, UserType.User, "avatar.ru");
            userFacade.RegUser("Galya", new Credentials("email2", "password"), true, UserType.User, "avatar.ru");
            List<User> listOfUsers = userFacade.GetUsers().ToList();
            User testUser = listOfUsers[0];
            User creator = listOfUsers[1];

            List<string> tags = new List<string>();
            tags.Add("Math");
            groupFacade.CreateGroup(creator.Id, "Group1", tags, "Good group", 5, 0, false, GroupType.MasterClass);
            groupFacade.CreateGroup(creator.Id, "Group2", tags, "The best group!", 7, 0, true, GroupType.Seminar);
            List<Group> listOfGroups = groupFacade.GetGroups().ToList();
            Group testGroup1 = listOfGroups[0];
            Group testGroup2 = listOfGroups[1];

            //Act
            testGroup1.AddMember(testUser.Id);
            testGroup2.AddMember(testUser.Id);
            List<Group> expected = new List<Group>();
            expected.Add(testGroup1);
            expected.Add(testGroup2);
            List<Group> groups = userFacade.GetAllGroupsOfUser(testUser.Id).ToList();

            //Assert
            Assert.AreEqual(true, expected.SequenceEqual(groups));
        }

        [TestMethod]
        public void TryToFindAnyExistingUserViaFullName_ReturnTrue()
        {
            //Arrange
            InMemoryUserRepository inMemoryUserRepository = new InMemoryUserRepository();
            InMemoryGroupRepository inMemoryGroupRepository = new InMemoryGroupRepository();
            UserFacade userFacade = new UserFacade(inMemoryUserRepository, inMemoryGroupRepository);

            userFacade.RegUser("Alena", new Credentials("email1", "password"), true, UserType.User, "avatar.ru");
            userFacade.RegUser("Galya", new Credentials("email2", "password"), true, UserType.User, "avatar.ru");

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
            InMemoryUserRepository inMemoryUserRepository = new InMemoryUserRepository();
            InMemoryGroupRepository inMemoryGroupRepository = new InMemoryGroupRepository();
            UserFacade userFacade = new UserFacade(inMemoryUserRepository, inMemoryGroupRepository);

            userFacade.RegUser("Alena", new Credentials("email1", "password"), true, UserType.User, "avatar.ru");
            userFacade.RegUser("Galya", new Credentials("email2", "password"), true, UserType.User, "avatar.ru");

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
            InMemoryUserRepository inMemoryUserRepository = new InMemoryUserRepository();
            InMemoryGroupRepository inMemoryGroupRepository = new InMemoryGroupRepository();
            UserFacade userFacade = new UserFacade(inMemoryUserRepository, inMemoryGroupRepository);

            userFacade.RegUser("Alena", new Credentials("email1", "password"), true, UserType.User, "avatar.ru");
            userFacade.RegUser("Galya", new Credentials("email2", "password"), true, UserType.User, "avatar.ru");

            //Act
            var actual = userFacade.DoesUserExist("Grisha");
            var expected = false;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TryToGetFoundUsers_ReturnListWithSorting()
        {
            //Arrange
            InMemoryUserRepository inMemoryUserRepository = new InMemoryUserRepository();
            InMemoryGroupRepository inMemoryGroupRepository = new InMemoryGroupRepository();
            UserFacade userFacade = new UserFacade(inMemoryUserRepository, inMemoryGroupRepository);
            
            userFacade.RegUser("Alenka", new Credentials("email1", "password"), true, UserType.User, "avatar.ru");
            userFacade.RegUser("Alena", new Credentials("email2", "password"), true, UserType.User, "avatar.ru");
            userFacade.RegUser("Olena", new Credentials("email3", "password"), true, UserType.User, "avatar.ru");

            List<User> allUsers = inMemoryUserRepository.GetAll().ToList();
            
            List<User> expected = new List<User>();
            expected.Add(allUsers[1]);
            expected.Add(allUsers[0]);

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
            InMemoryUserRepository inMemoryUserRepository = new InMemoryUserRepository();
            InMemoryGroupRepository inMemoryGroupRepository = new InMemoryGroupRepository();
            UserFacade userFacade = new UserFacade(inMemoryUserRepository, inMemoryGroupRepository);

            //Act
            userFacade.RegUser("Grisha", new Credentials("sokolov@mail.ru", "password1"), true, UserType.User, "avatar1.ru");
            userFacade.RegUser("Sasha", new Credentials("sokolov@mail.ru", "password2"), false, UserType.User, "avatar2.ru");
        }

        [TestMethod]
        public void TryToInviteUserWithTeacherFlag_IsItPossible()
        {
            //Arrange
            InMemoryUserRepository inMemoryUserRepository = new InMemoryUserRepository();
            InMemoryGroupRepository inMemoryGroupRepository = new InMemoryGroupRepository();
            GroupFacade groupFacade = new GroupFacade(inMemoryGroupRepository, inMemoryUserRepository, new GroupSettings(1, 100, 0, 1000));
            UserFacade userFacade = new UserFacade(inMemoryUserRepository, inMemoryGroupRepository);

            userFacade.RegUser("Creator", new Credentials("email1", "password"), false, UserType.User, "avatar.ru");
            userFacade.RegUser("Pseudo teacher", new Credentials("email2", "password"), true, UserType.User, "avatar.ru");
            List<User> allUsers = userFacade.GetUsers().ToList();
            Guid creatorId = allUsers[0].Id;
            Guid teacherId = allUsers[1].Id;

            var tags = new List<string>();
            tags.Add("js");

            groupFacade.CreateGroup(creatorId, "Some group", tags, "Very interesting", 1, 100, false, GroupType.Lecture);
            List<Group> allGroups = groupFacade.GetGroups().ToList();
            var createdGroup = allGroups[0];

            //Act
            userFacade.Invite(creatorId, teacherId, createdGroup.GroupInfo.Id, MemberRole.Teacher);
            List <Invitation> invitations = userFacade.GetAllInvitationsForUser(teacherId).ToList();

            //Assert
            Assert.AreEqual(createdGroup.GroupInfo.Id, invitations[0].GroupId);
        }

        [TestMethod]
        [ExpectedException(typeof(UserIsNotTeacher))]
        public void TryToInviteUserWithoutTeacherFlag_GetException()
        {
            //Arrange
            InMemoryUserRepository inMemoryUserRepository = new InMemoryUserRepository();
            InMemoryGroupRepository inMemoryGroupRepository = new InMemoryGroupRepository();
            GroupFacade groupFacade = new GroupFacade(inMemoryGroupRepository, inMemoryUserRepository, new GroupSettings(1, 100, 0, 1000));
            UserFacade userFacade = new UserFacade(inMemoryUserRepository, inMemoryGroupRepository);

            userFacade.RegUser("Creator", new Credentials("email1", "password"), false, UserType.User, "avatar.ru");
            userFacade.RegUser("Pseudo teacher", new Credentials("email2", "password"), false, UserType.User, "avatar.ru");
            List<User> allUsers = userFacade.GetUsers().ToList();
            Guid creatorId = allUsers[0].Id;
            Guid pseudoTeacherId = allUsers[1].Id;

            var tags = new List<string>();
            tags.Add("js");

            groupFacade.CreateGroup(creatorId, "Some group", tags, "Very interesting", 1, 100, false, GroupType.Lecture);
            List<Group> allGroups = groupFacade.GetGroups().ToList();
            var createdGroupId = allGroups[0].GroupInfo.Id;

            //Act
            userFacade.Invite(creatorId, pseudoTeacherId, createdGroupId, MemberRole.Teacher);
        }

        [TestMethod]
        public void TryToInviteUser_GetAddedInvitationInGroup()
        {
            //Arrange
            InMemoryUserRepository inMemoryUserRepository = new InMemoryUserRepository();
            InMemoryGroupRepository inMemoryGroupRepository = new InMemoryGroupRepository();
            GroupFacade groupFacade = new GroupFacade(inMemoryGroupRepository, inMemoryUserRepository, new GroupSettings(1, 100, 0, 1000));
            UserFacade userFacade = new UserFacade(inMemoryUserRepository, inMemoryGroupRepository);

            userFacade.RegUser("Creator", new Credentials("email1", "password"), false, UserType.User, "avatar.ru");
            userFacade.RegUser("Invited", new Credentials("email2", "password"), false, UserType.User, "avatar.ru");
            List<User> allUsers = userFacade.GetUsers().ToList();
            Guid creatorId = allUsers[0].Id;
            Guid invitedId = allUsers[1].Id;

            var tags = new List<string>();
            tags.Add("js");

            groupFacade.CreateGroup(creatorId, "Some group", tags, "Very interesting", 1, 100, false, GroupType.Lecture);
            List<Group> allGroups = groupFacade.GetGroups().ToList();
            var createdGroupId = allGroups[0].GroupInfo.Id;

            //Act
            userFacade.Invite(creatorId, invitedId, createdGroupId, MemberRole.Member);

            //Assert
            Assert.AreEqual(allGroups[0].GetAllInvitation().ToList().Count, 1);
        }
    }
}
