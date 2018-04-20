using EduHubLibrary.Domain.Events;
using EduHubLibrary.Domain.NotificationService;

namespace EduHubLibrary.Domain.Consumers
{
    public class TagPopularityConsumer : IEventConsumer<UsingTagEvent>
    {
        private readonly ITagFacade _tagFacade;

        public TagPopularityConsumer(ITagFacade tagFacade)
        {
            _tagFacade = tagFacade;
        }

        public void Consume(UsingTagEvent @event)
        {
            _tagFacade.UseTag(@event.Tag);
        }
    }
}