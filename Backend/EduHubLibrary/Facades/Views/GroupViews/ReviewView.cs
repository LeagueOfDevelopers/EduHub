using System;

namespace EduHubLibrary.Facades.Views.GroupViews
{
    public class ReviewView
    {
        public int FromUser { get; }
        public int FromGroup { get; }
        public string Title { get; }
        public string Text { get; }
        public DateTimeOffset Date { get; }

        public ReviewView(int fromUser, int fromGroup,
            string title, string text, DateTimeOffset date)
        {
            FromUser = fromUser;
            FromGroup = fromGroup;
            Title = title;
            Text = text;
            Date = date;
        }
    }
}
