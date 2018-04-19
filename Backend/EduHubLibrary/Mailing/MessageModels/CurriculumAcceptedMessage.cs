using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Mailing.MessageModels
{
    public class CurriculumAcceptedMessage
    {
        public CurriculumAcceptedMessage(string groupTitle, string receiverName)
        {
            GroupTitle = groupTitle;
            ReceiverName = receiverName;
        }

        public string GroupTitle { get; }
        public string ReceiverName { get; }
    }
}
