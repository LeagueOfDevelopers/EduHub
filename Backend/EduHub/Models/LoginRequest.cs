using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models
{
    public class LoginRequest
    {
        /// <summary>User's email</summary>
        public string Email { get; set; }
        /// <summary>User's password</summary>
        public string Password { get; set; }
    }
}
