using EduHubLibrary.Common;

namespace EduHub.Models
{
    public class RegistrationRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public TypeOfUser Type { get; set; }
        public bool IsTeacher { get; set; }
        public string AvatarLink { get; set; }
    }
}
