using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EduHubLibrary.Facades;
using EduHubLibrary.Infrastructure;
using EduHubLibrary.Domain;

using System.Linq;

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
            UserFacade userFacade = new UserFacade(inMemoryUserRepository);
            string expectedName = "yaroslav";
            string expectedPass = "123123";
            string expectedEmail = "bus.yaroslav@gmail.com";
            bool expectedStatus = false;
            //Act
            userFacade.RegUser(expectedName, expectedEmail, expectedPass, expectedStatus);
            List<User> listOfUsers = userFacade.GetUsers().ToList();
            User currentUser = listOfUsers[0];
            //Assert
            Assert.AreEqual(currentUser.Name, expectedName);
            Assert.AreEqual(currentUser.Email, expectedEmail);
            Assert.AreEqual(currentUser.Password, expectedPass);
            Assert.AreEqual(currentUser.IsTeacher, expectedStatus);
        }

    }
}
