using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Mailing.EmailModels
{
    public class CurriculumDeclinedMessage
    {
        public CurriculumDeclinedMessage(string groupTitle, string declinedName)
        {
            GroupTitle = groupTitle;
            DeclinedName = declinedName;
        }

        public string GroupTitle { get; }
        public string DeclinedName { get; }
    }
}
