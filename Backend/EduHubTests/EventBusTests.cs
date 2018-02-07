using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Consumers;
using EduHubLibrary.Domain.Events;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Facades;
using EduHubLibrary.Infrastructure;
using EduHubLibrary.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EduHubTests
{
    [TestClass]
    public class EventBusTests
    {
        /*
        [TestMethod]
        public void SendMessageOfOneTypeToEventBus_GetMessageInNotifies()
        {
            //Arrange
            InMemoryUserRepository inMemoryUserRepository = new InMemoryUserRepository();
            InMemoryGroupRepository inMemoryGroupRepository = new InMemoryGroupRepository();
            GroupFacade groupFacade = new GroupFacade(inMemoryGroupRepository, inMemoryUserRepository, new GroupSettings(3, 100, 0, 1000));
            UserFacade userFacade = new UserFacade(inMemoryUserRepository, inMemoryGroupRepository);

            EventBus eventBus = new EventBus();
            eventBus.RegisterConsumer(new GroupEventsConsumer(userFacade, groupFacade));

            userFacade.RegUser("Alena", new Credentials("email1", "password"), true, UserType.User);
            List<User> allUsers = userFacade.GetUsers().ToList();
            Guid creatorId = allUsers[0].Id;

            var tags = new List<string> { "c#" };

            groupFacade.CreateGroup(creatorId, "Some group", tags, "You're welcome!", 3, 100, false, GroupType.Lecture);
            List<Group> allGroups = groupFacade.GetGroups().ToList();
            var createdGroup = allGroups[0];

            //Act
            eventBus.PublishEvent(new NewMemberEvent(createdGroup.GroupInfo.Id, Guid.NewGuid()));

            //Assert
            List<string> notifies = userFacade.GetNotifies(creatorId).ToList();
            Assert.AreEqual(1, notifies.Count);
        }

        [TestMethod]
        public void SendMessageOfTwoTypesInEventBus_GetMessageInBothCases()
        {
            //Arrange
            InMemoryUserRepository inMemoryUserRepository = new InMemoryUserRepository();
            InMemoryGroupRepository inMemoryGroupRepository = new InMemoryGroupRepository();
            GroupFacade groupFacade = new GroupFacade(inMemoryGroupRepository, inMemoryUserRepository, new GroupSettings(3, 100, 0, 1000));
            UserFacade userFacade = new UserFacade(inMemoryUserRepository, inMemoryGroupRepository);

            EventBus eventBus = new EventBus();
            eventBus.RegisterConsumer(new GroupEventsConsumer(userFacade, groupFacade));
            eventBus.RegisterConsumer(new InvitationConsumer(groupFacade));

            userFacade.RegUser("Alena", new Credentials("email1", "password"), true, UserType.User);
            userFacade.RegUser("Somebody", new Credentials("email2", "password"), true, UserType.User);
            List<User> allUsers = userFacade.GetUsers().ToList();
            Guid creatorId = allUsers[0].Id;
            Guid invitedId = allUsers[1].Id;

            var tags = new List<string> { "c#" };

            groupFacade.CreateGroup(creatorId, "Some group", tags, "You're welcome!", 3, 100, false, GroupType.Lecture);
            List<Group> allGroups = groupFacade.GetGroups().ToList();
            var createdGroup = allGroups[0];

            //Act
            eventBus.PublishEvent(new NewMemberEvent(createdGroup.GroupInfo.Id, Guid.NewGuid()));
            eventBus.PublishEvent(new InvitationEvent(new Invitation(creatorId, invitedId, createdGroup.GroupInfo.Id, MemberRole.Member, InvitationStatus.InProgress)));

            //Assert
            List<string> notifies = userFacade.GetNotifies(creatorId).ToList();
            Assert.AreEqual(1, notifies.Count);

            List<Invitation> groupInvitations = groupFacade.GetAllInvitations(createdGroup.GroupInfo.Id).ToList();
            Assert.AreEqual(1, groupInvitations.Count);
        }
        */
    }
}
