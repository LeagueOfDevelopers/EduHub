using System;
using EnsureThat;

namespace EduHubLibrary.Domain.Tools
{
    public class Review
    {
        public Review(int fromUser, string title, string text, int fromGroup)
        {
            FromUser = fromUser;
            Title = Ensure.String.IsNotNullOrWhiteSpace(title);
            Text = Ensure.String.IsNotNullOrWhiteSpace(text);
            Date = DateTimeOffset.Now;
            FromGroup = fromGroup;
        }

        public int FromUser { get; }
        public int FromGroup { get; }
        public string Title { get; }
        public string Text { get; }
        public DateTimeOffset Date { get; }
    }
}