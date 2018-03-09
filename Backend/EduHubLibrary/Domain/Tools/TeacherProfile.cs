using System;
using System.Collections.Generic;

namespace EduHubLibrary.Domain.Tools
{
    public class TeacherProfile
    {
        public TeacherProfile()
        {
            Reviews = new List<Review>();
            Skills = new List<string>();
        }

        public List<Review> Reviews { get; set; }
        public List<string> Skills { get; set; }

        public void AddReview(int fromUser, string title, string text, int fromGroup)
        {
            var newReview = new Review(fromUser, title, text, fromGroup);
            Reviews.Add(newReview);
        }
    }
}