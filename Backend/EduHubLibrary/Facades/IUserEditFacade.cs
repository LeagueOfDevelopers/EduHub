using System.Collections.Generic;
using EduHubLibrary.Domain.Tools;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Domain.NotificationService.UserSettings;

namespace EduHubLibrary.Facades
{
    public interface IUserEditFacade
    {
        void EditName(int userId, string newName);
        void EditAboutUser(int userId, string newAboutUser);
        void EditGender(int userId, Gender gender);
        void EditAvatarLink(int userId, string newAvatarLink);
        void EditContacts(int userId, List<string> newContactData);
        void EditBirthYear(int userId, int newYear);
        void BecomeTeacher(int userId);
        void StopToBeTeacher(int userId);
        void EditProfile(int userId, string newName, string newAboutUser, Gender newGender, string newAvatarLink,
            List<string> newContactData, int newYear);
        void ConfigureNotificationsSettings(int userId, EventType configuringNotification, NotificationValue newValue);
    }
}