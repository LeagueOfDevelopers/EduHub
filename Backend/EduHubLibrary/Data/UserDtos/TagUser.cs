namespace EduHubLibrary.Data.UserDtos
{
    public class TagUser
    {
        public TagUser(int id, string tag)
        {
            Id = id;
            Name = tag;
        }

        internal TagUser()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}