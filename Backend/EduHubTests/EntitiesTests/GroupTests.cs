using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Domain.Tools;
using EduHubLibrary.Interators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EduHubTests
{
    [TestClass]
    public class GroupTests
    {
        [TestMethod]
        public void CreateGroupWithSomeData_GroupWasCreated()
        {
            //Arrange
            var creatorId = IntIterator.GetNextId();
            var title = "some group";
            var description = "some description";
            var tags = new List<string> {"c#"};
            var size = 3;
            var moneyPerUser = 100.0;

            //Act
            var someGroup = new Group(creatorId, title, tags, description, size, moneyPerUser, false,
                GroupType.Lecture);

            //Assert
            Assert.AreEqual(creatorId, someGroup.GetMember(creatorId).UserId);
        }

        [TestMethod]
        public void AddUserToGroup_UserWasAddedToGroup()
        {
            //Arrange
            var creatorId = IntIterator.GetNextId();
            var invitedUserId = IntIterator.GetNextId();
            var someGroup = new Group(creatorId, "SomeGroup", new List<string> {"c#"},
                "The best", 3, 0, false, GroupType.Seminar);

            //Act
            someGroup.AddMember(invitedUserId);
            var expectedQuantity = 2;
            var actualQuantity = someGroup.Members.Count;

            //Assert
            Assert.AreEqual(expectedQuantity, actualQuantity);
        }

        [TestMethod]
        [ExpectedException(typeof(GroupIsFullException))]
        public void TryToAddUserToFullGroup_GetException()
        {
            //Arrange
            var creatorId = IntIterator.GetNextId();
            var someGroup = new Group(creatorId, "SomeGroup", new List<string> {"c#"},
                "The best", 1, 0, false, GroupType.Seminar);

            //Act
            someGroup.AddMember(IntIterator.GetNextId());
        }

        [TestMethod]
        [ExpectedException(typeof(AlreadyMemberException))]
        public void TryToAddAlreadyAddedUser_GetException()
        {
            //Arrange
            var creatorId = IntIterator.GetNextId();
            var invitedUserId = IntIterator.GetNextId();
            var someGroup = new Group(creatorId, "SomeGroup", new List<string> {"c#"},
                "The best", 3, 0, false, GroupType.Seminar);
            someGroup.AddMember(invitedUserId);

            //Act
            someGroup.AddMember(invitedUserId);
        }

        [TestMethod]
        [ExpectedException(typeof(GroupIsNotActiveException))]
        public void TryToAddUserToInactiveGroup_GetException()
        {
            //Arrange
            var creatorId = IntIterator.GetNextId();
            var invitedUserId = IntIterator.GetNextId();
            var someGroup = new Group(creatorId, "SomeGroup", new List<string> {"c#"},
                "The best", 3, 0, false, GroupType.Seminar);
            someGroup.DeleteMember(creatorId, creatorId);

            //Act
            someGroup.AddMember(invitedUserId);
        }

        [TestMethod]
        public void DeleteUserFromGroupByAdmin_UserWasDeleted()
        {
            //Arrange
            var creatorId = IntIterator.GetNextId();
            var invitedUserId = IntIterator.GetNextId();
            var someGroup = new Group(creatorId, "SomeGroup", new List<string> {"c#"},
                "The best", 3, 0, false, GroupType.Seminar);

            //Act
            someGroup.AddMember(invitedUserId);
            someGroup.DeleteMember(creatorId, invitedUserId);
            var expectedQuantity = 1;
            var resultQuantity = someGroup.Members.Count;

            //Assert
            Assert.AreEqual(expectedQuantity, resultQuantity);
        }

        [ExpectedException(typeof(MemberNotFoundException))]
        [TestMethod]
        public void TryToDeleteNotExistingMember_GetException()
        {
            //Arrange
            var creatorId = IntIterator.GetNextId();
            var someGroup = new Group(creatorId, "SomeGroup", new List<string> {"c#"},
                "The best", 3, 0, false, GroupType.Seminar);

            //Act
            someGroup.DeleteMember(creatorId, IntIterator.GetNextId());
        }

        [ExpectedException(typeof(NotEnoughPermissionsException))]
        [TestMethod]
        public void TryToDeleteWithNotEnoughtRights_GetException()
        {
            //Arrange
            var creatorId = IntIterator.GetNextId();
            var invitedUserId = IntIterator.GetNextId();
            var someGroup = new Group(creatorId, "SomeGroup", new List<string> {"c#"},
                "The best", 3, 0, false, GroupType.Seminar);

            //Act
            someGroup.AddMember(invitedUserId);
            someGroup.DeleteMember(invitedUserId, creatorId);
        }

        [TestMethod]
        public void DeleteYourselfFromGroup_ToBeDeleted()
        {
            //Arrange
            var creatorId = IntIterator.GetNextId();
            var invitedUserId = IntIterator.GetNextId();
            var someGroup = new Group(creatorId, "SomeGroup", new List<string> {"c#"},
                "The best", 3, 0, false, GroupType.Seminar);

            //Act
            someGroup.AddMember(invitedUserId);
            someGroup.DeleteMember(invitedUserId, invitedUserId);
            var expectedQuantity = 1;
            var resultQuantity = someGroup.Members.Count;

            //Assert
            Assert.AreEqual(expectedQuantity, resultQuantity);
        }

        [TestMethod]
        public void CreatorLeftTheGroup_MemberBecameCreator()
        {
            //Arrange
            var creatorId = IntIterator.GetNextId();
            var invitedUserId = IntIterator.GetNextId();
            var someGroup = new Group(creatorId, "SomeGroup", new List<string> {"c#"},
                "The best", 3, 0, false, GroupType.Seminar);

            //Act
            someGroup.AddMember(invitedUserId);
            someGroup.DeleteMember(creatorId, creatorId);

            var expectedRole = MemberRole.Creator;
            var expectedQuantity = 1;
            var resultRole = someGroup.Members[0].MemberRole;
            var resultLength = someGroup.Members.Count;

            //Assert
            Assert.AreEqual(expectedRole, resultRole);
            Assert.AreEqual(expectedQuantity, resultLength);
        }

        [TestMethod]
        public void ApproveTeacher_TeacherIsSet()
        {
            //Arrange
            var teacher = new User("Sergey", new Credentials("email", "password"), true, UserType.User);
            var group = new Group(IntIterator.GetNextId(), "SomeGroup", new List<string> {"c#"},
                "The best", 1, 0, false, GroupType.Seminar);

            //Act
            group.ApproveTeacher(teacher);

            //Assert
            Assert.AreEqual(teacher, group.Teacher);
        }

        [TestMethod]
        public void AddInvitation_GetRightListOfInvitations()
        {
            //Arrange
            var group = new Group(IntIterator.GetNextId(), "SomeGroup", new List<string> {"c#"},
                "The best", 1, 0, false, GroupType.Seminar);
            var invitation = new Invitation(IntIterator.GetNextId(), IntIterator.GetNextId(), group.GroupInfo.Id,
                MemberRole.Member, InvitationStatus.InProgress);

            //Act
            group.AddInvitation(invitation);

            //Assert
            Assert.AreEqual(1, group.Invitations.Count);
            Assert.AreEqual(invitation, group.Invitations[0]);
        }

        [TestMethod]
        public void CheckIfGroupContainsExistingTags_GetTrue()
        {
            //Arrange
            var group = new Group(IntIterator.GetNextId(), "SomeGroup", new List<string> {"c#", "c++", "js"},
                "The best", 1, 0, false, GroupType.Seminar);

            //Act
            var expected = true;
            var actual = group.DoesContainsTags(new List<string> {"c++", "c#"});

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckIfGroupContainsNotExistingTags_GetFalse()
        {
            //Arrange
            var group = new Group(IntIterator.GetNextId(), "SomeGroup", new List<string> {"c#", "c++", "js"},
                "The best", 1, 0, false, GroupType.Seminar);

            //Act
            var expected = false;
            var actual = group.DoesContainsTags(new List<string> {"c++", "ada"});

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(TeacherIsAlreadyFoundException))]
        public void TryToApproveAnotherTeacherWithApprovedTeacher_GetException()
        {
            //Arrange
            var approvedTeacher = new User("Sergey", new Credentials("email", "password"), true, UserType.User);
            var newTeacher = new User("Bogdan", new Credentials("email", "password"), true, UserType.User);
            var group = new Group(IntIterator.GetNextId(), "SomeGroup", new List<string> {"c#"},
                "The best", 1, 0, false, GroupType.Seminar);

            //Act
            group.ApproveTeacher(approvedTeacher);
            group.ApproveTeacher(newTeacher);
        }

        [TestMethod]
        public void OfferCourseWithApprovedTeacher_GetOfferedCourse()
        {
            //Arrange
            var approvedTeacher = new User("Sergey", new Credentials("email", "password"), true, UserType.User);
            var creatorId = IntIterator.GetNextId();
            var invitedUserId = IntIterator.GetNextId();
            var group = new Group(creatorId, "SomeGroup", new List<string> {"c#"},
                "The best", 2, 0, false, GroupType.Seminar);
            var expectedCurriculum = "Awesome course";

            //Act
            group.AddMember(invitedUserId);
            group.ApproveTeacher(approvedTeacher);
            group.OfferCurriculum(approvedTeacher.Id, expectedCurriculum);

            //Assert
            Assert.AreEqual(group.GroupInfo.Curriculum, expectedCurriculum);
        }

        [TestMethod]
        public void StartCourseWithApprovedTeacherAndAllReadyMembers_CourseWasStarted()
        {
            //Arrange
            var approvedTeacher = new User("Sergey", new Credentials("email", "password"), true, UserType.User);
            var creatorId = IntIterator.GetNextId();
            var user1Id = IntIterator.GetNextId();
            var user2Id = IntIterator.GetNextId();
            var group = new Group(creatorId, "SomeGroup", new List<string> {"c#"},
                "The best", 3, 0, false, GroupType.Seminar);
            var expectedCurriculum = "Awesome course";

            //Act
            group.AddMember(user1Id);
            group.AddMember(user2Id);
            group.ApproveTeacher(approvedTeacher);
            group.OfferCurriculum(approvedTeacher.Id, expectedCurriculum);
            group.AcceptCurriculum(creatorId);
            group.AcceptCurriculum(user1Id);
            group.AcceptCurriculum(user2Id);

            //Assert
            Assert.AreEqual(group.Status, CourseStatus.Started);
        }

        [TestMethod]
        public void CommitChatSession_GetUpdatedChatHistory()
        {
            //Arrange
            var creatorId = IntIterator.GetNextId();
            var group = new Group(creatorId, "SomeGroup", new List<string> {"c#"},
                "The best", 3, 0, false, GroupType.Seminar);

            var someMessages = new List<BaseMessage>
            {
                new UserMessage(creatorId, "message1"),
                new UserMessage(creatorId, "message2"),
                new UserMessage(creatorId, "message3")
            };

            //Act
            group.CommitChatSession(someMessages);

            //Assert
            Assert.AreEqual(true, someMessages.SequenceEqual(group.Messages));
        }
    }
}