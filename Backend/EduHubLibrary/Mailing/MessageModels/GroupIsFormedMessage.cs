using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Mailing.MessageModels
{
    public class GroupIsFormedMessage
    {
        public GroupIsFormedMessage(string groupTitle)
        {
            GroupTitle = groupTitle;
        }

        public string GroupTitle { get; }
    }
}
