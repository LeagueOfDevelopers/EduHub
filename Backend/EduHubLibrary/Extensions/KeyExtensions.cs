using EduHubLibrary.Data.KeyDtos;
using EduHubLibrary.Domain;

namespace EduHubLibrary.Extensions
{
    internal static class KeyExtensions
    {
        public static Key ParseFromKeyDto(KeyDto keyDto)
        {
            var result = new Key(keyDto.UserEmail, keyDto.Appointment, keyDto.Used, keyDto.Value);
            return result;
        } 
    }
}
