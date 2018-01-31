using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnsureThat;
using EduHubLibrary.Common;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Infrastructure;

namespace EduHubLibrary.Domain
{
    public class User : ISubscriber
    {
        public string Name { get; private set; }
        public Credentials Credentials { get; private set; }
        public UserType Type { get; set; }
        public bool IsTeacher { get; private set; }
        public TeacherProfile TeacherProfile { get; private set; }
        public bool IsActive { get; private set; }
        public Guid Id { get; private set; }
        public string AvatarLink { get; private set; }
        public List<Invitation> ListOfInvitations { get; private set; }

        private InMemoryEventRepository _events;

        public User(string name, Credentials credentials, bool isTeacher, UserType type, string avatarLink)
        {
            Name = Ensure.String.IsNotNullOrWhiteSpace(name);
            Credentials = credentials;
            Type = type;
            TeacherProfile = new TeacherProfile();
            IsTeacher = isTeacher;
            IsActive = true;
            Id = Guid.NewGuid();
            AvatarLink = avatarLink;
            ListOfInvitations = new List<Invitation>();
        }

        public void EditName(string newName)
        {
            Name = Ensure.String.IsNotNullOrWhiteSpace(newName);
        }

        public void ConfigureTeacherProfile(List<string> skills)
        {
            TeacherProfile.Skills = skills;
        }

        public void BecomeTeacher()
        {
            IsTeacher = true;
        }

        public void StopToBeTeacher()
        {
            IsTeacher = false;
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
            return _events.GetAllEvents();
        }

        public void Update(string description)
        {
            _events.AddEvent(new Event(description));
        }
    }
}
