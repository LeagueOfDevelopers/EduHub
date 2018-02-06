using EnsureThat;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService
{
    public class EventBusSettings
    {
        public EventBusSettings(string hostName, string virtualHost, string userName, string password)
        {
            Ensure.Any.IsNotNull(hostName);
            Ensure.Any.IsNotNull(virtualHost);
            Ensure.Any.IsNotNull(userName);
            Ensure.Any.IsNotNull(password);
            HostName = hostName;
            VirtualHost = virtualHost;
            UserName = userName;
            Password = password;
        }

        public string HostName;
        public string VirtualHost;
        public string UserName;
        public string Password;
    }
}
