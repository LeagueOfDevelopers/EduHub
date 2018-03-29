namespace EduHubLibrary.Data.UserDtos
{
    public class ContactDto
    {
        public ContactDto(int id, string contact)
        {
            Id = id;
            Contact = contact;
        }

        public ContactDto()
        {
        }

        public int Id { get; set; }
        public string Contact { get; set; }
    }
}