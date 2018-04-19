using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Mailing.MessageModels
{
    public class AdminInvitationMessage
    {
        public AdminInvitationMessage(int inviteCode)
        {
            InviteCode = inviteCode;
        }

        public int InviteCode { get; }
    }
}
