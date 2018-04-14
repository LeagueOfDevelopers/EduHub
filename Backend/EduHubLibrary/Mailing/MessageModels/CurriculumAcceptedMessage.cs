using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Mailing.EmailModels
{
    public class CurriculumAcceptedMessage
    {
        public CurriculumAcceptedMessage(string groupTitle)
        {
            GroupTitle = groupTitle;
        }

        public string GroupTitle { get; }
    }
}
