using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EduHubLibrary.Domain;
using EduHubLibrary.Common;
using System.Collections.Generic;

namespace EduHubTests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void CreateUser_IsItPossible()
        {
            //Arrange
            var UserName = "Ivan";
            var Email = "sokolov@gmail.com";
            var Password = "sokolov";
            var IsTeacher = false;
            TypeOfUser Type = TypeOfUser.User;
            var Avatar = "avatar.ru";

            //Act
            var UserTest = new User(UserName, Credentials.FromRawData(Email, Password), IsTeacher, Type, Avatar);
            var ActualName = UserTest.Name;
            var ActualIsTeacher = UserTest.IsTeacher;

            //Assert
            Assert.AreEqual(UserName, ActualName);
            Assert.AreEqual(IsTeacher, ActualIsTeacher);

        }

        [TestMethod]
        public void EditName_IsItPossible()
        {
            //Arrange
            var NewName = "Nikolai";

            //Act
            User UserTest = new User("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false, TypeOfUser.User, "avatar.com");
            UserTest.EditName(NewName);
            var ActualName = UserTest.Name;

            //Assert
            Assert.AreEqual(NewName, ActualName);
        }

        [ExpectedException(typeof(System.ArgumentException)), TestMethod]
        public void EditName_IsItCorrect()
        {
            //Arrange
            var NewName = "";

            //Act
            User UserTest = new User("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false, TypeOfUser.User, "avatar.com");
            UserTest.EditName(NewName);
        }

        [TestMethod]
        public void BecomeTeacher_IsItPossible()
        {
            //Arrange
            User UserTest = new User("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false, TypeOfUser.User, "avatar.com");

            //Act
            UserTest.BecomeTeacher();

            //Assert
            Assert.AreEqual(true, UserTest.IsTeacher);
        }

        [TestMethod]
        public void StopToTeacher_IsItPossible()
        {
            //Arrange
            User UserTest = new User("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), true, TypeOfUser.User, "avatar.com");

            //Act
            UserTest.StopToBeTeacher();

            //Assert
            Assert.AreEqual(false, UserTest.IsTeacher);
        }

        [TestMethod]
        public void DeleteProfile_IsItPossible()
        {
            //Arrange
            User UserTest = new User("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false, TypeOfUser.User, "avatar.com");

            //Act
            UserTest.DeleteProfile();
            var Actual = UserTest.IsActive;

            //Assert
            Assert.AreEqual(false, Actual);
        }

        [TestMethod]
        public void RestoreProfile_IsItPossible()
        {
            //Arrange
            User UserTest = new User("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false, TypeOfUser.User, "avatar.com");

            //Act
            UserTest.DeleteProfile();
            UserTest.RestoreProfile();
            var Actual = UserTest.IsActive;

            //Assert
            Assert.AreEqual(true, Actual);
        }

        [ExpectedException(typeof(System.ArgumentException)), TestMethod]
        public void CreateUser_IsCorrectData()
        {
            //Arrange
            var NameOfUser = "";
            var Email = "";
            var IsTeacher = false;
            TypeOfUser Type = TypeOfUser.Admin;
            var AvatarLink = "";

            //Act
            User UserTest = new User(NameOfUser, Credentials.FromRawData(Email, "1"), IsTeacher, Type, AvatarLink);
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
