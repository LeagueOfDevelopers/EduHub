using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models
{
    public class MinItemUserResponse
    {
        public MinItemUserResponse(Guid id, string name, string email, bool isTeacher, TeacherProfile teacherProfile, bool isActive)
        {
            Id = id;
            Name = name;
            Email = email;
            IsTeacher = isTeacher;
            TeacherProfile = teacherProfile;
            IsActive = isActive;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsTeacher { get; set; }
        public TeacherProfile TeacherProfile { get; set; }
        public bool IsActive { get; set; }
    }
}
