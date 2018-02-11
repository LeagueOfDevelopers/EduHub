using EduHub.Models.Tools;

namespace EduHub.Models
{
    public class ProfileResponse
    {
        public ProfileResponse(UserProfileModel userProfile)
        {
            UserProfile = userProfile;
        }

        public ProfileResponse(UserProfileModel userProfile, TeacherProfileModel teacherProfile)
        {
            UserProfile = userProfile;
            TeacherProfile = teacherProfile;
        }

        public UserProfileModel UserProfile { get; set; }
        public TeacherProfileModel TeacherProfile { get; set; }
    }
}