using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EduHubLibrary.Domain.Exceptions;

namespace EduHubLibrary.Domain
{
    public class Group
    {
        public void AddMember(Guid inviterId, Guid invitedId)
        {
            if (!IsMember(inviterId))
                throw new MemberNotFoundException(inviterId);
            var newMember = new Member(invitedId, MemberRole.Member);
        }

        public void DeleteMember(Guid requestedPerson, Guid deletingPerson)
        {
            if(!IsMember(requestedPerson))
                throw new MemberNotFoundException(requestedPerson);
            if (!IsMember(deletingPerson))
                throw new MemberNotFoundException(deletingPerson);
            if (requestedPerson != deletingPerson && GetMemberById(requestedPerson).MemberRole != MemberRole.Creator)
                throw new NotEnoughPermissionsException(requestedPerson);
            listOfMembers.Remove(GetMemberById(deletingPerson));
        }

        public bool IsMember(Guid userId)
        {
            return listOfMembers.FirstOrDefault(current => current.UserId == userId) != null;
        }

        private Member GetMemberById(Guid userId)
        {
            return listOfMembers.FirstOrDefault(current => current.UserId == userId);
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
        public Chat Chat { get; private set; }
        public Course Course { get; private set; }
    }
}
