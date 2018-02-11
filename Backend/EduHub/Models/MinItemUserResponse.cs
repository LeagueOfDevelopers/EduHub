using System;
using EduHubLibrary.Domain;

namespace EduHub.Models
{
    public class MinItemUserResponse
    {
        public MinItemUserResponse(Guid id, string name, string email, bool isTeacher, bool isActive, string avatarLink)
        {
            Id = id;
            Name = name;
            Email = email;
            IsTeacher = isTeacher;
            IsActive = isActive;
            AvatarLink = avatarLink;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string AvatarLink { get; set; }
        public bool IsTeacher { get; set; }
        public TeacherProfile TeacherProfile { get; set; }
        public bool IsActive { get; set; }
    }
}