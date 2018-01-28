using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models
{
    public class UserResponse
    {
        public UserResponse(string name, string email, UserType type, bool isTeacher, TeacherProfile teacherProfile, bool isActive)
        {
            Name = name;
            Email = email;
            Type = type;
            IsTeacher = isTeacher;
            TeacherProfile = teacherProfile;
            IsActive = isActive;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public UserType Type { get; set; }
        public bool IsTeacher { get; set; }
        public TeacherProfile TeacherProfile { get; set; }
        public bool IsActive { get; set; }
    }
}
