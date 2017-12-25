using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EduHubLibrary.Domain.Exceptions;
using EnsureThat;
using System.Resources;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("EduHubTests")]

namespace EduHubLibrary.Domain
{
    public class Group
    {
        internal void AddMember(Guid inviterId, Guid invitedId)
        {
            Ensure.Bool.IsTrue(IsMember(inviterId), nameof(IsMember), 
                opt => opt.WithException(new MemberNotFoundException(inviterId)));
            Ensure.Bool.IsTrue(listOfMembers.Count < Size, nameof(Size), 
                opt => opt.WithException(new GroupIsFullException(Id)));
            var newMember = new Member(invitedId, MemberRole.Member);
            listOfMembers.Add(newMember);
        }

        internal void DeleteMember(Guid requestedPerson, Guid deletingPerson)
        {
            Member deletingMember = GetMemberById(Ensure.Guid.IsNotEmpty(deletingPerson));
            Member requestedMember = GetMemberById(Ensure.Guid.IsNotEmpty(requestedPerson));
            Ensure.Bool.IsTrue(IsMember(requestedPerson), nameof(IsMember),
                opt => opt.WithException(new MemberNotFoundException(requestedPerson)));
            Ensure.Bool.IsTrue(IsMember(deletingPerson), nameof(IsMember),
                opt => opt.WithException(new MemberNotFoundException(deletingPerson)));
            Ensure.Bool.IsTrue(requestedPerson == deletingPerson || requestedMember.MemberRole == MemberRole.Creator,
                nameof(DeleteMember), opt => opt.WithException(new NotEnoughPermissionsException(requestedPerson)));
            if (deletingMember.MemberRole == MemberRole.Creator)
            {
                DeleteCreator(deletingMember);
                return;
            }
            listOfMembers.Remove(deletingMember);
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

        internal void ChangeSizeOfGroup(Guid idOfChanger, int newSize)
        {
            Member current = Ensure.Any.IsNotNull(GetMemberById(idOfChanger), nameof(GetMemberById),
                opt => opt.WithException(new MemberNotFoundException(idOfChanger)));
            Ensure.Bool.IsTrue(current.MemberRole == MemberRole.Creator, nameof(ChangeSizeOfGroup),
                opt => opt.WithException(new NotEnoughPermissionsException(idOfChanger)));
            //TODO validate size
            ResourceManager rm = new ResourceManager(typeof(int));
            rm.GetString("maxSizeOfGroup");
            Size = newSize;
        }

        private void DeleteCreator(Member deletingCreator)
        {
            Ensure.Any.IsNotNull(deletingCreator);
            Ensure.Bool.IsTrue(deletingCreator.MemberRole == MemberRole.Creator, nameof(DeleteCreator),
                opt => opt.WithException(new NotEnoughPermissionsException(deletingCreator.UserId)));
            int indexOfCreator = listOfMembers.IndexOf(deletingCreator);
            if (indexOfCreator + 1 == listOfMembers.Count)
            {
                listOfMembers.Remove(deletingCreator);
                IsActive = false;
                return;
            }
            Member newCreator = listOfMembers[indexOfCreator + 1];
            newCreator.MemberRole = MemberRole.Creator;
            listOfMembers.Remove(deletingCreator);
        }

        public Group(Guid creatorId, List<Member> toWrite, string title, List<string> tags,
            string description, int size, double totalValue)
        {
            Id = Ensure.Guid.IsNotEmpty(Guid.NewGuid());
            Tags = Ensure.Any.IsNotNull(tags);
            Title = Ensure.String.IsNotNullOrWhiteSpace(title);
            Description = Ensure.String.IsNotNullOrWhiteSpace(description);
            listOfMembers = Ensure.Any.IsNotNull(toWrite);
            
            var creator = new Member(creatorId, MemberRole.Creator);
            listOfMembers.Add(creator);
        }

        public Group(Guid creatorId, string title, List<string> tags, string description, int size, double totalValue)
        {
            Id = Ensure.Guid.IsNotEmpty(Guid.NewGuid());
            Tags = Ensure.Any.IsNotNull(tags);
            Title = Ensure.String.IsNotNullOrWhiteSpace(title);
            Description = Ensure.String.IsNotNullOrWhiteSpace(description);
            listOfMembers = new List<Member>();
            Size = Ensure.Any.IsNotNull(size);
            TotalValue = Ensure.Any.IsNotNull(totalValue);
            var creator = new Member(creatorId, MemberRole.Creator);
            listOfMembers.Add(creator);
        }
        public Guid Id { get; private set; }
        public string Description { get; private set; }
        public string Title { get; private set; }
        public List<string> Tags { get; private set; }
        public bool IsActive { get; private set; }
        public int Size { get; private set; } //size of group
        public double TotalValue { get; private set; } //whole amount money for teacher
        public Chat Chat { get; private set; }
        public Course Course { get; private set; }
        private List<Member> listOfMembers;

    }
}
