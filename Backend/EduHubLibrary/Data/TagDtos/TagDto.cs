using System.ComponentModel.DataAnnotations;

namespace EduHubLibrary.Data.TagDtos
{
    public class TagDto
    {
        public TagDto(string name)
        {
            Name = name;
            Popularity = 1;
        }

        internal TagDto()
        {
        }

        [Key] [MaxLength(250)] public string Name { get; set; }

        public int Popularity { get; set; }

        //public ICollection<UserTag> UserTags { get; } = new List<UserTag>();
        //public ICollection<GroupTag> GroupTags { get; } = new List<GroupTag>();
    }
}