﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain
{
    public class TeacherProfile
    {
        public TeacherProfile()
        {
            Reviews = new List<Review>();
            Skills = new List<string>();
        }

        public List<Review> Reviews { get; private set; }
        public List<string> Skills { get; set; }

        public void AddReview(Review review)
        {
            Reviews.Add(review);
        }
    }
}
