﻿using System.Collections.Generic;
using EduHubLibrary.Domain.Tools;
using EduHubLibrary.Domain;

namespace EduHub.Models.Tools
{
    public class UserProfileModel
    {
        public UserProfileModel(string name, string aboutUser, int birthYear, Gender gender,
            bool isTeacher, string avatarLink, List<string> contacts, List<Sanction> sanctions)
        {
            Name = name;
            AboutUser = aboutUser;
            BirthYear = birthYear;
            Gender = gender;
            IsTeacher = isTeacher;
            AvatarLink = avatarLink;
            Contacts = contacts;
            Sanctions = sanctions;
        }

        public string Name { get; set; }
        public string AboutUser { get; set; }
        public int BirthYear { get; set; }
        public Gender Gender { get; set; }
        public bool IsTeacher { get; set; }
        public string AvatarLink { get; set; }
        public List<string> Contacts { get; set; }
        public List<Sanction> Sanctions { get; set; }
    }
}