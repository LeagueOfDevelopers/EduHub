using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService
{
    public class Event
    {
        public Event(MessageType type, string description)
        {
            Id = Guid.NewGuid();
            Type = type;
            Description = description;
            OccurredOn = DateTime.Now;
            IsRead = false;
        }

        public Guid Id { get; private set; }
        public MessageType Type { get; private set; }
        public string Description { get; private set; }
        public DateTime OccurredOn { get; private set; }
        public bool IsRead { get; private set; }

        public void ReadEvent()
        {
            IsRead = true;
        }
    }
}
