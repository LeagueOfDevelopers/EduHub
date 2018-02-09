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
        private GroupFacade _groupFacade;
        private UserFacade _userFacade;

        [TestInitialize]
        public void Initialize()
        {
            InMemoryUserRepository inMemoryUserRepository = new InMemoryUserRepository();
            InMemoryGroupRepository inMemoryGroupRepository = new InMemoryGroupRepository();
            _groupFacade = new GroupFacade(inMemoryGroupRepository, inMemoryUserRepository, new GroupSettings(3, 100, 0, 1000));
            _userFacade = new UserFacade(inMemoryUserRepository, inMemoryGroupRepository);
        }
        
        [TestMethod]
        public void SendMessageOfOneTypeToEventBus_GetMessageInNotifies()
        {
            //Arrange
            EventBus eventBus = new EventBus(_userFacade, _groupFacade);

            var creatorId = _userFacade.RegUser("Alena", new Credentials("email1", "password"), true, UserType.User);
            var createdGroupId = _groupFacade.CreateGroup(creatorId, "Some group", new List<string> { "c#" }, "You're welcome!", 3, 100, false, GroupType.Lecture);

            //Act
            eventBus.PublishEvent(new NewMemberEvent(createdGroupId, Guid.NewGuid()));

            //Assert
            List<string> notifies = _userFacade.GetNotifies(creatorId).ToList();
            Assert.AreEqual(1, notifies.Count);
        }
  
        [TestMethod]
        public void SendMessageOfTwoTypesInEventBus_GetMessageInBothCases()
        {
            //Arrange
            EventBus eventBus = new EventBus(_userFacade, _groupFacade);

            var creatorId = _userFacade.RegUser("Alena", new Credentials("email1", "password"), true, UserType.User);
            var invitedId = _userFacade.RegUser("Somebody", new Credentials("email2", "password"), true, UserType.User);
            
            var createdGroupId = _groupFacade.CreateGroup(creatorId, "Some group", new List<string> { "c#" }, "You're welcome!", 3, 100, false, GroupType.Lecture);
          
            //Act
            eventBus.PublishEvent(new NewMemberEvent(createdGroupId, Guid.NewGuid()));
            eventBus.PublishEvent(new InvitationEvent(new Invitation(creatorId, invitedId, createdGroupId, MemberRole.Member, InvitationStatus.InProgress)));

            //Assert
            List<string> notifies = _userFacade.GetNotifies(creatorId).ToList();
            Assert.AreEqual(1, notifies.Count);

            List<Invitation> groupInvitations = _groupFacade.GetAllInvitations(createdGroupId).ToList();
            Assert.AreEqual(1, groupInvitations.Count);
        }
    }
}
