using EduHubLibrary.Settings;
using MimeKit;

namespace EduHubLibrary.Common
{
    public class EmailSender
    {
        private readonly EmailSettings _settings;

        public EmailSender(EmailSettings settings)
        {
            _settings = settings;
        }

        public string ConfirmAdress => _settings.ConfirmAdress;

        public void SendMessage(string username, string email, string text, string theme)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("EduHub", _settings.Email));
            message.To.Add(new MailboxAddress(username, email));
            message.Subject = theme;

            message.Body = new TextPart("plain")
            {
                Text = text
            };

            /*using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect(_settings.SmtpAdress, _settings.SmtpPort, true);

                client.Authenticate(_settings.EmailLogin, _settings.EmailPassword);

                client.Send(message);
                client.Disconnect(true);
            }*/
        }
    }
}