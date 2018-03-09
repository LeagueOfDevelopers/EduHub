namespace EduHubLibrary.Data.UserDtos
{
    public class SkillsDto
    {
        public int Id { get; set; }
        public string Skill { get; set; }

        public SkillsDto(int id, string skill)
        {
            Id = id;
            Skill = skill;
        }
    }
}
