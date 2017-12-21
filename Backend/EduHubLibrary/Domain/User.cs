using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnsureThat;

namespace EduHubLibrary.Domain
{
    public class User
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public bool IsTeacher { get; private set; }
        public bool IsActive { get; private set; }
        public Guid Id { get; private set; }

        public User(string name, string email, string password, bool isTeacher)
        {
            Name = Ensure.String.IsNotNullOrWhiteSpace(name);
            Email = Ensure.String.IsNotNullOrWhiteSpace(email);
            Password = Ensure.String.IsNotNullOrWhiteSpace(password);
            IsTeacher = isTeacher;
            IsActive = true;
            Id = Guid.NewGuid();
        }

        public void EditName(string newName)
        {
            Name = Ensure.String.IsNotNullOrWhiteSpace(newName);
        }

        public void BecomeTeacher()
        {
            IsTeacher = true;
        }

        public void StopToBeTeacher()
        {
            IsTeacher = false;
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
