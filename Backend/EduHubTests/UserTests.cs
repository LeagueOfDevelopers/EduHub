using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EduHubLibrary.Domain;

namespace EduHubTests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void CreateUser_IsItPossible()
        {
            var nameOfUser = "Ivan";
            var email = "sokolov@gmail.com";
            var password = "sokolov";

            var IsTeacher = false;

            var UserTest = new User(nameOfUser, email, password, IsTeacher);
            var ExpectedName = nameOfUser;
            var ExpectedEmail = email;
            var ExpectedIsTeacher = IsTeacher;

            var ActualName = UserTest.Name;
            var ActualEmail = UserTest.Email;
            var ActualPass = UserTest.Password;
            var ActualIsTeacher = UserTest.IsTeacher;
            Assert.AreEqual(ExpectedName, ActualName);
            Assert.AreEqual(ExpectedEmail, ActualEmail);
            Assert.AreEqual(ExpectedIsTeacher, ActualIsTeacher);
            Assert.AreEqual(password, ActualPass);

        }

        [TestMethod]
        public void EditName_IsItPossible()
        {
            string NewName = "Nikolai";

            User UserTest = new User("Ivan", "ivanov@mail.ru", "1", false);
            UserTest.EditName(NewName);
            string ExpectedName = NewName;

            string ActualName = UserTest.Name;
            Assert.AreEqual(ExpectedName, ActualName);
        }

        [ExpectedException(typeof(System.ArgumentException)), TestMethod]
        public void EditName_IsItCorrect()
        {
            string NewName = "";

            User UserTest = new User("Ivan", "ivanov@mail.ru", "1",false);
            UserTest.EditName(NewName);
        }

        [TestMethod]
        public void BecomeTeacher_IsItPossible()
        {
            User UserTest = new User("Ivan", "ivanov@mail.ru", "1",false);
            UserTest.BecomeTeacher();

            Assert.AreEqual(true, UserTest.IsTeacher);
        }

        [TestMethod]
        public void StopToTeacher_IsItPossible()
        {
            User UserTest = new User("Ivan", "ivanov@mail.ru","1", true);
            UserTest.StopToBeTeacher();

            Assert.AreEqual(false, UserTest.IsTeacher);
        }

        [TestMethod]
        public void DeleteProfile_IsItPossible()
        {
            User UserTest = new User("Ivan", "ivanov@gmail.com","1", false);

            UserTest.DeleteProfile();
            bool Expected = false;

            bool Actual = UserTest.IsActive;
            Assert.AreEqual(Expected, Actual);
        }

        [TestMethod]
        public void RestoreProfile_IsItPossible()
        {
            User UserTest = new User("Ivan", "ivanov@gmail.com","1", false);

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

            User UserTest = new User(NameOfUser, Email, "1", IsTeacher);
        }
    }
}
