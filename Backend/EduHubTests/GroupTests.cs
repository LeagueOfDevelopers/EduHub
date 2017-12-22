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
            var idOfUser = Guid.NewGuid();
            //Act
            var someGroup = new Group(idOfUser);
            //Assert
            Assert.AreEqual(idOfUser, someGroup.GetMemberById(idOfUser).UserId);
        }

        [ExpectedException(typeof(MemberNotFoundException)), TestMethod]
        public void TryToDeleteNotExistingMember_IsItPossible()
        {
            //Arrange
            var idOfUser = Guid.NewGuid();
            //Act
            var someGroup = new Group(idOfUser);
            someGroup.DeleteMember(idOfUser, Guid.NewGuid());
        }

        [ExpectedException(typeof(NotEnoughPermissionsException)), TestMethod]
        public void TryToDeleteWithNotEnoughtRights_IsItPossible()
        {
            //Arrange
            var idOfUser = Guid.NewGuid();
            var idOfInvitedUser = Guid.NewGuid();
            //Act
            var someGroup = new Group(idOfUser);
            someGroup.AddMember(idOfUser, idOfInvitedUser);
            someGroup.DeleteMember(idOfInvitedUser, idOfUser);
        }

        [TestMethod]
        public void TryToDeleteYourselfFromGroup_HasItDeleted()
        {
            //Arrange
            var idOfUser = Guid.NewGuid();
            var idOfInvitedUser = Guid.NewGuid();
            var expected = 1;
            //Act
            var someGroup = new Group(idOfUser);
            someGroup.AddMember(idOfUser, idOfInvitedUser);
            someGroup.DeleteMember(idOfInvitedUser, idOfInvitedUser);
            var result = someGroup.GetAllMembers().ToArray().Length;
            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TryToAddUserToGroup_HasItAdded()
        {
            //Arrange
            var idOfUser = Guid.NewGuid();
            var idOfInvitedUser = Guid.NewGuid();
            var expected = 2;
            //Act
            var someGroup = new Group(idOfUser);
            someGroup.AddMember(idOfUser, idOfInvitedUser);
            var result = someGroup.GetAllMembers().ToArray().Length;
            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TryToDeleteUserFromGroupByAdmin_HasItDeleted()
        {
            //Arrange
            var idOfCreator = Guid.NewGuid();
            var idOfInvitedUser = Guid.NewGuid();
            var expected = 1;
            //Act
            var someGroup = new Group(idOfCreator);
            someGroup.AddMember(idOfCreator, idOfInvitedUser);
            someGroup.DeleteMember(idOfCreator, idOfInvitedUser);
            var result = someGroup.GetAllMembers().ToArray().Length;
            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CreatorLeftTheGroup_HasNewOneAppeared()
        {
            //Arrange
            var idOfCreator = Guid.NewGuid();
            var idOfInvitedUser = Guid.NewGuid();
            var expectedRole = MemberRole.Creator;
            var expectedLength = 1;

            //Act
            var someGroup = new Group(idOfCreator);
            someGroup.AddMember(idOfCreator, idOfInvitedUser);
            someGroup.DeleteMember(idOfCreator, idOfCreator);
            var listOfMembers = someGroup.GetAllMembers().ToList();
            var resultRole = listOfMembers[0].MemberRole;
            var resultLength = listOfMembers.Count;
            //Assert
            Assert.AreEqual(expectedRole, resultRole);
            Assert.AreEqual(expectedLength, resultLength);
        }
    }

}
