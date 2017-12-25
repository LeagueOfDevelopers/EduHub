using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnsureThat;

namespace EduHubLibrary.Domain
{
    public class User
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public bool IsTeacher { get; private set; }
        public bool IsActive { get; private set; }
        public Guid Id { get; private set; }
        public List<Invitation> listOfInvitation { get; private set; }

        public User(string name, string email, string password, bool isTeacher)
        {
            Name = Ensure.String.IsNotNullOrWhiteSpace(name);
            Email = Ensure.String.IsNotNullOrWhiteSpace(email);
            Password = Ensure.String.IsNotNullOrWhiteSpace(password);
            IsTeacher = isTeacher;
            IsActive = true;
            Id = Guid.NewGuid();
            listOfInvitation = new List<Invitation>();
        }

        public void EditName(string newName)
        {
            Name = Ensure.String.IsNotNullOrWhiteSpace(newName);
        }

        public void BecomeTeacher()
        {
            IsTeacher = true;
        }

        public void StopToBeTeacher()
        {
            IsTeacher = false;
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
