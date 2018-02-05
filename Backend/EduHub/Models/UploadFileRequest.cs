using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models
{
    public class UploadFileRequest
    {
        public IFormFile File { get; set; }

        public UploadFileRequest(IFormFile file)
        {
            File = file;
        }
    }
}
