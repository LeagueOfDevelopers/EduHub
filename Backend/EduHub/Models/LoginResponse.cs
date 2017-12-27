using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models
{
    public class LoginResponse
    {
        public LoginResponse(string name, UserType type, bool isTeacher, string token)
        {
            Name = name;
            IsTeacher = isTeacher;
            Token = token;
            
            if (type == UserType.Admin)
            {
                Type = "Admin";
            }
            else
            {
                Type = "User";
            }
        }

        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsTeacher { get; set; }
        public string Token { get; set; }
    }

    public enum UserType { Admin, User };
}
