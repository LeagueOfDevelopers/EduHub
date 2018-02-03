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
    public class MessageBusTests
    {
        [TestMethod]
        public void TryToGetNotify()
        {
            EventBus messageBus = new EventBus();
            User user = new User("name", Credentials.FromRawData("email", "password"), false, UserType.User, "avatar");
            User user2 = new User("name", Credentials.FromRawData("email2", "password"), false, UserType.User, "avatar");
            Guid id = Guid.NewGuid();
            Guid id2 = Guid.NewGuid();
            EditedGroupEvent @event = new EditedGroupEvent(id);
            EditedGroupEvent @event2 = new EditedGroupEvent(id2);
            messageBus.AddSubscriber(user, @event);
            messageBus.SendMessage(new Event(new EditedGroupEvent(id)));
            messageBus.SendMessage(new Event(@event2));
            messageBus.Notify();
            messageBus.SendMessage(new Event(@event));
            List<Event> eventsUser = user.GetNotifies();
            Assert.AreEqual(user.GetNotifies().Count, 1);
            Assert.AreEqual(user2.GetNotifies().Count, 0);
        }
    }
 }
