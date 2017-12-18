using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EduHubLibrary.Domain;

namespace UnitTests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void CreateUser_IsItPossible()
        {
            string NameOfUser = "Ivan";
            string Email = "sokolov@gmail.com";
            bool IsTeacher = false;

            User UserTest = new User(NameOfUser, Email, IsTeacher);
            string ExpectedName = NameOfUser;
            string ExpectedEmail = Email;
            bool ExpectedIsTeacher = IsTeacher;

            string ActualName = UserTest.Name;
            string ActualEmail = UserTest.Email;
            bool ActualIsTeacher = UserTest.IsTeacher;
            Assert.AreEqual(ExpectedName, ActualName);
            Assert.AreEqual(ExpectedEmail, ActualEmail);
            Assert.AreEqual(ExpectedIsTeacher, ActualIsTeacher);
        }

        [TestMethod]
        public void EditName_IsItPossible()
        {
            string NewName = "Nikolai";

            User UserTest = new User("Ivan", "ivanov@mail.ru", false);
            UserTest.EditName(NewName);
            string ExpectedName = NewName;

            string ActualName = UserTest.Name;
            Assert.AreEqual(ExpectedName, ActualName);
        }

        [ExpectedException(typeof(System.ArgumentException)), TestMethod]
        public void EditName_IsItCorrect()
        {
            string NewName = "";

            User UserTest = new User("Ivan", "ivanov@mail.ru", false);
            UserTest.EditName(NewName);
        }

        [TestMethod]
        public void BecomeTeacher_IsItPossible()
        {
            User UserTest = new User("Ivan", "ivanov@mail.ru", false);
            UserTest.BecomeTeacher();

            Assert.AreEqual(true, UserTest.IsTeacher);
        }

        [TestMethod]
        public void StopToTeacher_IsItPossible()
        {
            User UserTest = new User("Ivan", "ivanov@mail.ru", true);
            UserTest.StopToBeTeacher();

            Assert.AreEqual(false, UserTest.IsTeacher);
        }

        [TestMethod]
        public void DeleteProfile_IsItPossible()
        {
            User UserTest = new User("Ivan", "ivanov@gmail.com", false);

            UserTest.DeleteProfile();
            bool Expected = false;

            bool Actual = UserTest.IsActive;
            Assert.AreEqual(Expected, Actual);
        }

        [TestMethod]
        public void RestoreProfile_IsItPossible()
        {
            User UserTest = new User("Ivan", "ivanov@gmail.com", false);

            UserTest.DeleteProfile();
            UserTest.RestoreProfile();
            bool Expected = true;

            bool Actual = UserTest.IsActive;
            Assert.AreEqual(Expected, Actual);
        }

        [ExpectedException(typeof(System.ArgumentException)), TestMethod]
        public void CreateUser_IsCorrectData()
        {
            string NameOfUser = "";
            string Email = "";
            bool IsTeacher = false;

            User UserTest = new User(NameOfUser, Email, IsTeacher);
        }
    }
}
