using System.Linq;
using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Mailing;
using EduHubLibrary.Mailing.MessageModels;
using EduHubLibrary.Settings;
using EnsureThat;

namespace EduHubLibrary.Facades
{
    public class AccountFacade : IAccountFacade
    {
        private readonly IKeysRepository _keysRepository;
        private readonly IEmailSender _sender;
        private readonly IUserRepository _userRepository;
        private readonly UserSettings _userSettings;

        public AccountFacade(IKeysRepository keysRepository, IUserRepository userRepository,
            IEmailSender sender, UserSettings userSettings)
        {
            _keysRepository = keysRepository;
            _userRepository = userRepository;
            _sender = sender;
            _userSettings = userSettings;
        }

        public int RegUser(string username, Credentials credentials, bool isTeacher)
        {
            Ensure.String.IsNotNullOrWhiteSpace(username);
            Ensure.Any.IsNotNull(credentials);
            Ensure.Bool.IsFalse(_userRepository.GetAll().Any(u => u.Credentials.Email.Equals(credentials.Email)),
                nameof(RegUser), opt => opt.WithException(new UserAlreadyExistsException(credentials.Email)));

            var user = new User(username, credentials, isTeacher, UserType.UnConfirmed, _userSettings.DefaultAvatar);
            var key = new Key(user.Credentials.Email, KeyAppointment.ConfirmEmail);

            _keysRepository.AddKey(key);
            _sender.SendMessage(credentials.Email, new UserConfirmationMessage(username, key.Value),
                MessageThemes.UserConfirmation, username);
            _userRepository.Add(user);
            return user.Id;
        }

        public int RegUser(string username, Credentials credentials, bool isTeacher, int regKey)
        {
            Ensure.String.IsNotNullOrWhiteSpace(username);
            Ensure.Any.IsNotNull(credentials);
            Ensure.Bool.IsFalse(_userRepository.GetAll().Any(u => u.Credentials.Email.Equals(credentials.Email)),
                nameof(RegUser), opt => opt.WithException(new UserAlreadyExistsException(credentials.Email)));

            var key = _keysRepository.GetKey(regKey);
            Ensure.Bool.IsTrue(key.UserEmail == credentials.Email, nameof(key.UserEmail),
                opt => opt.WithException(new InappropriateEmailException(key.UserEmail, credentials.Email)));
            CheckKey(key, KeyAppointment.BecomeAdmin, KeyAppointment.BecomeModerator);

            var userType = key.Appointment.Equals(KeyAppointment.BecomeAdmin) ? UserType.Admin : UserType.Moderator;
            var user = new User(username, credentials, isTeacher, userType, _userSettings.DefaultAvatar);
            _userRepository.Add(user);

            return user.Id;
        }

        public void ConfirmUser(int key)
        {
            var currentKey = _keysRepository.GetKey(key);
            CheckKey(currentKey, KeyAppointment.ConfirmEmail);

            currentKey.UseKey();
            _userRepository.GetUserByEmail(currentKey.UserEmail).Type = UserType.User;
            _keysRepository.UpdateKey(currentKey);
        }

        public void CheckAdminExistence(string email)
        {
            Ensure.String.IsNotNullOrWhiteSpace(email);

            if (_userRepository.GetAll().All(user => user.UserProfile.Email != email))
            {
                var key = new Key(email, KeyAppointment.BecomeAdmin);
                _keysRepository.AddKey(key);
                _sender.SendMessage(email, new AdminInvitationMessage(key.Value), MessageThemes.AdminInvitation);
            }
        }

        public void ChangePassword(int userId, string newPassword)
        {
            Ensure.String.IsNotNullOrWhiteSpace(newPassword);
            var currentUser = _userRepository.GetUserById(userId);
            currentUser.ChangePassword(newPassword);
            _userRepository.Update(currentUser);
        }

        public void ChangePassword(string newPassword, int key)
        {
            Ensure.String.IsNotNullOrWhiteSpace(newPassword);
            var currentKey = _keysRepository.GetKey(key);
            CheckKey(currentKey, KeyAppointment.ChangePassword);

            var currentUser = _userRepository.GetUserByEmail(currentKey.UserEmail);
            currentUser.ChangePassword(newPassword);
            _userRepository.Update(currentUser);
            currentKey.UseKey();
            _keysRepository.UpdateKey(currentKey);
        }

        public void SendQueryToChangePassword(string email)
        {
            var username = _userRepository.GetUserByEmail(email).UserProfile.Name;
            var key = new Key(email, KeyAppointment.ChangePassword);
            _keysRepository.AddKey(key);

            _sender.SendMessage(email, new RestorePasswordMessage(username, key.Value), MessageThemes.RestorePassword,
                username);
        }

        public void SendTokenToModerator(string email)
        {
            Ensure.Bool.IsFalse(_userRepository.GetAll().Any(u => u.Credentials.Email.Equals(email)),
                nameof(RegUser), opt => opt.WithException(new UserAlreadyExistsException(email)));

            var key = new Key(email, KeyAppointment.BecomeModerator);
            _keysRepository.AddKey(key);

            _sender.SendMessage(email, new ModeratorInvitationMessage(key.Value), MessageThemes.ModeratorInvitation);
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