using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Mailing.MessageModels
{
    public class NewCreatorMessage
    {
        public NewCreatorMessage(string groupTitle, string exCreatorUsername, string newCreatorUsername, string receiverName)
        {
            GroupTitle = groupTitle;
            ExCreatorUsername = exCreatorUsername;
            NewCreatorUsername = newCreatorUsername;
            ReceiverName = receiverName;
        }

        public string GroupTitle { get; }
        public string ExCreatorUsername { get; }
        public string NewCreatorUsername { get; }
        public string ReceiverName { get; }
    }
}
