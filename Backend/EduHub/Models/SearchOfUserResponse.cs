﻿using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models
{
    public class SearchOfUserResponse
    {
        public SearchOfUserResponse(IEnumerable<User> result)
        {
            /*
            Name = name;
            Email = email;
            IsTeacher = isTeacher;
            IsActive = isActive;
            */
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsTeacher { get; set; }
        public bool IsActive { get; set; }
    }
}