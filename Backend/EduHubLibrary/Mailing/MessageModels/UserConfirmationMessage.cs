using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Mailing.EmailModels
{
    public class UserConfirmationMessage
    {
        public UserConfirmationMessage(string username)
        {
            Username = username;
        }

        public string Username { get; }
    }
}
