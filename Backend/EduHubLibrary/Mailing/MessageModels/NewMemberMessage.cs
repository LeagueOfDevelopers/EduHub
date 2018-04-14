using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Mailing.MessageModels
{
    public class NewMemberMessage
    {
        public NewMemberMessage(string groupTitle, string username)
        {
            GroupTitle = groupTitle;
            Username = username;
        }

        public string GroupTitle { get; }
        public string Username { get; }
    }
}
