using EduHubLibrary.Settings;
using MailKit.Net.Smtp;
using MimeKit;

namespace EduHubLibrary.Mailing
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _settings;

        public EmailSender(EmailSettings settings)
        {
            _settings = settings;
        }

        public void SendMessage(string email, object messageContent, string theme, string username = "")
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("EduHub", _settings.Email));
            message.To.Add(new MailboxAddress(username, email));
            message.Subject = theme;

            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = MessageRenderer.RenderPartialToString(messageContent)
            };
            
            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect(_settings.SmtpAdress, _settings.SmtpPort, true);

                client.Authenticate(_settings.EmailLogin, _settings.EmailPassword);

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}