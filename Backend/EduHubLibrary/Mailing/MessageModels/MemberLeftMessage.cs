using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Mailing.MessageModels
{
    public class MemberLeftMessage
    {
        public MemberLeftMessage(string groupTitle, string username, string receiverName)
        {
            GroupTitle = groupTitle;
            Username = username;
            ReceiverName = receiverName;
        }

        public string GroupTitle { get; }
        public string Username { get; }
        public string ReceiverName { get; }
    }
}
