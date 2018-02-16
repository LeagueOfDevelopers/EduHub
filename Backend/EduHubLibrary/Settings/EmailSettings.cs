namespace EduHubLibrary.Settings
{
    public class EmailSettings
    {
        public EmailSettings(string emailLogin, string email, string emailPassword, string smtpAdress,
            string confirmAdress, int smtpPort)
        {
            EmailLogin = emailLogin;
            Email = email;
            EmailPassword = emailPassword;
            SmtpAdress = smtpAdress;
            ConfirmAdress = confirmAdress;
            SmtpPort = smtpPort;
        }

        public string EmailLogin { get; }
        public string Email { get; }
        public string EmailPassword { get; }
        public string SmtpAdress { get; }
        public string ConfirmAdress { get; }
        public int SmtpPort { get; }
    }
}