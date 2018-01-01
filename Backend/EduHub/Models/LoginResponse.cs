using EduHubLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models
{
    public class LoginResponse
    {
        public LoginResponse(string name, Role typeOfRole, bool isTeacher, string token)
        {
            Name = name;
            IsTeacher = isTeacher;
            Token = token;

            if (typeOfRole == Role.Admin)
            {
                TypeOfRole = "Admin";
            }
            else
            {
                TypeOfRole = "User";
            }
        }

        public string Name { get; set; }
        public string TypeOfRole { get; set; }
        public bool IsTeacher { get; set; }
        public string Token { get; set; }
    }
}
