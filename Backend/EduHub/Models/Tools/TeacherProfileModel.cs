using System.Collections.Generic;

namespace EduHub.Models.Tools
{
    public class TeacherProfileModel
    {
        public TeacherProfileModel(IEnumerable<ReviewModel> reviews, IEnumerable<string> skills)
        {
            Reviews = reviews;
            Skills = skills;
        }

        public IEnumerable<ReviewModel> Reviews { get; set; }
        public IEnumerable<string> Skills { get; set; }
    }
}