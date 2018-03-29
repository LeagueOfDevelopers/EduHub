using System.ComponentModel.DataAnnotations;

namespace EduHubLibrary.Data.FileDtos
{
    public class UserFileDto
    {
        public UserFileDto(string filename, string contentType)
        {
            Filename = filename;
            ContentType = contentType;
        }

        internal UserFileDto()
        {
        }

        [Key] [MaxLength(250)] public string Filename { get; set; }

        public string ContentType { get; set; }
    }
}