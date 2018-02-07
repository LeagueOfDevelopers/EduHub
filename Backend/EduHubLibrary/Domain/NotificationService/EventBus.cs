using System;
using EasyNetQ;
using EasyNetQ.Topology;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EduHubLibrary.Domain.Consumers;
using EduHubLibrary.Facades;
using System.Collections;
using EduHubLibrary.Domain.Events;
using EduHubLibrary.Infrastructure;

namespace EduHubLibrary.Domain.NotificationService
{
    public class EventBus
    {
        public EventBus()
        {
            _events = new InMemoryEventRepository();
        }

        public void RegisterConsumers(GroupFacade groupFacade, UserFacade userFacade)
        {
            _groupEventsConsumer = new GroupEventsConsumer(userFacade, groupFacade);
            _invitationConsumer = new InvitationConsumer(groupFacade);
        }

        public void PublishEvent(InvitationEvent invitationEvent)
        {
            _invitationConsumer.Consume(invitationEvent);
            _events.AddEvent(new Event(invitationEvent));
        }

        public void PublishEvent(NewCurriculumEvent newCurriculumEvent)
        {
            _groupEventsConsumer.Consume(newCurriculumEvent);
            _events.AddEvent(new Event(newCurriculumEvent));
        }

        public void PublishEvent(NewMemberEvent newMemberEvent)
        {
            _groupEventsConsumer.Consume(newMemberEvent);
            _events.AddEvent(new Event(newMemberEvent));
        }

        private GroupEventsConsumer _groupEventsConsumer;
        private InvitationConsumer _invitationConsumer;
        private InMemoryEventRepository _events;
    }
}