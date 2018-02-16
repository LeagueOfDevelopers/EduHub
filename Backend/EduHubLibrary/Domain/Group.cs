using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Domain.Tools;
using EnsureThat;

[assembly: InternalsVisibleTo("EduHubTests")]

namespace EduHubLibrary.Domain
{
    public class Group
    {
        public Group(Guid creatorId, List<Member> toWrite, string title, List<string> tags,
            string description, int size, double moneyPerUser, bool isPrivate, GroupType groupType)
        {
            Ensure.Any.IsNotNull(tags);
            Ensure.String.IsNotNullOrWhiteSpace(title);
            Ensure.String.IsNotNullOrWhiteSpace(description);
            Ensure.Any.IsNotNull(size);
            Ensure.Any.IsNotNull(groupType);
            Ensure.Any.IsNotNull(moneyPerUser);

            var isActive = true;
            GroupInfo = new GroupInfo(Guid.NewGuid(), title, description, tags, groupType, isPrivate, isActive, size,
                moneyPerUser);
            Chat = new Chat();
        }

        public Group(Guid creatorId, string title, List<string> tags,
            string description, int size, double moneyPerUser, bool isPrivate, GroupType groupType)
        {
            Ensure.Any.IsNotNull(tags);
            Ensure.String.IsNotNullOrWhiteSpace(title);
            Ensure.String.IsNotNullOrWhiteSpace(description);
            Ensure.Any.IsNotNull(size);
            Ensure.Any.IsNotNull(groupType);
            Ensure.Any.IsNotNull(moneyPerUser);

            var isActive = true;
            GroupInfo = new GroupInfo(Guid.NewGuid(), title, description, tags, groupType, isPrivate, isActive, size,
                moneyPerUser);
            Members = new List<Member>();
            Invitations = new List<Invitation>();
            var creator = new Member(creatorId, MemberRole.Creator);
            Members.Add(creator);
            Chat = new Chat();
        }

        public Chat Chat { get; }
        public GroupInfo GroupInfo { get; set; }
        public User Teacher { get; private set; }
        public CourseStatus Status { get; set; }
        public List<Member> Members { get; }
        public List<Invitation> Invitations { get; }

        internal void AddMember(Guid newMemberId)
        {
            Ensure.Bool.IsFalse(IsMember(newMemberId), nameof(AddMember),
                opt => opt.WithException(new AlreadyMemberException(newMemberId, GroupInfo.Id)));
            Ensure.Bool.IsTrue(Members.Count < GroupInfo.Size, nameof(GroupInfo.Size),
                opt => opt.WithException(new GroupIsFullException(GroupInfo.Id)));

            var newMember = new Member(newMemberId, MemberRole.Member);
            Members.Add(newMember);
        }

        internal void DeleteMember(Guid requestedPerson, Guid deletingPerson)
        {
            var deletingMember = GetMember(Ensure.Guid.IsNotEmpty(deletingPerson));
            var requestedMember = GetMember(Ensure.Guid.IsNotEmpty(requestedPerson));
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

            Members.Remove(deletingMember);
        }

        internal void DeleteTeacher(Guid requestedPerson, Guid teacherId)
        {
            Ensure.Guid.IsNotEmpty(requestedPerson);
            Ensure.Guid.IsNotEmpty(teacherId);
            Ensure.Bool.IsTrue(IsTeacher(teacherId), nameof(DeleteTeacher),
                opt => opt.WithException(new InvalidOperationException()));
            if (requestedPerson == teacherId)
            {
                Teacher = null;
            }
            else
            {
                Ensure.Bool.IsTrue(IsMember(requestedPerson), nameof(DeleteTeacher),
                    opt => opt.WithException(new MemberNotFoundException(requestedPerson)));
                var current = GetMember(requestedPerson);
                Ensure.Bool.IsTrue(current.MemberRole == MemberRole.Creator, nameof(DeleteCreator),
                    opt => opt.WithException(new NotEnoughPermissionsException(requestedPerson)));
                Teacher = null;
            }
        }

        internal bool IsMember(Guid userId)
        {
            return Members.FirstOrDefault(current => current.UserId == userId) != null;
        }

        internal bool IsTeacher(Guid userId)
        {
            return Teacher.Id == userId;
        }

        internal Member GetMember(Guid userId)
        {
            return Ensure.Any.IsNotNull(Members.Find(current => current.UserId == userId), nameof(GetMember),
                opt => opt.WithException(new MemberNotFoundException(userId)));
        }

        internal void AddInvitation(Invitation invitation)
        {
            Invitations.Add(invitation);
        }

        internal void ApproveTeacher(User teacher)
        {
            Ensure.Bool.IsTrue(Teacher == null, nameof(Teacher),
                opt => opt.WithException(new TeacherIsAlreadyFoundException()));
            /*Ensure.Bool.IsTrue(listOfMembers.Count == GroupInfo.Size, nameof(GroupInfo.Size),
                opt => opt.WithException(new GroupIsNotFullException(GroupInfo.Id)));*/
            // because of waffle task 103 this line is commented
            Teacher = Ensure.Any.IsNotNull(teacher);
        }

        internal void OfferCurriculum(Guid userId, string curriculum)
        {
            Ensure.Guid.IsNotEmpty(userId);
            Ensure.Bool.IsTrue(IsTeacher(userId), nameof(OfferCurriculum),
                opt => opt.WithException(new NotEnoughPermissionsException(userId)));
            Ensure.String.IsNotNullOrWhiteSpace(curriculum);
            GroupInfo.Curriculum = curriculum;
        }

        internal void AcceptCurriculum(Guid userId)
        {
            Ensure.Guid.IsNotEmpty(userId);
            Ensure.Bool.IsTrue(IsMember(userId), nameof(IsMember),
                opt => opt.WithException(new MemberNotFoundException(userId)));
            var currentMember = GetMember(userId);
            currentMember.AcceptedCurriculum = true;
            if (Members.All(m => m.AcceptedCurriculum)) Status = CourseStatus.Started;
        }

        internal bool DoesContainsTags(List<string> tags)
        {
            if (tags.TrueForAll(t => GroupInfo.Tags.Contains(t)))
                return true;

            return false;
        }

        private void DeleteCreator(Member deletingCreator)
        {
            Ensure.Any.IsNotNull(deletingCreator);
            Ensure.Bool.IsTrue(deletingCreator.MemberRole == MemberRole.Creator, nameof(DeleteCreator),
                opt => opt.WithException(new NotEnoughPermissionsException(deletingCreator.UserId)));
            var indexOfCreator = Members.IndexOf(deletingCreator);
            if (indexOfCreator + 1 == Members.Count)
            {
                Members.Remove(deletingCreator);
                GroupInfo.IsActive = false;
                return;
            }

            var newCreator = Members[indexOfCreator + 1];
            newCreator.MemberRole = MemberRole.Creator;
            Members.Remove(deletingCreator);
        }
    }
}