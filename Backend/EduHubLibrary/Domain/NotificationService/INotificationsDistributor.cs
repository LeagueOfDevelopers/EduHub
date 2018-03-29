﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService
{
    public interface INotificationsDistributor
    {
        void NotifyGroup(int groupId, IEventInfo eventInfo);
        void NotifyPerson(int userId, IEventInfo eventInfo);
        void NotifyAdmins(IEventInfo eventInfo);
    }
}
