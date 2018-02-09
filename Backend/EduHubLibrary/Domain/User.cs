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
        public List<Invitation> Invitations { get; private set; }
        public List<string> Notifies { get; private set; }

        public User(string name, Credentials credentials, bool isTeacher, UserType type)
        {
            Ensure.String.IsNotNullOrWhiteSpace(name);
            Credentials = Ensure.Any.IsNotNull(credentials);
            Type = type;
            TeacherProfile = new TeacherProfile();
            UserProfile = new UserProfile(name, Credentials.Email, isTeacher);
            IsActive = true;
            Id = Guid.NewGuid();
            Invitations = new List<Invitation>();
            Notifies = new List<string>();
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
            Invitations.Add(newInvitation);
        }

        internal void AcceptInvitation(Guid invitationId)
        {
            Ensure.Guid.IsNotEmpty(invitationId);
            var currentInvitation =
                Ensure.Any.IsNotNull(Invitations.Find(current => current.Id == invitationId));
            if (currentInvitation.Status != InvitationStatus.InProgress)
                throw new InvitationAlreadyChangedException(invitationId);
            currentInvitation.Status = InvitationStatus.Accepted;
        }

        internal void DeclineInvitation(Guid invitationId)
        {
            Ensure.Guid.IsNotEmpty(invitationId);
            var currentInvitation =
                Ensure.Any.IsNotNull(Invitations.Find(current => current.Id == invitationId));
            if (currentInvitation.Status != InvitationStatus.InProgress)
                throw new InvitationAlreadyChangedException(invitationId);
            currentInvitation.Status = InvitationStatus.Declined;
        }

        internal IEnumerable<Invitation> GetAllInvitation()
        {
            return Invitations;
        }

        internal Invitation GetInvitationById(Guid invitationId)
        {
            Ensure.Guid.IsNotEmpty(invitationId);
            return Ensure.Any.IsNotNull(Invitations.Find(current => current.Id == invitationId));
        }

        internal void AddNotify(string notify)
        {
            Notifies.Add(notify);
        }
    }
}
