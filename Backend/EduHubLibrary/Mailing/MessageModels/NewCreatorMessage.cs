using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Mailing.MessageModels
{
    public class NewCreatorMessage
    {
        public NewCreatorMessage(string groupTitle, string exCreatorUsername, string newCreatorUsername)
        {
            GroupTitle = groupTitle;
            ExCreatorUsername = exCreatorUsername;
            NewCreatorUsername = newCreatorUsername;
        }

        public string GroupTitle { get; }
        public string ExCreatorUsername { get; }
        public string NewCreatorUsername { get; }
    }
}
