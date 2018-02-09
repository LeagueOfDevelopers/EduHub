using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models
{
    public class AddFileResponse
    {
        public string Filename { get; set; }

        public AddFileResponse(string filename)
        {
            Filename = filename;
        }
    }
}
