using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using EduHubLibrary.Common;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Domain.Tools;
using EnsureThat;

[assembly: InternalsVisibleTo("EventBusTests")]

namespace EduHubLibrary.Domain
{
    public class User
    {
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

        public Credentials Credentials { get; }
        public UserType Type { get; set; }
        public TeacherProfile TeacherProfile { get; }
        public UserProfile UserProfile { get; }
        public bool IsActive { get; private set; }
        public Guid Id { get; }
        public List<Invitation> Invitations { get; }
        public List<string> Notifies { get; }

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
            if (newInvitation.ToUser.Equals(Id)) Invitations.Add(newInvitation);
            //TODO: create exception class
            else throw new ArgumentException("User's ids are not equal");
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

        internal Invitation GetInvitation(Guid invitationId)
        {
            Ensure.Guid.IsNotEmpty(invitationId);
            return Ensure.Any.IsNotNull(Invitations.Find(current => current.Id == invitationId));
        }

        internal void AddNotify(string notify)
        {
            Ensure.String.IsNotNullOrWhiteSpace(notify);
            Notifies.Add(notify);
        }
    }
}