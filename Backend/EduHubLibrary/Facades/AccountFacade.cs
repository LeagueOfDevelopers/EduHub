using System;
using System.Linq;
using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Mailing;
using EnsureThat;

namespace EduHubLibrary.Facades
{
    public class AccountFacade : IAccountFacade
    {
        private readonly IKeysRepository _keysRepository;
        private readonly EmailSender _sender;
        private readonly IUserRepository _userRepository;

        public AccountFacade(IKeysRepository keysRepository, IUserRepository userRepository, EmailSender sender)
        {
            _keysRepository = keysRepository;
            _userRepository = userRepository;
            _sender = sender;
        }

        public Guid RegUser(string username, Credentials credentials, bool isTeacher)
        {
            CheckUserExistence(username, credentials);

            var user = new User(username, credentials, isTeacher, UserType.UnConfirmed);
            var key = new Key(user.Credentials.Email, KeyAppointment.ConfirmEmail);
            var text = string.Format(EmailTemplates.ConfirmEmail,
                username, _sender.ConfirmAdress, key.Value);
            var theme = EmailTemplates.ConfirmEmailTheme;

            _sender.SendMessage(username, credentials.Email, text, theme);
            _keysRepository.AddKey(key);
            _userRepository.Add(user);
            return user.Id;
        }

        public Guid RegUser(string username, Credentials credentials, bool isTeacher, Guid regKey)
        {
            CheckUserExistence(username, credentials);

            Ensure.Guid.IsNotEmpty(regKey);
            var key = _keysRepository.GetKey(regKey);
            CheckKey(key, KeyAppointment.BecomeAdmin, KeyAppointment.BecomeModerator);

            var userType = key.Appointment.Equals(KeyAppointment.BecomeAdmin) ? UserType.Admin : UserType.Moderator;
            var user = new User(username, credentials, isTeacher, userType);
            _userRepository.Add(user);

            return user.Id;
        }

        public void ConfirmUser(Guid key)
        {
            Ensure.Guid.IsNotEmpty(key);
            var currentKey = _keysRepository.GetKey(key);
            CheckKey(currentKey, KeyAppointment.ConfirmEmail);

            currentKey.UseKey();
            _userRepository.GetUserByEmail(currentKey.UserEmail).Type = UserType.User;
        }
        
        public void CheckAdminExistence(string email, string adminName)
        {
            if (!_userRepository.GetAll().Any(user => user.UserProfile.Email == email))
            {
                var key = new Key(email, KeyAppointment.BecomeAdmin);
                _keysRepository.AddKey(key);
                var text = string.Format(EmailTemplates.AdminInvitationEmail, key.Value);
                _sender.SendMessage(adminName, email, text, EmailTemplates.AdminInvitationEmailTheme);
            }
        }

        public void ChangePassword(Guid userId, string newPassword)
        {
            Ensure.String.IsNotNullOrWhiteSpace(newPassword);
            _userRepository.GetUserById(userId).ChangePassword(newPassword);
        }

        public void ChangePassword(string newPassword, Guid key)
        {
            Ensure.String.IsNotNullOrWhiteSpace(newPassword);
            var currentKey = _keysRepository.GetKey(key);
            CheckKey(currentKey, KeyAppointment.ChangePassword);

            _userRepository.GetUserByEmail(currentKey.UserEmail).ChangePassword(newPassword);
            currentKey.UseKey();
        }

        public void SendQueryToChangePassword(string email)
        {
            var username = _userRepository.GetUserByEmail(email).UserProfile.Name;
            var key = new Key(email, KeyAppointment.ChangePassword);
            _keysRepository.AddKey(key);
            var text = string.Format(EmailTemplates.RestorePasswordEmail, username, key.Value);
            _sender.SendMessage(username, email, text, EmailTemplates.RestorePasswordEmailTheme);
        }

        private void CheckUserExistence(string username, Credentials credentials)
        {
            Ensure.String.IsNotNullOrWhiteSpace(username);
            Ensure.Any.IsNotNull(credentials);
            Ensure.Bool.IsFalse(_userRepository.GetAll().Any(u => u.Credentials.Email.Equals(credentials.Email)),
                nameof(RegUser), opt => opt.WithException(new UserAlreadyExistsException(credentials.Email)));
        }

        private void CheckKey(Key key, params KeyAppointment[] possipleAppointments)
        {
            Ensure.Bool.IsTrue(possipleAppointments.Contains(key.Appointment), nameof(key.Appointment),
                opt => opt.WithException(new WrongKeyAppointmentException(key.Appointment)));
            Ensure.Bool.IsFalse(key.Used, nameof(key.Used),
                opt => opt.WithException(new KeyAlreadyUsedException()));
        }
    }
}