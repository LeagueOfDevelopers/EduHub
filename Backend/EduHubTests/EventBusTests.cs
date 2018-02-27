using EduHubLibrary.Domain;
using EduHubLibrary.Facades;
using EduHubLibrary.Infrastructure;
using EduHubLibrary.Mailing;
using EduHubLibrary.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EduHubTests
{
    [TestClass]
    public class EventBusTests
    {
        private IAuthUserFacade _authUserFacade;
        private IGroupFacade _groupFacade;
        private IUserFacade _userFacade;

        [TestInitialize]
        public void Initialize()
        {
            var inMemoryUserRepository = new InMemoryUserRepository();
            var inMemoryGroupRepository = new InMemoryGroupRepository();
            var inMemoryKeysRepository = new InMemoryKeysRepository();
            var emailSettings = new EmailSettings("", "", "", "", "", 4);
            var emailSender = new EmailSender(emailSettings);
            _groupFacade = new GroupFacade(inMemoryGroupRepository, inMemoryUserRepository,
                new GroupSettings(3, 100, 0, 1000), new TagsManager());
            _userFacade = new UserFacade(inMemoryUserRepository, inMemoryGroupRepository);
            _authUserFacade = new AuthUserFacade(inMemoryKeysRepository, inMemoryUserRepository,
                emailSender);
        }

        /*
        [TestMethod]
        public void SendMessageOfOneTypeToEventBus_GetMessageInNotifies()
        {
            //Arrange
            var eventBus = new EventBus();
            eventBus.RegisterConsumer<NewMemberEvent>(new GroupEventsConsumer(_userFacade, _groupFacade),
                EventType.NewMemberEvent);

            var creatorId =
                _authUserFacade.RegUser("Alena", new Credentials("email1", "password"), true, UserType.User);
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

            var creatorId =
                _authUserFacade.RegUser("Alena", new Credentials("email1", "password"), true, UserType.User);
            var invitedId =
                _authUserFacade.RegUser("Somebody", new Credentials("email2", "password"), true, UserType.User);

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
        

        [TestMethod]
        public void FirstTestOfRabbit()
        {
            //Arrange
            var bus = new EventConsumersContainer(new EventBusSettings("localhost", "", "", ""));
            bus.StartListening();
            bus.RegisterConsumer(new InvitationConsumer(_groupFacade));

            var creatorId =
                _authUserFacade.RegUser("Alena", new Credentials("email1", "password"), true, UserType.User);
            var invitedId =
                _authUserFacade.RegUser("Somebody", new Credentials("email2", "password"), true, UserType.User);

            var createdGroupId = _groupFacade.CreateGroup(creatorId, "Some group", new List<string> {"c#"},
                "You're welcome!", 3, 100, false, GroupType.Lecture);

            //Act
            bus.GetEventPublisher().PublishEvent(new InvitationEvent(new Invitation(
                Guid.NewGuid(), invitedId, createdGroupId, MemberRole.Member, InvitationStatus.InProgress)));

            //Assert
            var notifies = _userFacade.GetNotifies(creatorId).ToList();
            Assert.AreEqual(1, notifies.Count);

            var groupInvitations = _groupFacade.GetAllInvitations(createdGroupId).ToList();
            Assert.AreEqual(1, groupInvitations.Count);
        }
        */
    }
}