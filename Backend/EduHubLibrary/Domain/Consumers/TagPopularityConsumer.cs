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
        public TagPopularityConsumer(TagsManager tagManager)
        {
            _tagManager = tagManager;
        }

        public void Consume(UsingTagEvent @event)
        {
            if (_tagManager.DoesExist(@event.Tag))
            {
                _tagManager.AddPopularity(@event.Tag);
            }
            else
            {
                _tagManager.AddTag(@event.Tag);
            }
        }

        private TagsManager _tagManager;        
    }
}
