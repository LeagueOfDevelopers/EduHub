using EnsureThat;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain
{
    public class Review
    {
        public Review(Guid evaluator, string text, int rating)
        {
            Evaluator = Ensure.Guid.IsNotEmpty(evaluator);
            Text = text;
            Rating = rating;
        }

        public Guid Evaluator { get; }
        public string Text { get; private set; }
        public int Rating { get; private set; }
    }
}
