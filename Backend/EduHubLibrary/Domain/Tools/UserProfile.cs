using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Tools
{
    public class UserProfile
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string AboutUser { get; set; }
        public string BirthYear { get; set; }
        public bool IsMan { get; set; }
        public bool IsTeacher { get; set; }
        public string AvatarLink { get; set; }
        public List<string> Contacts { get; set; }

        public UserProfile(string name, string email, bool isTeacher)
        {
            Email = email;
            Name = name;
            IsTeacher = isTeacher;
        }
    }
}
