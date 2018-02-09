using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models.Tools
{
    public class TeacherProfileModel
    {
        public List<ReviewModel> Reviews { get;  set; }
        public List<string> Skills { get; set; }

        public TeacherProfileModel(List<ReviewModel> reviews, List<string> skills)
        {
            Reviews = reviews;
            Skills = skills;
        }
    }
}
