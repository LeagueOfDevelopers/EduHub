namespace EduHubLibrary.Data.UserDtos
{
    public class TagUser
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TagUser(int id, string tag)
        {
            Id = id;
            Name = tag;
        }

        internal TagUser()
        {
        }
    }
}
