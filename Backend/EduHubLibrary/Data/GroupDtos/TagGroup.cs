namespace EduHubLibrary.Data.GroupDtos
{
    public class TagGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TagGroup(int id, string tag)
        {
            Id = id;
            Name = tag;
        }

        internal TagGroup()
        {
        }
    }
}
