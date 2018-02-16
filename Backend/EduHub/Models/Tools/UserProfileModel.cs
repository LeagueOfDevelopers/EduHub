﻿using System.Collections.Generic;
using EduHubLibrary.Domain.Tools;

namespace EduHub.Models.Tools
{
    public class UserProfileModel
    {
        public UserProfileModel(string name, string email, string aboutUser, int birthYear, Gender gender,
            bool isTeacher,
            string avatarLink, List<string> contacts)
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

        public string Name { get; set; }
        public string Email { get; set; }
        public string AboutUser { get; set; }
        public int BirthYear { get; set; }
        public Gender Gender { get; set; }
        public bool IsTeacher { get; set; }
        public string AvatarLink { get; set; }
        public List<string> Contacts { get; set; }
    }
}