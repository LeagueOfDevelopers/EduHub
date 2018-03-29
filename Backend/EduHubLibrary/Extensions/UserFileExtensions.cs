using EduHubLibrary.Data.FileDtos;
using EduHubLibrary.Domain;

namespace EduHubLibrary.Extensions
{
    internal static class UserFileExtensions
    {
        public static UserFile ParseFromUserFileDto(UserFileDto userFileDto)
        {
            var userFile = new UserFile(userFileDto.Filename, userFileDto.ContentType);
            return userFile;
        }
    }
}