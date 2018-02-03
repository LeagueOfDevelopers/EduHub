using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Events
{
    public class EditedGroupEvent : IEventInfo
    {
        public EditedGroupEvent(Guid groupId)
        {
            GroupId = groupId;
        }

        public string GetDescription()
        {
            return $"В группе с id {GroupId} произошло редактирование";
        }

        public string GetEventType()
        {
            return GetType().Name;
        }
        
        public override bool Equals(object obj)
        {
            EditedGroupEvent newObj = (EditedGroupEvent)obj;

            if (obj.GetType().Equals(GetType()) && newObj.GroupId.Equals(GroupId)) return true;
            else return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public Guid GroupId { get; set; }
    }
}
