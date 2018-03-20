using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduHubLibrary.Data.UserDtos
{
    public class SkillsDto
    {
        [Key]
        [StringLength(250)]
        public string Skill { get; set; }

        public SkillsDto(string skill)
        {
            Skill = skill;
        }

        public SkillsDto()
        {
        }
    }
}
