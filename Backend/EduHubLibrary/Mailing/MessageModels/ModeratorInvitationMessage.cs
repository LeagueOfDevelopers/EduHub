using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Mailing.EmailModels
{
    public class ModeratorInvitationMessage
    {
        public ModeratorInvitationMessage(int inviteCode)
        {
            InviteCode = inviteCode;
        }

        public int InviteCode { get; }
    }
}
