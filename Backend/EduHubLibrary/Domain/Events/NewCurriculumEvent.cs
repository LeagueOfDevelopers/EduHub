using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Events
{
    public class NewCurriculumEvent : EventInfoBase
    {
        public NewCurriculumEvent(Guid groupId, string curriculum)
        {
            GroupId = groupId;
            Curriculum = curriculum;
        }

        public Guid GroupId { get; private set; }
        public string Curriculum { get; private set; }
    }
}
