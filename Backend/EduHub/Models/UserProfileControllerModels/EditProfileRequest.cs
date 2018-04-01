using EduHubLibrary.Domain.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models.UserProfileControllerModels
{
    public class EditProfileRequest
    {
        public EditProfileRequest(string name, string aboutUser, Gender gender, string avatarLink, List<string> contacts, int birthYear)
        {
            Name = name;
            AboutUser = aboutUser;
            Gender = gender;
            AvatarLink = avatarLink;
            Contacts = contacts;
            BirthYear = birthYear;
        }

        public string Name { get; set; }
        public string AboutUser { get; set; }
        public Gender Gender { get; set; }
        public string AvatarLink { get; set; }
        public List<string> Contacts { get; set; }
        public int BirthYear { get; set; }
    }
}
