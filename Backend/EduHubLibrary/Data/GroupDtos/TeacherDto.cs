namespace EduHubLibrary.Data.GroupDtos
{
    public class TeacherDto
    {
        public TeacherDto(int id)
        {
            Id = id;
        }

        public TeacherDto()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}