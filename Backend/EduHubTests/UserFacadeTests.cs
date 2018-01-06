using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EduHubLibrary.Facades;
using EduHubLibrary.Infrastructure;
using EduHubLibrary.Domain;

using System.Linq;
using EduHubLibrary.Common;

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

    }
}
