﻿using System.Collections.Generic;
using EnsureThat;

namespace EduHubLibrary.Domain.Tools
{
    public class TeacherProfile
    {
        public TeacherProfile()
        {
            Reviews = new List<Review>();
            Skills = new List<string>();
        }

        //constr for db
        internal TeacherProfile(List<Review> reviewList, List<string> skillsList)
        {
            Reviews = Ensure.Any.IsNotNull(reviewList);
            Skills = Ensure.Any.IsNotNull(skillsList);
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