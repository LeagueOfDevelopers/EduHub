namespace EduHubLibrary.Domain.NotificationService
{
    public class EventBusSettings
    {
        public string HostName;
        public string Password;
        public string UserName;
        public string VirtualHost;

        public EventBusSettings(string hostName, string virtualHost, string userName, string password)
        {
            HostName = hostName;
            VirtualHost = virtualHost;
            UserName = userName;
            Password = password;
        }
    }
}