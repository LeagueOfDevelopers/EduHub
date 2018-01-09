using EduHubLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;   

namespace EduHub.Models
{
    public class LoginResponse
    {
        public LoginResponse(string name, string email, string avatarLink, string token)
        {
            Name = name;
            Email = email;
            AvatarLink = avatarLink;
            Token = token;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string AvatarLink { get; set; }
        public string Token { get; set; }
    }
}
