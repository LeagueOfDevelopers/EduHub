using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Mailing.MessageModels
{
    public class InvitationAcceptedMessage
    {
        public InvitationAcceptedMessage(string groupTitle, string invitedName, string receiverName)
        {
            GroupTitle = groupTitle;
            InvitedName = invitedName;
            ReceiverName = receiverName;
        }

        public string GroupTitle { get; }
        public string InvitedName { get; }
        public string ReceiverName { get; }
    }
}
