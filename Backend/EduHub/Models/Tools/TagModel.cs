using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models.Tools
{
    public class TagModel
    {
        public TagModel(string tag)
        {
            Tag = tag;
        }

        public string Tag { get; set; }
    }
}
