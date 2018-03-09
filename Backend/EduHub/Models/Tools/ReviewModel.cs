using System;

namespace EduHub.Models.Tools
{
    public class ReviewModel
    {
        public ReviewModel(int fromUser, string title, string text, DateTimeOffset date, int fromGroup)
        {
            FromUser = fromUser;
            FromGroup = fromGroup;
            Title = title;
            Text = text;
            Date = date;
        }

        public int FromUser { get; }
        public int FromGroup { get; }
        public string Title { get; }
        public string Text { get; }
        public DateTimeOffset Date { get; }
    }
}