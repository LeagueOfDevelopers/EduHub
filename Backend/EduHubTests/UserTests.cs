using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EduHubLibrary.Domain;

namespace UnitTests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void CreationOfUserReturnTrue()
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
        public void EditProfileReturnTrue()
        {
            string NewName = "Nikolai";
            string NewEmail = "petrov@gmail.com";
            bool IsTeacher = false;

            User UserTest = new User("Ivan", "sokolov@gmail.com", true);
            UserTest.EditProfile(NewName, NewEmail, IsTeacher);
            string ExpectedName = NewName;
            string ExpectedEmail = NewEmail;
            bool ExpectedIsTeacher = IsTeacher;

            string ActualName = UserTest.Name;
            string ActualEmail = UserTest.Email;
            bool ActualIsTeacher = UserTest.IsTeacher;
            Assert.AreEqual(ExpectedName, ActualName);
            Assert.AreEqual(ExpectedEmail, ActualEmail);
            Assert.AreEqual(ExpectedIsTeacher, ActualIsTeacher);
        }

        [TestMethod]
        public void DeleteProfileReturnTrue()
        {
            User UserTest = new User("Ivan", "ivanov@gmail.com", false);

            UserTest.DeleteProfile();
            bool Expected = false;

            bool Actual = UserTest.IsActive;
            Assert.AreEqual(Expected, Actual);
        }

        [TestMethod]
        public void RestoreProfileReturnTrue()
        {
            User UserTest = new User("Ivan", "ivanov@gmail.com", false);

            UserTest.DeleteProfile();
            UserTest.RestoreProfile();
            bool Expected = true;

            bool Actual = UserTest.IsActive;
            Assert.AreEqual(Expected, Actual);
        }
    }
}
