using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models
{
    public class CreateGroupRequest
    {
        public Guid IdOfCreator { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }
        public int Size { get; set; }
        public double totalValue { get; set; }
    }
}
