using System;

namespace EduHub.Models.Tools
{
    public class ReviewModel
    {
        public ReviewModel(Guid fromUser, string title, string text, DateTimeOffset date)
        {
            FromUser = fromUser;
            Title = title;
            Text = text;
            Date = date;
        }

        public Guid FromUser { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}