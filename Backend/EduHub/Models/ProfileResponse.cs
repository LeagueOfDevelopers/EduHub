using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Tools;

namespace EduHub.Models
{
    public class ProfileResponse
    {
        public UserProfile UserProfile { get; set; }
        public TeacherProfile TeacherProfile { get; set; }

        public ProfileResponse(UserProfile userProfile)
        {
            UserProfile = userProfile;
        }

        public ProfileResponse(UserProfile userProfile, TeacherProfile teacherProfile)
        {
            UserProfile = userProfile;
            TeacherProfile = teacherProfile;
        }
    }
}
