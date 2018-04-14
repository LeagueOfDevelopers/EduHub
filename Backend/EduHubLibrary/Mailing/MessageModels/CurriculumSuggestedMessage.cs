using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Mailing.EmailModels
{
    public class CurriculumSuggestedMessage
    {
        public CurriculumSuggestedMessage(string curriculumLink, string groupTitle)
        {
            CurriculumLink = curriculumLink;
            GroupTitle = groupTitle;
        }

        public string CurriculumLink { get; }
        public string GroupTitle { get; }
    }
}
