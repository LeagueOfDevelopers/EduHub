using System;

namespace EduHubLibrary.Data.UserDtos
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int FromUser { get; }
        public int FromGroup { get; }
        public string Title { get; }
        public string Text { get; }
        public DateTimeOffset Date { get; }

        public ReviewDto(int id, int fromUser, int fromGroup, string title, string text, DateTimeOffset date)
        {
            Id = id;
            FromUser = fromUser;
            FromGroup = fromGroup;
            Title = title;
            Text = text;
            Date = date;
        }
    }
}
