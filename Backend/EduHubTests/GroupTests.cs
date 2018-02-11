using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using System.Linq;
using System.Collections.Generic;
using EduHubLibrary.Common;

namespace EduHubTests
{
    [TestClass]
    public class GroupTests
    {
        [TestMethod]
        public void CreateGroupWithSomeData_IsGroupCorrect()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var title = "some group";
            var description = "some description";
            var tags = new List<string> { "c#" };
            var size = 3;
            var moneyPerUser = 100.0;
            var info = new GroupInfo(Guid.NewGuid(), title, description, tags, GroupType.Lecture, false, true, size, moneyPerUser);
            //Act
            var someGroup = new Group(userId, title, tags, description, size, moneyPerUser, false, GroupType.Lecture);
            //Assert
            Assert.AreEqual(userId, someGroup.GetMember(userId).UserId);
        }

        [ExpectedException(typeof(MemberNotFoundException)), TestMethod]
        public void TryToDeleteNotExistingMember_IsItPossible()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var title = "some group";
            var description = "some description";
            var tags = new List<string> { "c#" };
            var size = 3;
            var moneyPerUser = 100.0;
            
            //Act
            var someGroup = new Group(userId, title, tags, description, size, moneyPerUser, false, GroupType.Lecture);
            someGroup.DeleteMember(userId, Guid.NewGuid());
        }

        [ExpectedException(typeof(NotEnoughPermissionsException)), TestMethod]
        public void TryToDeleteWithNotEnoughtRights_IsItPossible()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var idOfInvitedUser = Guid.NewGuid();
            var title = "some group";
            var description = "some description";
            var tags = new List<string> { "c#" };
            var size = 3;
            var moneyPerUser = 100.0;
            
            //Act
            var someGroup = new Group(userId, title, tags, description, size, moneyPerUser, false, GroupType.Lecture);
            someGroup.AddMember(idOfInvitedUser);
            someGroup.DeleteMember(idOfInvitedUser, userId);
        }

        [TestMethod]
        public void TryToDeleteYourselfFromGroup_HasItDeleted()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var idOfInvitedUser = Guid.NewGuid();
            var expected = 1;
            var title = "some group";
            var description = "some description";
            var tags = new List<string> { "c#" };
            var size = 3;
            var moneyPerUser = 100.0;
            
            //Act
            var someGroup = new Group(userId, title, tags, description, size, moneyPerUser, false, GroupType.Lecture);
            someGroup.AddMember(idOfInvitedUser);
            someGroup.DeleteMember(idOfInvitedUser, idOfInvitedUser);
            var result = someGroup.Members.Count;
            
            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TryToAddUserToGroup_HasItAdded()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var title = "some group";
            var description = "some description";
            var tags = new List<string> { "c#" };
            tags.Add("js");
            var idOfInvitedUser = Guid.NewGuid();
            var expected = 2;
            var size = 3;
            var moneyPerUser = 100.0;
            
            //Act
            var someGroup = new Group(userId, title, tags, description, size, moneyPerUser, false, GroupType.Lecture);
            someGroup.AddMember(idOfInvitedUser);
            var result = someGroup.Members.Count;
            
            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TryToDeleteUserFromGroupByAdmin_HasItDeleted()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var idOfInvitedUser = Guid.NewGuid();
            var title = "some group";
            var description = "some description";
            var tags = new List<string> { "c#" };
            var size = 3;
            var moneyPerUser = 100.0;
            var expected = 1;
            
            //Act
            var someGroup = new Group(userId, title, tags, description, size, moneyPerUser, false, GroupType.Lecture);
            someGroup.AddMember(idOfInvitedUser);
            someGroup.DeleteMember(userId, idOfInvitedUser);
            var result = someGroup.Members.Count;
            
            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CreatorLeftTheGroup_HasNewOneAppeared()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var title = "some group";
            var description = "some description";
            var tags = new List<string> { "c#" };
            var size = 3;
            var moneyPerUser = 100.0;
            var idOfInvitedUser = Guid.NewGuid();
            var expectedRole = MemberRole.Creator;
            var expectedLength = 1;

            //Act
            var someGroup = new Group(userId, title, tags, description, size, moneyPerUser, false, GroupType.Lecture);
            someGroup.AddMember(idOfInvitedUser);
            someGroup.DeleteMember(userId, userId);
            var listOfMembers = someGroup.Members;
            var resultRole = listOfMembers[0].MemberRole;
            var resultLength = listOfMembers.Count;
            
            //Assert
            Assert.AreEqual(expectedRole, resultRole);
            Assert.AreEqual(expectedLength, resultLength);
        }

        [TestMethod]
        public void TryToApproveTeacher_TeacherIsSet()
        {
            //Arrange
            var teacher = new User("Sergey", new Credentials("email", "password"), true, UserType.User);
            var tags = new List<string> { "c#" };
            var group = new Group(Guid.NewGuid(), "SomeGroup", tags, "The best", 1, 0, false, GroupType.Seminar);

            //Act
            group.ApproveTeacher(teacher);

            //Assert
            Assert.AreEqual(teacher, group.Teacher);
        }

        [TestMethod]
        [ExpectedException(typeof(TeacherIsAlreadyFoundException))]
        public void TryToApproveAnotherTeacherWithApprovedTeacher_GetException()
        {
            //Arrange
            var approvedTeacher = new User("Sergey", new Credentials("email", "password"), true, UserType.User);
            var newTeacher = new User("Bogdan", new Credentials("email", "password"), true, UserType.User);
            var tags = new List<string> { "c#" };
            var group = new Group(Guid.NewGuid(), "SomeGroup", tags, "The best", 1, 0, false, GroupType.Seminar);

            //Act
            group.ApproveTeacher(approvedTeacher);
            group.ApproveTeacher(newTeacher);
        }

        [TestMethod]
        public void TryToOfferCourseWithApprovedTeacher_IsItPossible()
        {
            //Arrange
            var approvedTeacher = new User("Sergey", new Credentials("email", "password"), true, UserType.User);
            var tags = new List<string> { "c#" };
            var creatorId = Guid.NewGuid();
            var user1 = Guid.NewGuid();
            var user2 = Guid.NewGuid();
            var group = new Group(creatorId, "SomeGroup", tags, "The best", 3, 0, false, GroupType.Seminar);
            var expectedCurriculum = "Awesome course";
            
            //Act
            group.AddMember(user1);
            group.AddMember(user2);
            group.ApproveTeacher(approvedTeacher);
            group.OfferCurriculum(approvedTeacher.Id, expectedCurriculum);
            
            //Assert
            Assert.IsNotNull(group.GroupInfo.Curriculum);
        }

        [TestMethod]
        public void TryToStartCourseWithApprovedTeacherAndAllReadyMembers_IsItPossible()
        {
            //Arrange
            var approvedTeacher = new User("Sergey", new Credentials("email", "password"), true, UserType.User);
            var tags = new List<string> { "c#" };
            var creatorId = Guid.NewGuid();
            var user1 = Guid.NewGuid();
            var user2 = Guid.NewGuid();
            var group = new Group(creatorId, "SomeGroup", tags, "The best", 3, 0, false, GroupType.Seminar);
            var expectedCurriculum = "Awesome course";

            //Act
            group.AddMember(user1);
            group.AddMember(user2);
            group.ApproveTeacher(approvedTeacher);
            group.OfferCurriculum(approvedTeacher.Id, expectedCurriculum);
            group.AcceptCurriculum(creatorId);
            group.AcceptCurriculum(user1);
            group.AcceptCurriculum(user2);
            
            //Assert
            Assert.AreEqual(group.Status, EduHubLibrary.Domain.Tools.CourseStatus.Started);
        }
    }
}
