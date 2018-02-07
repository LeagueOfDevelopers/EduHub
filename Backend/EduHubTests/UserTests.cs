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
            UserType type = UserType.User;

            //Act
            var testUser = new User(userName, Credentials.FromRawData(email, password), isTeacher, type);
            var actualName = testUser.UserProfile.Name;
            var actualIsTeacher = testUser.UserProfile.IsTeacher;

            //Assert
            Assert.AreEqual(userName, actualName);
            Assert.AreEqual(isTeacher, actualIsTeacher);

        }
        
        [TestMethod]
        public void DeleteProfile_IsItPossible()
        {
            //Arrange
            User testUser = new User("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false, UserType.User);

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
            User testUser = new User("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false, UserType.User);

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
            UserType type = UserType.Admin;

            //Act
            User testUser = new User(nameOfUser, Credentials.FromRawData(email, "1"), isTeacher, type);
        }

        [TestMethod]
        public void ConfigureSkills_IsItPossible()
        {
            //Arrange
            User testUser = new User("Ivan", new Credentials("SomeEmail", "SomePassword"), true, UserType.User);
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
            User teacher = new User("Petr", new Credentials("SomeEmail", "SomePassword"), true, UserType.User);
            
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
            User teacher = new User("Petr", new Credentials("SomeEmail", "SomePassword"), true, UserType.User);
            
            //Act
            Review review = new Review(Guid.Empty, "The best", 5);
            teacher.TeacherProfile.AddReview(review);
        }
    }
}
