using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Events
{
    public class UsingTagEvent : EventInfoBase
    {
        public UsingTagEvent(string tag)
        {
            Tag = tag;
        }

        public string Tag { get; }
    }
}
