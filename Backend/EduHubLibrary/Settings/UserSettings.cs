using EnsureThat;

namespace EduHubLibrary.Settings
{
    public class UserSettings
    {
        public UserSettings(string defaultAvatar)
        {
            DefaultAvatar = Ensure.Any.IsNotNull(defaultAvatar);
        }

        public string DefaultAvatar { get; }
    }
}