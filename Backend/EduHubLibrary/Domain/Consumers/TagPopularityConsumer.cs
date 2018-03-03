using EduHubLibrary.Domain.Events;
using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EduHubLibrary.Domain.Consumers
{
    public class TagPopularityConsumer : IEventConsumer<UsingTagEvent>
    {
        public TagPopularityConsumer(ITagFacade tagFacade)
        {
            _tagFacade = tagFacade;
        }

        public void Consume(UsingTagEvent @event)
        {
            _tagFacade.UseTag(@event.Tag);
        }

        private ITagFacade _tagFacade;        
    }
}
