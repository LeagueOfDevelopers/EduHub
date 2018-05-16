using System;
using System.Collections.Generic;
using EnsureThat;

namespace EduHubLibrary.Domain.Tools
{
    public class UserProfile
    {
        public UserProfile(string name, string email, bool isTeacher, string avatarLink)
        {
            Email = email;
            Name = name;
            IsTeacher = isTeacher;
            AvatarLink = Ensure.Any.IsNotNull(avatarLink);
            Contacts = new List<string>();
        }

        public UserProfile(string name, string email, string aboutUser,
            DateTimeOffset birthYear, Gender gender, bool isTeacher, string avatarLink, List<string> contacts)
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
        public DateTimeOffset BirthYear { get; internal set; }
        public Gender Gender { get; internal set; }
        public bool IsTeacher { get; internal set; }
        public string AvatarLink { get; internal set; }
        public List<string> Contacts { get; internal set; }
    }
}