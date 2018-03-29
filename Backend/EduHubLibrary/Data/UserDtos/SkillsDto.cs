using System.ComponentModel.DataAnnotations;

namespace EduHubLibrary.Data.UserDtos
{
    public class SkillsDto
    {
        public SkillsDto(string skill)
        {
            Skill = skill;
        }

        public SkillsDto()
        {
        }

        [Key] [StringLength(250)] public string Skill { get; set; }
    }
}