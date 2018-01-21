using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EduHubLibrary.Domain;
using EduHubLibrary.Common;
using System.Collections.Generic;
using EduHubLibrary.Domain.Exceptions;

namespace EduHubTests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void CreateUser_IsItPossible()
        {
            //Arrange
            var userName = "Ivan";
            var email = "sokolov@gmail.com";
            var password = "sokolov";
            var isTeacher = false;
            TypeOfUser type = TypeOfUser.User;
            var avatar = "avatar.ru";

            //Act
            var testUser = new User(userName, Credentials.FromRawData(email, password), isTeacher, type, avatar);
            var actualName = testUser.Name;
            var actualIsTeacher = testUser.IsTeacher;

            //Assert
            Assert.AreEqual(userName, actualName);
            Assert.AreEqual(isTeacher, actualIsTeacher);

        }

        [TestMethod]
        public void EditName_IsItPossible()
        {
            //Arrange
            var newName = "Nikolai";

            //Act
            User testUser = new User("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false, TypeOfUser.User, "avatar.com");
            testUser.EditName(newName);
            var actualName = testUser.Name;

            //Assert
            Assert.AreEqual(newName, actualName);
        }

        [ExpectedException(typeof(System.ArgumentException)), TestMethod]
        public void EditName_IsItCorrect()
        {
            //Arrange
            var newName = "";

            //Act
            User testUser = new User("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false, TypeOfUser.User, "avatar.com");
            testUser.EditName(newName);
        }

        [TestMethod]
        public void BecomeTeacher_IsItPossible()
        {
            //Arrange
            User testUser = new User("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false, TypeOfUser.User, "avatar.com");

            //Act
            testUser.BecomeTeacher();

            //Assert
            Assert.AreEqual(true, testUser.IsTeacher);
        }

        [TestMethod]
        public void StopToTeacher_IsItPossible()
        {
            //Arrange
            User testUser = new User("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), true, TypeOfUser.User, "avatar.com");

            //Act
            testUser.StopToBeTeacher();

            //Assert
            Assert.AreEqual(false, testUser.IsTeacher);
        }

        [TestMethod]
        public void DeleteProfile_IsItPossible()
        {
            //Arrange
            User testUser = new User("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false, TypeOfUser.User, "avatar.com");

            //Act
            testUser.DeleteProfile();
            var Actual = testUser.IsActive;

            //Assert
            Assert.AreEqual(false, Actual);
        }

        [TestMethod]
        public void RestoreProfile_IsItPossible()
        {
            //Arrange
            User testUser = new User("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false, TypeOfUser.User, "avatar.com");

            //Act
            testUser.DeleteProfile();
            testUser.RestoreProfile();
            var Actual = testUser.IsActive;

            //Assert
            Assert.AreEqual(true, Actual);
        }

        [ExpectedException(typeof(System.ArgumentException)), TestMethod]
        public void CreateUser_IsCorrectData()
        {
            //Arrange
            var nameOfUser = "";
            var email = "";
            var isTeacher = false;
            TypeOfUser type = TypeOfUser.Admin;
            var avatarLink = "";

            //Act
            User testUser = new User(nameOfUser, Credentials.FromRawData(email, "1"), isTeacher, type, avatarLink);
        }

        [TestMethod]
        public void ConfigureSkills_IsItPossible()
        {
            //Arrange
            User testUser = new User("Ivan", new Credentials("SomeEmail", "SomePassword"), true, TypeOfUser.User, "avatar.com");
            List<string> skills = new List<string>();
            skills.Add("Math");
            skills.Add("Biology");

            //Act
            testUser.ConfigureTeacherProfile(skills);

            //Assert
            Assert.AreEqual(skills, testUser.TeacherProfile.Skills);
        }

        [TestMethod]
        public void AddReviewToTeacher_IsItPossibleWithCorrectData()
        {
            //Arrange
            User teacher = new User("Petr", new Credentials("SomeEmail", "SomePassword"), true, TypeOfUser.User, "avatar.com");
            
            //Act
            Review review = new Review(Guid.NewGuid(), "The best", 5);
            teacher.TeacherProfile.AddReview(review);

            //Assert
            Assert.AreEqual(review, teacher.TeacherProfile.Reviews[0]);
        }

        [ExpectedException(typeof(System.ArgumentException)), TestMethod]
        public void AddReviewToTeacher_IsCorrectEvaluatorData()
        {
            //Arrange
            User teacher = new User("Petr", new Credentials("SomeEmail", "SomePassword"), true, TypeOfUser.User, "avatar.com");
            
            //Act
            Review review = new Review(Guid.Empty, "The best", 5);
            teacher.TeacherProfile.AddReview(review);
        }
    }
}
