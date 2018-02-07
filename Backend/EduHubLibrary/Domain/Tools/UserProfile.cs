﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Tools
{
    public class UserProfile
    {
        public string Name { get; internal set; }
        public string Email { get; internal set; }
        public string AboutUser { get; internal set; }
        public string BirthYear { get; internal set; }
        public bool IsMan { get; internal set; }
        public bool IsTeacher { get; internal set; }
        public string AvatarLink { get; internal set; }
        public List<string> Contacts { get; internal set; }

        public UserProfile(string name, string email, bool isTeacher)
        {
            Email = email;
            Name = name;
            IsTeacher = isTeacher;
        }
    }
}
