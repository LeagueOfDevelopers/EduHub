using System;
using EnsureThat;

namespace EduHubLibrary.Domain.Tools
{
    public class Review
    {
        public Review(Guid fromUser, string title, string text, Guid fromGroup)
        {
            FromUser = Ensure.Guid.IsNotEmpty(fromUser);
            Title = Ensure.String.IsNotNullOrWhiteSpace(title);
            Text = Ensure.String.IsNotNullOrWhiteSpace(text);
            Date = DateTimeOffset.Now;
            FromGroup = Ensure.Guid.IsNotEmpty(fromGroup);
        }

        public Guid FromUser { get; }
        public Guid FromGroup { get; }
        public string Title { get; }
        public string Text { get; }
        public DateTimeOffset Date { get; }
    }
}