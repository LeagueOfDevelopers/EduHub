using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduHubLibrary.Data.UserDtos
{
    public class NotifiesDto
    {
        [Key]
        [StringLength(250)]
        public string Notifie { get; set; }

        public NotifiesDto(string notifie)
        {
            Notifie = notifie;
        }

        public NotifiesDto()
        {
        }
    }
}
