using System;
using System.Linq;
using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EnsureThat;

namespace EduHubLibrary.Facades
{
    public class AuthUserFacade : IAuthUserFacade
    {
        private readonly IKeysRepository _keysRepository;
        private readonly EmailSender _sender;
        private readonly IUserRepository _userRepository;

        public AuthUserFacade(IKeysRepository keysRepository, IUserRepository userRepository, EmailSender sender)
        {
            _keysRepository = keysRepository;
            _userRepository = userRepository;
            _sender = sender;
        }

        public Guid RegUser(string username, Credentials credentials, bool isTeacher, UserType userType)
        {
            Ensure.String.IsNotNullOrWhiteSpace(username);
            Ensure.Any.IsNotNull(credentials);
            Ensure.Bool.IsFalse(_userRepository.GetAll().Any(u => u.Credentials.Email.Equals(credentials.Email)),
                nameof(RegUser), opt => opt.WithException(new UserAlreadyExistsException(credentials.Email)));

            var user = new User(username, credentials, isTeacher, userType);
            var key = new Key(user.Id);
            _keysRepository.AddKey(key);
            _userRepository.Add(user);
            var text = "Подтвердите регистрацию на сайте EduHub, перейдя по ссылке " +
                       $"{_sender.ConfirmAdress}/{key.Value}";
            var theme = "Подтверждение аккаунта";
            _sender.SendMessage(username, credentials.Email, text, theme);
            return user.Id;
        }

        public void ConfirmUser(Guid key)
        {
            Ensure.Guid.IsNotEmpty(key);
            var currentKey = _keysRepository.GetKey(key);
            Ensure.Bool.IsFalse(currentKey.Used, nameof(currentKey.Used),
                opt => opt.WithException(new KeyAlreadyUsedException()));

            currentKey.UseKey();
            _userRepository.GetUserById(currentKey.UserId).Type = UserType.User;
        }
    }
}