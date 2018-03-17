using System.Collections.Generic;

namespace EduHubLibrary.Domain.Tools
{
    public class UserProfile
    {
        public UserProfile(string name, string email, bool isTeacher)
        {
            Email = email;
            Name = name;
            IsTeacher = isTeacher;
            Contacts = new List<string>();
        }

        public UserProfile(string name, string email, string aboutUser,
            int birthYear, Gender gender, bool isTeacher, string avatarLink, List<string> contacts)
        {
            Name = name;
            Email = email;
            AboutUser = aboutUser;
            BirthYear = birthYear;
            Gender = gender;
            IsTeacher = isTeacher;
            AvatarLink = avatarLink;
            Contacts = contacts;
        }

        public string Name { get; internal set; }
        public string Email { get; internal set; }
        public string AboutUser { get; internal set; }
        public int BirthYear { get; internal set; }
        public Gender Gender { get; internal set; }
        public bool IsTeacher { get; internal set; }
        public string AvatarLink { get; internal set; }
        public List<string> Contacts { get; internal set; }
    }
}