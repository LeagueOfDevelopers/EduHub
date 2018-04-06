using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Mailing
{
    public interface IEmailSender
    {
        void SendMessage(string email, string text, string theme, string username = "");
    }
}
