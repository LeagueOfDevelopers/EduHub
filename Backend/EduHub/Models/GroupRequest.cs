﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models
{
    public class GroupRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
    }
}