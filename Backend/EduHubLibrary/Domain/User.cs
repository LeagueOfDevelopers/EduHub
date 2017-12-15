using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EduHubLibrary.Domain
{
    public class User
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public bool IsTeacher { get; private set; }
        public bool IsActive { get; private set; }
        public Guid Id { get; private set; }

        public User(string name, string email, bool isTeacher)
        {
            Name = name;
            Email = email;
            IsTeacher = isTeacher;
            IsActive = true;
            Id = Guid.NewGuid();
        }

        public void EditProfile(string newName, string email, bool isTeacher)
        {
            Name = newName;
            Email = email;
            IsTeacher = isTeacher;
        }

        public void RestoreProfile()
        {
            IsActive = true;
        }

        public void DeleteProfile()
        {
            IsActive = false;
        }
    }
}
