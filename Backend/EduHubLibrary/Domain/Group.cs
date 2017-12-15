using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EduHubLibrary.Domain.Exceptions;
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("UnitTests")]

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

        private void DeleteCreator(Member deletingCreator)
        {
            if (deletingCreator.MemberRole != MemberRole.Creator)
                throw new NotEnoughPermissionsException(deletingCreator.UserId);
            int indexOfCreator = listOfMembers.IndexOf(deletingCreator);
            if (indexOfCreator+1 == listOfMembers.Count)
            {
                listOfMembers.Remove(deletingCreator);
                IsActive = false;
                return;
            }
            Member newCreator = listOfMembers[indexOfCreator + 1];
            newCreator.ChangeRole(MemberRole.Creator);
            listOfMembers.Remove(deletingCreator);
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

        public Group(Guid groupId, Guid creatorId)
        {
            Id = groupId;
            listOfMembers = new List<Member>();
            var creator = new Member(creatorId, MemberRole.Creator);
            listOfMembers.Add(creator);
        }
        public Guid Id { get; protected set; }
        List<Member> listOfMembers;
        public Chat Chat { get; protected set; }
        public Course Course { get; protected set; }
        public bool IsActive { get; protected set; }
    }
}
