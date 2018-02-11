using System.Collections.Generic;

namespace EduHub.Models.Tools
{
    public class TeacherProfileModel
    {
        public TeacherProfileModel(List<ReviewModel> reviews, List<string> skills)
        {
            Reviews = reviews;
            Skills = skills;
        }

        public List<ReviewModel> Reviews { get; set; }
        public List<string> Skills { get; set; }
    }
}