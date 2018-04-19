using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Mailing.MessageModels
{
    public class CurriculumDeclinedMessage
    {
        public CurriculumDeclinedMessage(string groupTitle, string declinedName, string receiverName)
        {
            GroupTitle = groupTitle;
            DeclinedName = declinedName;
            ReceiverName = receiverName;
        }

        public string GroupTitle { get; }
        public string DeclinedName { get; }
        public string ReceiverName { get; }
    }
}
