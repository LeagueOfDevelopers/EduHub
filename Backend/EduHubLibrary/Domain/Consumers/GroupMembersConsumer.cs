using EduHubLibrary.Domain.Events;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Facades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EduHubLibrary.Domain.Consumers
{
    public class GroupMembersConsumer : IEventConsumer<NewMemberEvent>
    {
        public GroupMembersConsumer(IUserFacade userFacade, IGroupFacade groupFacade)
        {
            _userFacade = userFacade;
            _groupFacade = groupFacade;
        }

        public void Consume(NewMemberEvent @event)
        {
            _groupFacade.GetGroupMembers(@event.GroupId).ToList().ForEach(
                m => _userFacade.AddNotify(m.UserId, $"В группе {@event.GroupId} новый участник {@event.NewMemberId}"));
        }

        private IUserFacade _userFacade;
        private IGroupFacade _groupFacade;
    }
}
