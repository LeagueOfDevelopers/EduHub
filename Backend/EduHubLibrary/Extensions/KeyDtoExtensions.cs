using EduHubLibrary.Data.KeyDtos;
using EduHubLibrary.Domain;

namespace EduHubLibrary.Extensions
{
    internal static class KeyDtoExtensions
    {
        public static KeyDto ParseFromKey(Key key)
        {
            var result = new KeyDto(key.Used, key.Value, key.UserEmail, key.Appointment);
            return result;
        }

        public static void UpdateKey(this KeyDto keyDto, Key key)
        {
            keyDto.Used = key.Used;
        }
    }
}