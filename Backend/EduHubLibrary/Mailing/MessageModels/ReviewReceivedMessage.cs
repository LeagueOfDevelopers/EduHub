using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Mailing.MessageModels
{
    public class ReviewReceivedMessage
    {
        public ReviewReceivedMessage(string groupTitle, string reviewerName, string reviewType, string receiverName)
        {
            GroupTitle = groupTitle;
            ReviewerName = reviewerName;
            ReviewType = reviewType;
            ReceiverName = receiverName;
        }

        public string GroupTitle { get; }
        public string ReviewerName { get; }
        public string ReviewType { get; }
        public string ReceiverName { get; }
    }
}
