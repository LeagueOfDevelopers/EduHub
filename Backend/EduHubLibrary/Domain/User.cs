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
    public class User
    {
        public Credentials Credentials { get; private set; }
        public UserType Type { get; set; }
        public TeacherProfile TeacherProfile { get; private set; }
        public UserProfile UserProfile { get; private set; }
        public bool IsActive { get; private set; }
        public Guid Id { get; }
        public List<Invitation> ListOfInvitations { get; private set; }

        public User(string name, Credentials credentials, bool isTeacher, UserType type)
        {
            Ensure.String.IsNotNullOrWhiteSpace(name);
            Credentials = Ensure.Any.IsNotNull(credentials);
            Type = type;
            TeacherProfile = new TeacherProfile();
            UserProfile = new UserProfile(name, Credentials.Email, isTeacher);
            IsActive = true;
            Id = Guid.NewGuid();
            ListOfInvitations = new List<Invitation>();
            _notifies = new List<string>();
        }

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

        public void AddNotify(string notify)
        {
            _notifies.Add(notify);
        }

        public List<string> GetNotifies()
        {
            return _notifies;
        }
        
        private List<string> _notifies;
    }
}
