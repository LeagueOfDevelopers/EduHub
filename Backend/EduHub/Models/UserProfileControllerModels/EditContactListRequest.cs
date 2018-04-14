using System.Collections.Generic;
using EduHub.Models.ValidationAttributes;

namespace EduHub.Models
{
    public class EditContactListRequest
    {
        [ListLength(0, 5, ErrorMessage = "contacts count must be <= 5")]
        [ContactCheck]
        public List<string> Contacts { get; set; }
    }
}