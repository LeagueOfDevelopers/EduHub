using EduHubLibrary.Domain.Tools;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EduHub.Models.ValidationAttributes;

namespace EduHub.Models.UserProfileControllerModels
{
    public class EditProfileRequest
    {
        [StringLength(70, MinimumLength = 3)]
        [RegularExpression(@"[a-zA-Zа-яА-Я\s]+")]
        public string Name { get; set; }
        [StringLength(3000)]
        public string AboutUser { get; set; }
        public Gender Gender { get; set; }
        public string AvatarLink { get; set; }
        [ListLength(0, 5, ErrorMessage = "contacts count must be <= 5")]
        [ContactCheck]
        public List<string> Contacts { get; set; }
        [BirthCheck(1900)]
        public int BirthYear { get; set; }
    }
}
