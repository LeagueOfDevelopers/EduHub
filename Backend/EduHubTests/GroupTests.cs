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
            var tags = new List<string>();
            var size = 3;
            var moneyPerUser = 100.0;
            tags.Add("js");
            GroupInfo info = new GroupInfo(Guid.NewGuid(), title, description, tags, GroupType.Lecture, false, true, size, moneyPerUser);
            //Act
            var someGroup = new Group(userId, title, tags, description, size, moneyPerUser, false, GroupType.Lecture);
            //Assert
            Assert.AreEqual(userId, someGroup.GetMemberById(userId).UserId);
        }

        [ExpectedException(typeof(MemberNotFoundException)), TestMethod]
        public void TryToDeleteNotExistingMember_IsItPossible()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var title = "some group";
            var description = "some description";
            var tags = new List<string>();
            var size = 3;
            var moneyPerUser = 100.0;
            tags.Add("js");
            
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
            var tags = new List<string>();
            var size = 3;
            var moneyPerUser = 100.0;
            tags.Add("js");
            
            //Act
            var someGroup = new Group(userId, title, tags, description, size, moneyPerUser, false, GroupType.Lecture);
            someGroup.AddMember(userId, idOfInvitedUser);
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
            var tags = new List<string>();
            var size = 3;
            var moneyPerUser = 100.0;
            tags.Add("js");
            
            //Act
            var someGroup = new Group(userId, title, tags, description, size, moneyPerUser, false, GroupType.Lecture);
            someGroup.AddMember(userId, idOfInvitedUser);
            someGroup.DeleteMember(idOfInvitedUser, idOfInvitedUser);
            var result = someGroup.GetAllMembers().ToArray().Length;
            
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
            var tags = new List<string>();
            tags.Add("js");
            var idOfInvitedUser = Guid.NewGuid();
            var expected = 2;
            var size = 3;
            var moneyPerUser = 100.0;
            tags.Add("js");
            
            //Act
            var someGroup = new Group(userId, title, tags, description, size, moneyPerUser, false, GroupType.Lecture);
            someGroup.AddMember(userId, idOfInvitedUser);
            var result = someGroup.GetAllMembers().ToArray().Length;
            
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
            var tags = new List<string>();
            var size = 3;
            var moneyPerUser = 100.0;
            tags.Add("js");
            var expected = 1;
            
            //Act
            var someGroup = new Group(userId, title, tags, description, size, moneyPerUser, false, GroupType.Lecture);
            someGroup.AddMember(userId, idOfInvitedUser);
            someGroup.DeleteMember(userId, idOfInvitedUser);
            var result = someGroup.GetAllMembers().ToArray().Length;
            
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
            var tags = new List<string>();
            var size = 3;
            var moneyPerUser = 100.0;
            tags.Add("js");
            var idOfInvitedUser = Guid.NewGuid();
            var expectedRole = MemberRole.Creator;
            var expectedLength = 1;

            //Act
            var someGroup = new Group(userId, title, tags, description, size, moneyPerUser, false, GroupType.Lecture);
            someGroup.AddMember(userId, idOfInvitedUser);
            someGroup.DeleteMember(userId, userId);
            var listOfMembers = someGroup.GetAllMembers().ToList();
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
            User teacher = new User("Sergey", new Credentials("email", "password"), true, TypeOfUser.User, "avatar");
            List<string> tags = new List<string>();
            tags.Add("The best group");
            Group group = new Group(Guid.NewGuid(), "SomeGroup", tags, "The best", 1, 0, false, GroupType.Seminar);

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
            User approvedTeacher = new User("Sergey", new Credentials("email", "password"), true, TypeOfUser.User, "avatar");
            User newTeacher = new User("Bogdan", new Credentials("email", "password"), true, TypeOfUser.User, "avatar");
            List<string> tags = new List<string>();
            tags.Add("The best group");
            Group group = new Group(Guid.NewGuid(), "SomeGroup", tags, "The best", 1, 0, false, GroupType.Seminar);

            //Act
            group.ApproveTeacher(approvedTeacher);
            group.ApproveTeacher(newTeacher);
        }

        [TestMethod]
        public void TryToOfferCourseWithApprovedTeacher_IsItPossible()
        {
            //Arrange
            User approvedTeacher = new User("Sergey", new Credentials("email", "password"), true, TypeOfUser.User, "avatar");
            List<string> tags = new List<string>();
            tags.Add("The best group");
            Guid creatorId = Guid.NewGuid();
            Guid user1 = Guid.NewGuid();
            Guid user2 = Guid.NewGuid();
            Group group = new Group(creatorId, "SomeGroup", tags, "The best", 3, 0, false, GroupType.Seminar);
            Course course = new Course("some awesome course");
            
            //Act
            group.AddMember(creatorId, user1);
            group.AddMember(user1, user2);
            group.ApproveTeacher(approvedTeacher);
            group.OfferCourse(approvedTeacher.Id, course);
            
            //Assert
            Assert.IsNotNull(group.Course);
        }

        [TestMethod]
        public void TryToStartCourseWithApprovedTeacherAndAllReadyMembers_IsItPossible()
        {
            //Arrange
            User approvedTeacher = new User("Sergey", new Credentials("email", "password"), true, TypeOfUser.User, "avatar");
            List<string> tags = new List<string>();
            tags.Add("The best group");
            Guid creatorId = Guid.NewGuid();
            Guid user1 = Guid.NewGuid();
            Guid user2 = Guid.NewGuid();
            Group group = new Group(creatorId, "SomeGroup", tags, "The best", 3, 0, false, GroupType.Seminar);
            Course course = new Course("some awesome course");
            
            //Act
            group.AddMember(creatorId, user1);
            group.AddMember(user1, user2);
            group.ApproveTeacher(approvedTeacher);
            group.OfferCourse(approvedTeacher.Id, course);
            group.AcceptCourse(creatorId);
            group.AcceptCourse(user1);
            group.AcceptCourse(user2);
            
            //Assert
            Assert.AreEqual(group.Course.CourseStatus, EduHubLibrary.Domain.Tools.CourseStatus.Started);
        }

        [TestMethod]
        [ExpectedException(typeof(GroupIsNotFullException))]
        public void TryToApproveTeacherWithNotFullGroup_IsItPossible()
        {
            //Arrange
            User approvedTeacher = new User("Sergey", new Credentials("email", "password"), true, TypeOfUser.User, "avatar");
            List<string> tags = new List<string>();
            tags.Add("The best group");
            Guid creatorId = Guid.NewGuid();
            Guid user1 = Guid.NewGuid();
            Guid user2 = Guid.NewGuid();
            Group group = new Group(creatorId, "SomeGroup", tags, "The best", 3, 0, false, GroupType.Seminar);
            Course course = new Course("some awesome course");
            
            //Act
            group.AddMember(creatorId, user1);
            group.ApproveTeacher(approvedTeacher);
        }
    }
}
