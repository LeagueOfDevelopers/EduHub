using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Mailing.MessageModels
{
    public class InvitationAcceptedMessage
    {
        public InvitationAcceptedMessage(string groupTitle, string invitedName)
        {
            GroupTitle = groupTitle;
            InvitedName = invitedName;
        }

        public string GroupTitle { get; }
        public string InvitedName { get; }
    }
}
