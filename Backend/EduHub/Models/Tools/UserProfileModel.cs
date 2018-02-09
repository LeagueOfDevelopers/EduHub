using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models.Tools
{
    public class UserProfileModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string AboutUser { get; set; }
        public int BirthYear { get; set; }
        public bool IsMan { get;  set; }
        public bool IsTeacher { get; set; }
        public string AvatarLink { get; set; }
        public List<string> Contacts { get; set; }

        public UserProfileModel(string name, string email, string aboutUser, int birthYear, bool isMan, bool isTeacher, string avatarLink, List<string> contacts)
        {
            Name = name;
            Email = email;
            AboutUser = aboutUser;
            BirthYear = birthYear;
            IsMan = isMan;
            IsTeacher = isTeacher;
            AvatarLink = avatarLink;
            Contacts = contacts;
        }
    }
}
