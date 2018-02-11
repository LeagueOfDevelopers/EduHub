using Microsoft.AspNetCore.Http;

namespace EduHub.Models
{
    public class UploadFileRequest
    {
        public UploadFileRequest(IFormFile file)
        {
            File = file;
        }

        public IFormFile File { get; set; }
    }
}