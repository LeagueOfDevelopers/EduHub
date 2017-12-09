using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models
{
    public class SearchOfGroupRequest
    {
        public string Name { get; set; }
        public int MaxCount { get; set; }
    }
}
