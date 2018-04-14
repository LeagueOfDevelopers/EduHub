using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Mailing.MessageModels
{
    public class InvitationReceivedMessage
    {
        public InvitationReceivedMessage(string groupTitle, string inviterName, MemberRole suggestedRole)
        {
            GroupTitle = groupTitle;
            InviterName = inviterName;
            SuggestedRole = suggestedRole;
        }

        public string GroupTitle { get; }
        public string InviterName { get; }
        public MemberRole SuggestedRole { get; }
    }
}
