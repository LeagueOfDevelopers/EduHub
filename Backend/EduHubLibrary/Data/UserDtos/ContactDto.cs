namespace EduHubLibrary.Data.UserDtos
{
    public class ContactDto
    {
        public int Id { get; set; }
        public string Contact { get; set; }

        public ContactDto(int id, string contact)
        {
            Id = id;
            Contact = contact;
        }
    }
}
