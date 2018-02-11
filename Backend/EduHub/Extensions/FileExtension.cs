using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace EduHub.Extensions
{
    public static class FileExtension
    {
        public static bool IsSupportedFile(this IFormFile file)
        {
            var allowedExtensions = new List<string>
            {
                ".jpg",
                ".jpeg",
                ".doc",
                ".docx",
                ".pdf",
                ".rtf",
                ".png",
                ".gif",
                ".txt"
            };

            var extension = Path.GetExtension(file.FileName);
            if (!allowedExtensions.Any(c => c.Equals(extension))) return false;
            if (file.Length < 256) return false;

            return true;
        }
    }
}