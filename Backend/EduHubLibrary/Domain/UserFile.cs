using EnsureThat;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain
{
    public class UserFile
    {
        public UserFile(string filename, string contentType)
        {
            Ensure.String.IsNotNullOrWhiteSpace(filename);
            Ensure.String.IsNotNullOrWhiteSpace(contentType);

            Filename = filename;
            ContentType = contentType;
        }

        public string Filename { get; private set; }
        public string ContentType { get; private set; }
    }
}
