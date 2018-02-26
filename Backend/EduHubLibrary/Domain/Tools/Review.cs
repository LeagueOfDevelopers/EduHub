using System;
using EnsureThat;

namespace EduHubLibrary.Domain
{
    public class Review
    {
        public Review(Guid fromUser, string title, string text)
        {
            FromUser = Ensure.Guid.IsNotEmpty(fromUser);
            Title = Ensure.String.IsNotNullOrWhiteSpace(title);
            Text = Ensure.String.IsNotNullOrWhiteSpace(text);
            Date = DateTimeOffset.Now;
        }

        public Guid FromUser { get; }
        public string Title { get; }
        public string Text { get; }
        public DateTimeOffset Date { get; }
    }
}