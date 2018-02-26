using System;

namespace EduHub.Models.Tools
{
    public class ReviewModel
    {
        public ReviewModel(Guid fromUser, string title, string text, DateTimeOffset date, Guid fromGroup)
        {
            FromUser = fromUser;
            FromGroup = fromGroup;
            Title = title;
            Text = text;
            Date = date;
        }

        public Guid FromUser { get; }
        public Guid FromGroup { get; }
        public string Title { get; }
        public string Text { get; }
        public DateTimeOffset Date { get; }
    }
}