using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain
{
    public class TeacherProfile
    {
        public List<Review> Reviews;
        public List<string> Skills;

        public TeacherProfile()
        {
            Reviews = new List<Review>();
            Skills = new List<string>();
        }

        public void ConfigureSkills(List<string> skills)
        {
            Skills = skills;
        }

        public void AddReview(Review review)
        {
            Reviews.Add(review);
        }
    }
}
