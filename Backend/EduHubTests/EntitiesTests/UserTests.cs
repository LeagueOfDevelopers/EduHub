using System;
using System.Collections.Generic;
using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Domain.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EduHubLibrary.Interators;

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
            var type = UserType.User;

            //Act
            var testUser = new User(userName, Credentials.FromRawData(email, password), isTeacher, type);
            var actualName = testUser.UserProfile.Name;
            var actualIsTeacher = testUser.UserProfile.IsTeacher;
            var actualType = testUser.Type;

            //Assert
            Assert.AreEqual(userName, actualName);
            Assert.AreEqual(isTeacher, actualIsTeacher);
            Assert.AreEqual(type, actualType);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TryToCreateUserWithInvalidValues_GetException()
        {
            //Arrange
            var userName = "";
            var email = "";
            var isTeacher = false;
            var type = UserType.Admin;

            //Act
            var testUser = new User(userName, Credentials.FromRawData(email, "1"), isTeacher, type);
        }

        [TestMethod]
        public void DeleteProfile_ProfileWasDeleted()
        {
            //Arrange
            var testUser = new User("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false, UserType.User);

            //Act
            testUser.DeleteProfile();

            //Assert
            Assert.AreEqual(false, testUser.IsActive);
        }

        [TestMethod]
        public void RestoreProfile_ProfileWasRestored()
        {
            //Arrange
            var testUser = new User("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false, UserType.User);

            //Act
            testUser.DeleteProfile();
            testUser.RestoreProfile();

            //Assert
            Assert.AreEqual(true, testUser.IsActive);
        }

        [TestMethod]
        public void ChangeUserTypeToModerator_UserBecameModerator()
        {
            //Arrange
            var testUser = new User("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false, UserType.User);

            //Act
            testUser.BecomeModerator();

            //Assert
            Assert.AreEqual(UserType.Moderator, testUser.Type);
        }

        [TestMethod]
        public void ChangeUserTypeToUser_ModeratorBecameUser()
        {
            //Arrange
            var testUser = new User("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false, UserType.Admin);

            //Act
            testUser.StopToBeModerator();

            //Assert
            Assert.AreEqual(UserType.User, testUser.Type);
        }

        [TestMethod]
        public void ConfigureSkills_IsItPossible()
        {
            //Arrange
            var testUser = new User("Ivan", new Credentials("SomeEmail", "SomePassword"), true, UserType.User);
            var skills = new List<string> {"c#", "c++"};

            //Act
            testUser.ConfigureTeacherProfile(skills);

            //Assert
            Assert.AreEqual(skills, testUser.TeacherProfile.Skills);
        }

        [TestMethod]
        public void AddReviewToTeacher_GetAddedReview()
        {
            //Arrange
            var teacher = new User("Petr", new Credentials("SomeEmail", "SomePassword"), true, UserType.User);

            //Act
            var guid = IntIterator.GetNextId();
            var review = new Review(guid, "The best", "The beast teacher of the year", guid);
            teacher.TeacherProfile.AddReview(guid, "The best", "The beast teacher of the year", guid);

            //Assert
            Assert.AreEqual(review.Text, teacher.TeacherProfile.Reviews[0].Text);
        }

        [TestMethod]
        public void AddInvitationWithRightReceiverToUser_GetRightInvitationList()
        {
            //Arrange
            var testUser = new User("Petr", new Credentials("SomeEmail", "SomePassword"), true, UserType.User);
            var invitation = new Invitation(IntIterator.GetNextId(), testUser.Id, IntIterator.GetNextId(),
                MemberRole.Member, InvitationStatus.InProgress);

            //Act
            testUser.AddInvitation(invitation);

            //Assert
            Assert.AreEqual(1, testUser.Invitations.Count);
            Assert.AreEqual(invitation, testUser.Invitations[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TryToAddInvitationWithWrongReceiverToUser_GetException()
        {
            //Arrange
            var testUser = new User("Petr", new Credentials("SomeEmail", "SomePassword"), true, UserType.User);
            var invitation = new Invitation(IntIterator.GetNextId(), IntIterator.GetNextId(), IntIterator.GetNextId(),
                MemberRole.Member, InvitationStatus.InProgress);

            //Act
            testUser.AddInvitation(invitation);
        }

        [TestMethod]
        public void AcceptInvitation_GetAcceptedInvitation()
        {
            //Arrange
            var testUser = new User("Petr", new Credentials("SomeEmail", "SomePassword"), true, UserType.User);
            var invitation = new Invitation(IntIterator.GetNextId(), testUser.Id, IntIterator.GetNextId(),
                MemberRole.Member, InvitationStatus.InProgress);
            testUser.AddInvitation(invitation);

            //Act
            testUser.AcceptInvitation(invitation.Id);

            //Assert
            Assert.AreEqual(InvitationStatus.Accepted, testUser.Invitations[0].Status);
        }

        [TestMethod]
        [ExpectedException(typeof(InvitationAlreadyChangedException))]
        public void TryToAcceptAlreadyAcceptedInvitation_GetException()
        {
            //Arrange
            var testUser = new User("Petr", new Credentials("SomeEmail", "SomePassword"), true, UserType.User);
            var invitation = new Invitation(IntIterator.GetNextId(), testUser.Id, IntIterator.GetNextId(),
                MemberRole.Member, InvitationStatus.InProgress);
            testUser.AddInvitation(invitation);
            testUser.AcceptInvitation(invitation.Id);

            //Act
            testUser.AcceptInvitation(invitation.Id);
        }

        [TestMethod]
        public void DeclineInvitation_GetDeclinedInvitation()
        {
            //Arrange
            var testUser = new User("Petr", new Credentials("SomeEmail", "SomePassword"), true, UserType.User);
            var invitation = new Invitation(IntIterator.GetNextId(), testUser.Id, IntIterator.GetNextId(),
                MemberRole.Member, InvitationStatus.InProgress);
            testUser.AddInvitation(invitation);

            //Act
            testUser.DeclineInvitation(invitation.Id);

            //Assert
            Assert.AreEqual(InvitationStatus.Declined, testUser.Invitations[0].Status);
        }

        [TestMethod]
        [ExpectedException(typeof(InvitationAlreadyChangedException))]
        public void TryToDeclineAlreadyDeclinedInvitation_GetException()
        {
            //Arrange
            var testUser = new User("Petr", new Credentials("SomeEmail", "SomePassword"), true, UserType.User);
            var invitation = new Invitation(IntIterator.GetNextId(), testUser.Id, IntIterator.GetNextId(),
                MemberRole.Member, InvitationStatus.InProgress);
            testUser.AddInvitation(invitation);
            testUser.DeclineInvitation(invitation.Id);

            //Act
            testUser.DeclineInvitation(invitation.Id);
        }

        [TestMethod]
        public void AddNotifyToUser_GetUpdatedNotifyList()
        {
            //Arrange
            var testUser = new User("Petr", new Credentials("SomeEmail", "SomePassword"), true, UserType.User);

            //Act
            testUser.AddNotify("Some notify");

            //Assert
            Assert.AreEqual(1, testUser.Notifies.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddInvalidNotifyToUser_GetException()
        {
            //Arrange
            var testUser = new User("Petr", new Credentials("SomeEmail", "SomePassword"), true, UserType.User);

            //Act
            testUser.AddNotify(" ");
        }

        [TestMethod]
        public void ChangeUserPassword_GetChangedPassword()
        {
            //Arrange
            var testUser = new User("Petr", new Credentials("SomeEmail", "SomePassword"), true, UserType.User);
            var expected = Credentials.FromRawData("email", "NewPassword").PasswordHash;

            //Act
            testUser.ChangePassword("NewPassword");

            //Assert
            Assert.AreEqual(expected, testUser.Credentials.PasswordHash);
        }
    }
}