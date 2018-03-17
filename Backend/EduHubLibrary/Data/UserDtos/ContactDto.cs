using System.ComponentModel.DataAnnotations;

namespace EduHubLibrary.Data.UserDtos
{
    public class ContactDto
    {
        [Key]
        [StringLength(250)]
        public string Contact { get; set; }

        public ContactDto(string contact)
        {
            Contact = contact;
        }

        public ContactDto()
        {
        }
    }
}
