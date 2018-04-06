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
            return allowedExtensions.Any(c => c.Equals(extension));
        }

        public static bool IsImg(this string fileName)
        {
            var allowedExtensions = new List<string>
            {
                ".jpg",
                ".jpeg",
                ".png"
            };

            var extension = Path.GetExtension(fileName);
            return allowedExtensions.Any(c => c.Equals(extension));
        }
    }
}