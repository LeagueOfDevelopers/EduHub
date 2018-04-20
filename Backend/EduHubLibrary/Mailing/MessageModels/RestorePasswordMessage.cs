using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Mailing.MessageModels
{
    public class RestorePasswordMessage
    {
        public RestorePasswordMessage(string userName, int key)
        {
            UserName = userName;
            Key = key;
        }

        public string UserName { get; }
        public int Key { get; }
    }
}
