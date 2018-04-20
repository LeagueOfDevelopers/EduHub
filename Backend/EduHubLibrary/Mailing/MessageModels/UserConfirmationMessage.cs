using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Mailing.MessageModels
{
    public class UserConfirmationMessage
    {
        public UserConfirmationMessage(string username, int key)
        {
            Username = username;
            Key = key;
        }

        public string Username { get; }
        public int Key { get; }
    }
}
