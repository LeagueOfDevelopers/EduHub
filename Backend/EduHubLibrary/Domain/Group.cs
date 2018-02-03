using System;
using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Domain.Exceptions;
using EnsureThat;
using EduHubLibrary.Domain.Tools;
using EduHubLibrary.Domain.NotificationService;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("EduHubTests")]

namespace EduHubLibrary.Domain
{
    public class Group
    {
        public Chat Chat { get; private set; }
        public GroupInfo GroupInfo { get; set; }
        public User Teacher { get; private set; }
        public CourseStatus Status { get; set; }
        
        public Group(Guid creatorId, List<Member> toWrite, string title, List<string> tags,
            string description, int size, double moneyPerUser, bool isPrivate, GroupType groupType)
        {
            Ensure.Any.IsNotNull(tags);
            Ensure.String.IsNotNullOrWhiteSpace(title);
            Ensure.String.IsNotNullOrWhiteSpace(description);
            Ensure.Any.IsNotNull(size);
            Ensure.Any.IsNotNull(groupType);
            Ensure.Any.IsNotNull(moneyPerUser);
            bool isActive = true;
            GroupInfo = new GroupInfo(Guid.NewGuid(), title, description, tags, groupType, isPrivate, isActive, size, moneyPerUser);
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
            bool isActive = true;
            GroupInfo = new GroupInfo(Guid.NewGuid(), title, description, tags, groupType, isPrivate, isActive, size, moneyPerUser);
            listOfMembers = new List<Member>();
            listOfInvitations = new List<Invitation>();
            var creator = new Member(creatorId, MemberRole.Creator);
            listOfMembers.Add(creator);
        }

        internal void AddMember(Guid newMemberId)
        {
            Ensure.Bool.IsFalse(IsMember(newMemberId), nameof(AddMember),
                opt => opt.WithException(new AlreadyMemberException(newMemberId, GroupInfo.Id)));
            Ensure.Bool.IsTrue(listOfMembers.Count < GroupInfo.Size, nameof(GroupInfo.Size),
                opt => opt.WithException(new GroupIsFullException(GroupInfo.Id)));
            var newMember = new Member(newMemberId, MemberRole.Member);
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
                Member current = GetMemberById(requestedPerson);
                Ensure.Bool.IsTrue(current.MemberRole == MemberRole.Creator, nameof(DeleteCreator),
                    opt => opt.WithException(new NotEnoughPermissionsException(requestedPerson)));
                Teacher = null;
            }
        }

        internal bool IsMember(Guid userId)
        {
            return listOfMembers.FirstOrDefault(current => current.UserId == userId) != null;
        }

        internal bool IsTeacher(Guid userId)
        {
            return Teacher.Id  == userId;
        }

        internal Member GetMemberById(Guid userId)
        {
            return listOfMembers.FirstOrDefault(current => current.UserId == userId);
        }

        internal IEnumerable<Member> GetAllMembers()
        {
            return listOfMembers;
        }

        internal IEnumerable<Invitation> GetAllInvitation()
        {
            return listOfInvitations; 
        }

        internal void AddInvitation(Invitation invitation)
        {
            listOfInvitations.Add(invitation);
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
            Member currentMember = GetMemberById(userId);
            currentMember.AcceptedCurriculum = true;
            if (listOfMembers.All(m => m.AcceptedCurriculum))
            {
                Status = CourseStatus.Started;
            }
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
                GroupInfo.IsActive = false;
                return;
            }
            Member newCreator = listOfMembers[indexOfCreator + 1];
            newCreator.MemberRole = MemberRole.Creator;
            listOfMembers.Remove(deletingCreator);
        }
        
        private List<Member> listOfMembers;
        private List<Invitation> listOfInvitations;
    }
}
