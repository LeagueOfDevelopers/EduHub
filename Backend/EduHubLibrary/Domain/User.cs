using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnsureThat;
using EduHubLibrary.Common;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Infrastructure;
using EduHubLibrary.Domain.Tools;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("EventBusTests")]

namespace EduHubLibrary.Domain
{
    public class User : ISubscriber
    {
        public Credentials Credentials { get; private set; }
        public UserType Type { get; set; }
        public TeacherProfile TeacherProfile { get; private set; }
        public UserProfile UserProfile { get; private set; }
        public bool IsActive { get; private set; }
        public Guid Id { get; private set; }
        public List<Invitation> ListOfInvitations { get; private set; }

        public User(string name, Credentials credentials, bool isTeacher, UserType type, string avatarLink)
        {
            Ensure.String.IsNotNullOrWhiteSpace(name);
            Ensure.String.IsNotNullOrWhiteSpace(avatarLink);
            Credentials = Ensure.Any.IsNotNull(credentials);
            Type = type;
            TeacherProfile = new TeacherProfile();
            UserProfile = new UserProfile(name, Credentials.Email, avatarLink, isTeacher);
            IsActive = true;
            Id = Guid.NewGuid();
            ListOfInvitations = new List<Invitation>();
            _events = new InMemoryEventRepository();
        }

        #region Edit Profile Data Methods
        public void EditName(string newName)
        {
            UserProfile.Name = Ensure.String.IsNotNullOrWhiteSpace(newName);
        }

        public void EditAboutUser(string newAboutUser)
        {
            UserProfile.AboutUser = Ensure.String.IsNotNullOrWhiteSpace(newAboutUser);
        }

        public void EditGender(bool isMan)
        {
            UserProfile.IsMan = isMan;
        }

        public void EditAvatarLink(string newAvatarLink)
        {
            UserProfile.AvatarLink = Ensure.String.IsNotNullOrWhiteSpace(newAvatarLink);
        }

        public void EditContacts(List<string> newContactData)
        {
            UserProfile.Contacts = Ensure.Any.IsNotNull(newContactData);
        }

        public void EditBirthYear(string newDate)
        {
            UserProfile.BirthYear = Ensure.String.IsNotNullOrWhiteSpace(newDate);
        }

        public void BecomeTeacher()
        {
            UserProfile.IsTeacher = true;
        }

        public void StopToBeTeacher()
        {
            UserProfile.IsTeacher = false;
        }
        #endregion

        public void ConfigureTeacherProfile(List<string> skills)
        {
            TeacherProfile.Skills = skills;
        }

        public void BecomeAdmin()
        {
            Type = UserType.Admin;
        }

        public void StopToBeAdmin()
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
            ListOfInvitations.Add(newInvitation);
        }

        internal void AcceptInvitation(Guid invitationId)
        {
            Ensure.Guid.IsNotEmpty(invitationId);
            Invitation currentInvitation =
                Ensure.Any.IsNotNull(ListOfInvitations.Find(current => current.Id == invitationId));
            if (currentInvitation.Status != InvitationStatus.InProgress)
                throw new InvitationAlreadyChangedException(invitationId);
            currentInvitation.Status = InvitationStatus.Accepted;
        }

        internal void DeclineInvitation(Guid invitationId)
        {
            Ensure.Guid.IsNotEmpty(invitationId);
            Invitation currentInvitation =
                Ensure.Any.IsNotNull(ListOfInvitations.Find(current => current.Id == invitationId));
            if (currentInvitation.Status != InvitationStatus.InProgress)
                throw new InvitationAlreadyChangedException(invitationId);
            currentInvitation.Status = InvitationStatus.Declined;
        }

        internal IEnumerable<Invitation> GetAllInvitation()
        {
            return ListOfInvitations;
        }

        internal Invitation GetInvitationById(Guid invitationId)
        {
            Ensure.Guid.IsNotEmpty(invitationId);
            return Ensure.Any.IsNotNull(ListOfInvitations.Find(current => current.Id == invitationId));
        }

        public List<Event> GetNotifies()
        {
            return _events.GetAllEvents().ToList();
        }


        //TODO delete: It was created to show work of message bus
        public void GetMessage(Event @event)
        {
            if (@event.EventInfo.GetEventType().Equals("EditedGroupEvent"))
            {
                _events.AddMessage(@event);
            }
        }

        private InMemoryEventRepository _events;
    }
}
