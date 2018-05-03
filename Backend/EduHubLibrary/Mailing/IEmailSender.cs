namespace EduHubLibrary.Mailing
{
    public interface IEmailSender
    {
        void SendMessage(string email, object messageContent, string theme, string username = "");
    }
}