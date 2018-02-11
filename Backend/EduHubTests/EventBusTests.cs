using System;
using System.Collections.Generic;
using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Consumers;
using EduHubLibrary.Domain.Events;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Facades;
using EduHubLibrary.Infrastructure;
using EduHubLibrary.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace EduHubTests
{
    [TestClass]
    public class EventBusTests
    {
        private GroupFacade _groupFacade;
        private UserFacade _userFacade;

        [TestInitialize]
        public void Initialize()
        {
            var inMemoryUserRepository = new InMemoryUserRepository();
            var inMemoryGroupRepository = new InMemoryGroupRepository();
            _groupFacade = new GroupFacade(inMemoryGroupRepository, inMemoryUserRepository,
                new GroupSettings(3, 100, 0, 1000));
            _userFacade = new UserFacade(inMemoryUserRepository, inMemoryGroupRepository);
        }

        [TestMethod]
        public void SendMessageOfOneTypeToEventBus_GetMessageInNotifies()
        {
            //Arrange
            var eventBus = new EventBus();
            eventBus.RegisterConsumer<NewMemberEvent>(new GroupEventsConsumer(_userFacade, _groupFacade),
                EventType.NewMemberEvent);

            var creatorId = _userFacade.RegUser("Alena", new Credentials("email1", "password"), true, UserType.User);
            var createdGroupId = _groupFacade.CreateGroup(creatorId, "Some group", new List<string> {"c#"},
                "You're welcome!", 3, 100, false, GroupType.Lecture);

            //Act
            eventBus.PublishEvent(new NewMemberEvent(createdGroupId, Guid.NewGuid()));

            //Assert
            var notifies = _userFacade.GetNotifies(creatorId).ToList();
            Assert.AreEqual(1, notifies.Count);
        }

        [TestMethod]
        public void SendMessageOfTwoTypesToEventBus_GetMessageInBothCases()
        {
            //Arrange
            var eventBus = new EventBus();
            eventBus.RegisterConsumer<NewMemberEvent>(new GroupEventsConsumer(_userFacade, _groupFacade),
                EventType.NewMemberEvent);
            eventBus.RegisterConsumer(new InvitationConsumer(_groupFacade), EventType.InvitationEvent);

            var creatorId = _userFacade.RegUser("Alena", new Credentials("email1", "password"), true, UserType.User);
            var invitedId = _userFacade.RegUser("Somebody", new Credentials("email2", "password"), true, UserType.User);

            var createdGroupId = _groupFacade.CreateGroup(creatorId, "Some group", new List<string> {"c#"},
                "You're welcome!", 3, 100, false, GroupType.Lecture);

            //Act
            eventBus.PublishEvent(new NewMemberEvent(createdGroupId, Guid.NewGuid()));
            eventBus.PublishEvent(new InvitationEvent(new Invitation(creatorId, invitedId, createdGroupId,
                MemberRole.Member, InvitationStatus.InProgress)));

            //Assert
            var notifies = _userFacade.GetNotifies(creatorId).ToList();
            Assert.AreEqual(1, notifies.Count);

            var groupInvitations = _groupFacade.GetAllInvitations(createdGroupId).ToList();
            Assert.AreEqual(1, groupInvitations.Count);
        }
    }
}