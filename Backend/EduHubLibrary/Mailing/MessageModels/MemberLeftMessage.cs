using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Mailing.MessageModels
{
    public class MemberLeftMessage
    {
        public MemberLeftMessage(string groupTitle, string userName, string receiverName)
        {
            GroupTitle = groupTitle;
            UserName = userName;
            ReceiverName = receiverName;
        }

        public string GroupTitle { get; }
        public string UserName { get; }
        public string ReceiverName { get; }
    }
}
