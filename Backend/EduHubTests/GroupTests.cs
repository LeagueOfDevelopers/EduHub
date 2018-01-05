using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using System.Linq;
using System.Collections.Generic;

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
            GroupInfo info = new GroupInfo(title, description, tags, GroupType.Lecture, false, true, size, moneyPerUser);
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
        [ExpectedException(typeof(NotEnoughPermissionsException))]
        public void TryToChangeSizeByMember_IsItPossible()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var title = "some group";
            var description = "some description";
            var tags = new List<string>();
            var size = 4;
            var moneyPerUser = 100.0;
            var idOfInvited = Guid.NewGuid();
            tags.Add("js");
            //Act
            var someGroup = new Group(userId, title, tags, description, size, moneyPerUser, false, GroupType.Lecture);
            someGroup.AddMember(userId, idOfInvited);
            someGroup.ChangeSizeOfGroup(idOfInvited, 10);
        }

    }

}
