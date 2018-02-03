using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Events;
using EduHubLibrary.Domain.NotificationService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubTests
{
    [TestClass]
    public class EventBusTests
    {
        [TestMethod]
        public void AddNewSubscriberInSubscription_GetListOfMembersWithOneMember()
        {
            //Arrange
            var subscription = new Subscription(new EditedGroupEvent(Guid.NewGuid()));
            var subscriber = new User("Alena", Credentials.FromRawData("alena", "password"), false, UserType.User, "avatar");

            //Act
            subscription.AddSubscriber(subscriber);

            //Assert
            Assert.AreEqual(1, subscription.GetSubscribers().Count);
        }

        [TestMethod]
        public void RemoveSingleSubscriberFronSubscription_GetEmptyListOfMembers()
        {
            //Arrange
            var subscription = new Subscription(new EditedGroupEvent(Guid.NewGuid()));
            var subscriber = new User("Alena", Credentials.FromRawData("alena", "password"), false, UserType.User, "avatar");
            
            subscription.AddSubscriber(subscriber);

            //Act
            subscription.RemoveSubscriber(subscriber);

            //Assert
            Assert.AreEqual(0, subscription.GetSubscribers().Count);
        }

        [TestMethod]
        public void CheckIfSubscriptionIsInterestedInEvent_GetRightResult()
        {
            //Arrange
            var groupId = Guid.NewGuid();
            var subscription = new Subscription(new EditedGroupEvent(groupId));

            //Act
            bool actual = subscription.IsInterestedInEvent(new EditedGroupEvent(groupId));

            //Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void AddNewSubscriptionInEmptyBus_GetListWithOneSubscription()
        {
            //Arrange
            var bus = new EventBus();
            var subscriber = new User("Alena", Credentials.FromRawData("alena", "password"), false, UserType.User, "avatar");

            //Act
            bus.AddSubscriber(subscriber, new EditedGroupEvent(Guid.NewGuid()));

            //Assert
            Assert.AreEqual(1, bus.GetSubscriptions().Count);
        }

        [TestMethod]
        public void RemoveSingleSubscriptionInBus_GetEmptyList()
        {
            //Arrange
            var bus = new EventBus();
            var subscriber = new User("Alena", Credentials.FromRawData("alena", "password"), false, UserType.User, "avatar");
            IEventInfo eventInfo = new EditedGroupEvent(Guid.NewGuid());
            bus.AddSubscriber(subscriber, eventInfo);

            //Act
            bus.RemoveSubscriber(subscriber, eventInfo);

            //Assert
            Assert.AreEqual(0, bus.GetSubscriptions().Count);
        }

        [TestMethod]
        public void SendMessageToBus_OnlySubscribedUserGetNotify()
        {
            //Arrange
            var messageBus = new EventBus();
            var user1 = new User("name", Credentials.FromRawData("email", "password"), false, UserType.User, "avatar");
            var user2 = new User("name", Credentials.FromRawData("email2", "password"), false, UserType.User, "avatar");
            var groupId = Guid.NewGuid();
            var @event = new EditedGroupEvent(groupId);
            messageBus.AddSubscriber(user1, @event);

            //Act
            messageBus.SendMessage(new Event(@event));
            messageBus.Notify();

            //Assert
            Assert.AreEqual(user1.GetNotifies().Count, 1);
            Assert.AreEqual(user2.GetNotifies().Count, 0);
        }

        [TestMethod]
        public void TryToGetNotifyAboutInvitationInGroup_GetListOfInvitationWithOneInvitation()
        {
            //Arrange
            var messageBus = new EventBus();
            var user = new User("name", Credentials.FromRawData("email", "password"), false, UserType.User, "avatar");
            var groupId = Guid.NewGuid();
            var @event = new EditedGroupEvent(groupId);
            messageBus.AddSubscriber(user, @event);
            
            var userId = Guid.NewGuid();
            var title = "some group";
            var description = "some description";
            var tags = new List<string>();
            var size = 3;
            var moneyPerUser = 100.0;
            tags.Add("js");
            var someGroup = new Group(userId, title, tags, description, size, moneyPerUser, false, GroupType.Lecture);
            messageBus.AddSubscriber(someGroup, new InvitationToGroupEvent(new Invitation(Guid.NewGuid(), Guid.NewGuid(), someGroup.GroupInfo.Id, MemberRole.Default, InvitationStatus.Unknown)));

            //Act
            messageBus.SendMessage(new Event(new InvitationToGroupEvent(new Invitation(Guid.NewGuid(), Guid.NewGuid(), someGroup.GroupInfo.Id, MemberRole.Creator, InvitationStatus.InProgress))));
            messageBus.Notify();

            //Assert
            Assert.AreEqual(someGroup.GetAllInvitation().Count, 1);
        }
    }
}
