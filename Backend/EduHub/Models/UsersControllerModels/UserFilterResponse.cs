using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduHubLibrary.Domain.Tools;

namespace EduHub.Models.UsersControllerModels
{
    public class UserFilterResponse
    {
        public UserFilterResponse(int id, string name, string email,
            string avatarLink, bool isTeacher, TeacherProfile teacherProfile,
            bool isActive, Gender gender, int birthYear)
        {
            Id = id;
            Name = name;
            Email = email;
            AvatarLink = avatarLink;
            Gender = gender;
            BirthYear = birthYear;
            IsTeacher = isTeacher;
            TeacherProfile = teacherProfile;
            IsActive = isActive;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string AvatarLink { get; set; }
        public Gender Gender { get; set; }
        public int BirthYear { get; set; }
        public bool IsTeacher { get; set; }
        public TeacherProfile TeacherProfile { get; set; }
        public bool IsActive { get; set; }

    }
}
