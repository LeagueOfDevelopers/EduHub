using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models
{
    public class UserResponse
    {
        public UserResponse(string name, string email, TypeOfUser typeOfUser, bool isTeacher, TeacherProfile teacherProfile, bool isActive)
        {
            Name = name;
            Email = email;
            TypeOfUser = typeOfUser;
            IsTeacher = isTeacher;
            TeacherProfile = teacherProfile;
            IsActive = isActive;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public TypeOfUser TypeOfUser { get; set; }
        public bool IsTeacher { get; set; }
        public TeacherProfile TeacherProfile { get; set; }
        public bool IsActive { get; set; }
    }
}
