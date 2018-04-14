using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Mailing.EmailModels
{
    public class RestorePasswordMessage
    {
        public RestorePasswordMessage(string username, int key)
        {
            Username = username;
            Key = key;
        }

        public string Username { get; }
        public int Key { get; }
    }
}
