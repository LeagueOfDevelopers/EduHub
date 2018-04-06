using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using EduHubLibrary.Common;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Domain.Tools;
using EnsureThat;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Domain.NotificationService.UserSettings;
using EduHubLibrary.Domain.Events;

[assembly: InternalsVisibleTo("EventBusTests")]

namespace EduHubLibrary.Domain
{
    public class User
    {
        public User(string name, Credentials credentials, bool isTeacher, UserType type, int id = 0)
        {
            Id = id;
            Ensure.String.IsNotNullOrWhiteSpace(name);
            Credentials = Ensure.Any.IsNotNull(credentials);
            Type = type;
            TeacherProfile = new TeacherProfile();
            UserProfile = new UserProfile(name, Credentials.Email, isTeacher);
            IsActive = true;
            Invitations = new List<Invitation>();
            Notifies = new List<string>();
            Notifications = new List<Event>();
            NotificationsSettings = new NotificationsSettings();
        }

        /*private User()
        {
            TeacherProfile = new TeacherProfile();
            UserProfile = new UserProfile();
            Credentials = new Credentials();
        }*/

        //constr for db
        internal User(string name, Credentials credentials, UserType type,
            List<Invitation> invitationList, TeacherProfile teacherProfile, UserProfile userProfile, bool isActive,
            List<string> notifiesList, int id = 0)
        {
            Id = id;
            Ensure.String.IsNotNullOrWhiteSpace(name);
            Credentials = Ensure.Any.IsNotNull(credentials);
            Type = type;
            TeacherProfile = Ensure.Any.IsNotNull(teacherProfile);
            UserProfile = Ensure.Any.IsNotNull(userProfile);
            IsActive = isActive;
            Invitations = Ensure.Any.IsNotNull(invitationList);
            Notifies = Ensure.Any.IsNotNull(notifiesList);
        }

        //constr for db
        internal User(string name, string email, int id)
        {
            Id = id;
            UserProfile = new UserProfile(name, email, true);
        }

        public Credentials Credentials { get; private set; }
        public UserType Type { get; set; }
        public TeacherProfile TeacherProfile { get; }
        public UserProfile UserProfile { get; }
        public bool IsActive { get; private set; }
        public int Id { get; internal set; }
        public List<Invitation> Invitations { get; }
        public List<string> Notifies { get; }
        public List<Event> Notifications { get; }
        public NotificationsSettings NotificationsSettings { get; }

        public void ConfigureTeacherProfile(List<string> skills)
        {
            TeacherProfile.Skills = skills;
        }

        public void BecomeModerator()
        {
            Type = UserType.Moderator;
        }

        public void StopToBeModerator()
        {
            Type = UserType.User;
        }

        public void RestoreProfile()
        {
            IsActive = true;
        }

        public void DeleteProfile()
        {
            IsActive = false;
        }

        internal void AddInvitation(Invitation newInvitation)
        {
            if (newInvitation.ToUser.Equals(Id)) Invitations.Add(newInvitation);
            //TODO: create exception class
            else throw new ArgumentException("User's ids are not equal");
        }

        internal void AcceptInvitation(int invitationId)
        {
            var currentInvitation =
                Ensure.Any.IsNotNull(Invitations.Find(current => current.Id == invitationId));
            if (currentInvitation.Status != InvitationStatus.InProgress)
                throw new InvitationAlreadyChangedException(invitationId);
            currentInvitation.Status = InvitationStatus.Accepted;
        }

        internal void DeclineInvitation(int invitationId)
        {
            var currentInvitation =
                Ensure.Any.IsNotNull(Invitations.Find(current => current.Id == invitationId));
            if (currentInvitation.Status != InvitationStatus.InProgress)
                throw new InvitationAlreadyChangedException(invitationId);
            currentInvitation.Status = InvitationStatus.Declined;
        }

        internal Invitation GetInvitation(int invitationId)
        {
            return Ensure.Any.IsNotNull(Invitations.Find(current => current.Id == invitationId));
        }

        internal void AddNotify(Event notify)
        {
            Ensure.Any.IsNotNull(notify);
            Notifications.Add(notify);
        }

        internal void ChangePassword(string newPassword)
        {
            var newCredentials = Credentials.FromRawData(Credentials.Email, newPassword);
            Credentials = newCredentials;
        }

        internal void ConfigureNotificationsSettings(EventType configuringEvent, NotificationValue newValue)
        {
                NotificationsSettings.ConfigureSettings(configuringEvent, newValue);
        }
    }
}