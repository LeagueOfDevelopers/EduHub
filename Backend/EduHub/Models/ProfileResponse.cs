using EduHub.Models.Tools;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Tools;

namespace EduHub.Models
{
    public class ProfileResponse
    {
        public UserProfileModel UserProfile { get; set; }
        public TeacherProfileModel TeacherProfile { get; set; }

        public ProfileResponse(UserProfileModel userProfile)
        {
            UserProfile = userProfile;
        }

        public ProfileResponse(UserProfileModel userProfile, TeacherProfileModel teacherProfile)
        {
            UserProfile = userProfile;
            TeacherProfile = teacherProfile;
        }
    }
}
