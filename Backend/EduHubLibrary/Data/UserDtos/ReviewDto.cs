using System;

namespace EduHubLibrary.Data.UserDtos
{
    public class ReviewDto
    {
        public ReviewDto(int id, int fromUser, int fromGroup, string title, string text, DateTimeOffset date)
        {
            Id = id;
            FromUser = fromUser;
            FromGroup = fromGroup;
            Title = title;
            Text = text;
            Date = date;
        }

        public ReviewDto()
        {
        }

        public int Id { get; set; }
        public int FromUser { get; set; }
        public int FromGroup { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}