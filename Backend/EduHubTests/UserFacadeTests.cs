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
            TypeOfUser expectedType = TypeOfUser.User;
            var expectedStatus = false;
            
            //Act
            userFacade.RegUser(expectedName, Credentials.FromRawData(expectedEmail, expectedPass), expectedStatus, expectedType, expectedLink);
            List<User> listOfUsers = userFacade.GetUsers().ToList();
            User currentUser = listOfUsers[0];
            
            //Assert
            Assert.AreEqual(currentUser.Name, expectedName);
            Assert.AreEqual(currentUser.IsTeacher, expectedStatus);
        }

        [TestMethod]
        public void TryToGetAllGroupsOfUser_ReturnRightResult()
        {
            //Arrange
            InMemoryUserRepository inMemoryUserRepository = new InMemoryUserRepository();
            InMemoryGroupRepository inMemoryGroupRepository = new InMemoryGroupRepository();

            UserFacade userFacade = new UserFacade(inMemoryUserRepository, inMemoryGroupRepository);
            GroupFacade groupFacade = new GroupFacade(inMemoryGroupRepository, inMemoryUserRepository, new GroupSettings(2, 10, 0, 100));

            userFacade.RegUser("Alena", new Credentials("email", "password"), true, TypeOfUser.User, "avatar.ru");
            userFacade.RegUser("Galya", new Credentials("email", "password"), true, TypeOfUser.User, "avatar.ru");
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
            testGroup1.AddMember(creator.Id, testUser.Id);
            testGroup2.AddMember(creator.Id, testUser.Id);
            List<Group> expected = new List<Group>();
            expected.Add(testGroup1);
            expected.Add(testGroup2);
            List<Group> groups = userFacade.GetAllGroupsOfUser(testUser.Id).ToList();

            //Arrange
            Assert.AreEqual(true, expected.SequenceEqual(groups));
        }
    }
}
