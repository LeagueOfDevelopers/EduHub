using System;
using System.Collections.Generic;
using EduHubLibrary.Domain.NotificationService.Notifications;
using EduHubLibrary.Domain.NotificationService.UserSettings;
using EduHubLibrary.Domain.Tools;

namespace EduHubLibrary.Facades
{
    public interface IUserEditFacade
    {
        void EditName(int userId, string newName);
        void EditAboutUser(int userId, string newAboutUser);
        void EditGender(int userId, Gender gender);
        void EditAvatarLink(int userId, string newAvatarLink);
        void EditContacts(int userId, List<string> newContactData);
        void EditBirthYear(int userId, DateTimeOffset newYear);
        void BecomeTeacher(int userId);
        void StopToBeTeacher(int userId);

        void EditProfile(int userId, string newName, string newAboutUser, Gender newGender, string newAvatarLink,
            List<string> newContactData, DateTimeOffset newYear);

        void EditTeacherProfile(int userId, List<string> newSkills);

        void ConfigureNotificationsSettings(int userId, NotificationType configuringNotification,
            NotificationValue newValue);
    }
}