using EnsureThat;

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

        public string Filename { get; }
        public string ContentType { get; }
    }
}