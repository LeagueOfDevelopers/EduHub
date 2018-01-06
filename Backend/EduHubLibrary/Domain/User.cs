using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnsureThat;
using EduHubLibrary.Common;

namespace EduHubLibrary.Domain
{
    public class User
    {
        public string Name { get; private set; }
        public Credentials Credentials { get; private set; }
        public TypeOfUser Type { get; set; }
        public bool IsTeacher { get; private set; }
        public TeacherProfile TeacherProfile { get; private set; }
        public bool IsActive { get; private set; }
        public Guid Id { get; private set; }
        public string AvatarLink { get; private set; }
        public List<Invitation> listOfInvitation { get; private set; }

        public User(string name, Credentials credentials, bool isTeacher, TypeOfUser type, string avatarLink)
        {
            Name = Ensure.String.IsNotNullOrWhiteSpace(name);
            Credentials = credentials;
            Type = type;
            TeacherProfile = new TeacherProfile();
            IsTeacher = isTeacher;
            IsActive = true;
            Id = Guid.NewGuid();
            AvatarLink = avatarLink;
            listOfInvitation = new List<Invitation>();
        }

        public void EditName(string newName)
        {
            Name = Ensure.String.IsNotNullOrWhiteSpace(newName);
        }

        public void ConfigureTeacherProfile(List<string> skills)
        {
            TeacherProfile.ConfigureSkills(skills);
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
            Type = TypeOfUser.Admin;
        }

        public void StopToBeAdmin()
        {
            Type = TypeOfUser.User;
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
            listOfInvitation.Add(newInvitation);
        }

        internal void AcceptInvitation(Guid invitationId)
        {
            Ensure.Guid.IsNotEmpty(invitationId);
            Invitation currentInvitation =
                Ensure.Any.IsNotNull(listOfInvitation.Find(current => current.Id == invitationId));
            currentInvitation.Status = InvitationStatus.Accepted;
        }
        internal void DeclineInvitation(Guid invitationId)
        {
            Ensure.Guid.IsNotEmpty(invitationId);
            Invitation currentInvitation =
                Ensure.Any.IsNotNull(listOfInvitation.Find(current => current.Id == invitationId));
            currentInvitation.Status = InvitationStatus.Declined;
        }

        internal IEnumerable<Invitation> GetAllInvitation()
        {
            return listOfInvitation;
        }

        internal Invitation GetInvitationById(Guid invitationId)
        {
            Ensure.Guid.IsNotEmpty(invitationId);
            return Ensure.Any.IsNotNull(listOfInvitation.Find(current => current.Id == invitationId));
        }
    }
}
