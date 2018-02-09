using Microsoft.AspNetCore.Http;
using MimeSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.IO;

namespace EduHub.Extensions
{
    public static class FileExtension
    {
        public static bool IsSupportedFile(this IFormFile file)
        {

            var allowedExtensions = new List<string>
            {
                ".jpg", ".jpeg", ".doc", ".docx", ".pdf", ".rtf", ".png", ".gif", ".txt"
            };

            string extension = Path.GetExtension(file.FileName);
            if (!allowedExtensions.Any(c => c.Equals(extension)))
            { 
                return false;
            }
            if (file.Length < 256)
            {
                return false;
            }

            return true;
        }
    }
}
