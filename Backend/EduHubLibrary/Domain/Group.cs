using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EduHubLibrary.Domain.Exceptions;
using EnsureThat;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("EduHubTests")]

namespace EduHubLibrary.Domain
{
    public class Group
    {
        public void AddMember(Guid inviterId, Guid invitedId)
        {
            if (!IsMember(inviterId))
                throw new MemberNotFoundException(inviterId);
            var newMember = new Member(invitedId, MemberRole.Member);
            listOfMembers.Add(newMember);
        }

        public void DeleteMember(Guid requestedPerson, Guid deletingPerson)
        {
            Member deletingMember = GetMemberById(deletingPerson);
            Member requestedMember = GetMemberById(requestedPerson);
            if (!IsMember(requestedPerson))
                throw new MemberNotFoundException(requestedPerson);
            if (!IsMember(deletingPerson))
                throw new MemberNotFoundException(deletingPerson);
            if (requestedPerson != deletingPerson && requestedMember.MemberRole != MemberRole.Creator)
                throw new NotEnoughPermissionsException(requestedPerson);
            if (deletingMember.MemberRole == MemberRole.Creator)
            {
                DeleteCreator(deletingMember);
                return;
            }
            listOfMembers.Remove(GetMemberById(deletingPerson));
        }

        internal bool IsMember(Guid userId)
        {
            return listOfMembers.FirstOrDefault(current => current.UserId == userId) != null;
        }

        internal Member GetMemberById(Guid userId)
        {
            return listOfMembers.FirstOrDefault(current => current.UserId == userId);
        }

        internal IEnumerable<Member> GetAllMembers()
        {
            return listOfMembers;
        }

        private void DeleteCreator(Member deletingCreator)
        {
            if (deletingCreator.MemberRole != MemberRole.Creator)
                throw new NotEnoughPermissionsException(deletingCreator.UserId);
            int indexOfCreator = listOfMembers.IndexOf(deletingCreator);
            if (indexOfCreator + 1 == listOfMembers.Count)
            {
                listOfMembers.Remove(deletingCreator);
                IsActive = false;
                return;
            }
            Member newCreator = listOfMembers[indexOfCreator + 1];
            newCreator.ChangeRole(MemberRole.Creator);
            listOfMembers.Remove(deletingCreator);
        }

        public Group(Guid creatorId, List<Member> toWrite, string title, List<string> tags,
            string description)
        {
            Id = Ensure.Guid.IsNotEmpty(Guid.NewGuid());
            Tags = Ensure.Any.IsNotNull(tags);
            Title = Ensure.String.IsNotNullOrWhiteSpace(title);
            Description = Ensure.String.IsNotNullOrWhiteSpace(description);
            listOfMembers = Ensure.Any.IsNotNull(toWrite);
            var creator = new Member(creatorId, MemberRole.Creator);
            listOfMembers.Add(creator);
        }

        public Group(Guid creatorId, string title, List<string> tags, string description)
        {
            Id = Ensure.Guid.IsNotEmpty(Guid.NewGuid());
            Tags = Ensure.Any.IsNotNull(tags);
            Title = Ensure.String.IsNotNullOrWhiteSpace(title);
            Description = Ensure.String.IsNotNullOrWhiteSpace(description);
            listOfMembers = new List<Member>();
            var creator = new Member(creatorId, MemberRole.Creator);
            listOfMembers.Add(creator);
        }
        public Guid Id { get; private set; }
        public string Description { get; private set; }
        public string Title { get; private set; }
        public List<string> Tags { get; private set; }
        private List<Member> listOfMembers;
        public Chat Chat { get; private set; }
        public Course Course { get; private set; }
        public bool IsActive { get; private set; }
    }
}
