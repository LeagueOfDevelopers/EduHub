using System.Linq;
using EduHubLibrary.Domain.Events;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Facades;

namespace EduHubLibrary.Domain.Consumers
{
    public class GroupEventsConsumer : IEventConsumer<NewMemberEvent>, IEventConsumer<NewCurriculumEvent>
    {
        private readonly IGroupFacade _groupFacade;

        private readonly IUserFacade _userFacade;

        public GroupEventsConsumer(IUserFacade userFacade, IGroupFacade groupFacade)
        {
            _userFacade = userFacade;
            _groupFacade = groupFacade;
        }

        public void Consume(NewCurriculumEvent @event)
        {
            _groupFacade.GetGroupMembers(@event.GroupId).ToList().ForEach(
                m => _userFacade.AddNotify(m.UserId,
                    $"В группе {@event.GroupId} предложен учебный план {@event.Curriculum}"));
        }

        public void Consume(NewMemberEvent @event)
        {
            _groupFacade.GetGroupMembers(@event.GroupId).ToList().ForEach(
                m => _userFacade.AddNotify(m.UserId, $"В группе {@event.GroupId} новый участник {@event.NewMemberId}"));
        }
    }
}