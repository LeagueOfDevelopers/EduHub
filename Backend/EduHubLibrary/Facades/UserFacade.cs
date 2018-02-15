﻿using System;
using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Domain.Tools;
using EnsureThat;

namespace EduHubLibrary.Facades
{
    public class UserFacade : IUserFacade
    {
        private readonly IGroupRepository _groupRepository;

        private readonly IUserRepository _userRepository;

        public UserFacade(IUserRepository userRepository, IGroupRepository groupRepository)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
        }

        public User GetUser(Guid id)
        {
            return _userRepository.GetUserById(id);
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetAll();
        }

        public Guid RegUser(string username, Credentials credentials, bool IsTeacher, UserType type)
        {
            Ensure.Bool.IsFalse(_userRepository.GetAll().Any(u => u.Credentials.Email.Equals(credentials.Email)),
                nameof(RegUser), opt => opt.WithException(new UserAlreadyExistsException(credentials.Email)));
            var user = new User(username, credentials, IsTeacher, type);
            _userRepository.Add(user);
            return user.Id;
        }

        public User FindByCredentials(Credentials credentials)
        {
            return _userRepository.GetUserByCredentials(credentials);
        }

        public void ChangeInvitationStatus(Guid userId, Guid invitationId, InvitationStatus status)
        {
            Ensure.Guid.IsNotEmpty(userId);
            Ensure.Guid.IsNotEmpty(invitationId);
            var currentUser = _userRepository.GetUserById(userId);
            if (status.Equals(InvitationStatus.Accepted))
            {
                currentUser.AcceptInvitation(invitationId);
                var currentInvitation = currentUser.GetInvitationById(invitationId);
                if (currentInvitation.SuggestedRole == MemberRole.Member)
                {
                    var currentGroup = _groupRepository.GetGroupById(currentInvitation.GroupId);
                    currentGroup.AddMember(currentInvitation.ToUser);
                }
            }
            else if (status.Equals(InvitationStatus.Declined))
            {
                currentUser.DeclineInvitation(invitationId);
            }
        }

        public void Invite(Guid inviterId, Guid invitedId, Guid groupId, MemberRole suggestedRole)
        {
            Ensure.Guid.IsNotEmpty(inviterId);
            Ensure.Guid.IsNotEmpty(invitedId);
            Ensure.Guid.IsNotEmpty(groupId);
            var currentGroup = _groupRepository.GetGroupById(groupId);
            var invitedUser = _userRepository.GetUserById(invitedId);

            Ensure.Bool.IsTrue(currentGroup.IsMember(inviterId), nameof(Invite),
                opt => opt.WithException(new NotEnoughPermissionsException(inviterId)));
            Ensure.Bool.IsFalse(currentGroup.IsMember(invitedId), nameof(Invite),
                opt => opt.WithException(new AlreadyMemberException(invitedId, groupId)));
            Ensure.Bool.IsFalse(invitedUser.GetAllInvitation().Any(c => c.GroupId == groupId),
                nameof(Invite), opt => opt.WithException(new AlreadyInvitedException(invitedId, groupId)));
            if (suggestedRole == MemberRole.Teacher)
                Ensure.Bool.IsTrue(invitedUser.UserProfile.IsTeacher, nameof(Invite),
                    opt => opt.WithException(new UserIsNotTeacher(invitedId)));
            Ensure.Bool.IsFalse(
                suggestedRole == MemberRole.Teacher && _groupRepository.GetGroupById(groupId).Teacher != null,
                nameof(Invite), opt => opt.WithException(new TeacherIsAlreadyFoundException()));
            var newInvintation =
                new Invitation(inviterId, invitedId, groupId, suggestedRole, InvitationStatus.InProgress);

            invitedUser.AddInvitation(newInvintation);
        }

        public IEnumerable<Invitation> GetAllInvitationsForUser(Guid userId)
        {
            Ensure.Guid.IsNotEmpty(userId);
            var currentUser = _userRepository.GetUserById(userId);
            return currentUser.GetAllInvitation();
        }

        public IEnumerable<Group> GetAllGroupsOfUser(Guid userId)
        {
            var groupsOfUser = new List<Group>();

            foreach (var group in _groupRepository.GetAll())
                if (group.Members.Any(member => member.UserId.Equals(userId)))
                    groupsOfUser.Add(group);

            return groupsOfUser;
        }

        public IEnumerable<User> FindByName(string name)
        {
            var result = _userRepository.GetAll().ToList().FindAll(u => u.UserProfile.Name.Contains(name));

            return result.OrderBy(u => u.UserProfile.Name.Length);
        }

        public IEnumerable<string> GetNotifies(Guid userId)
        {
            return _userRepository.GetUserById(userId).Notifies;
        }

        public void AddNotify(Guid userId, string notify)
        {
            _userRepository.GetUserById(userId).AddNotify(notify);
        }

        #region Edit Profile Data Methods

        public void EditName(Guid userId, string newName)
        {
            var currentUser = _userRepository.GetUserById(userId);
            currentUser.UserProfile.Name = Ensure.String.IsNotNullOrWhiteSpace(newName);
            _userRepository.Update(currentUser);
        }

        public void EditAboutUser(Guid userId, string newAboutUser)
        {
            var currentUser = _userRepository.GetUserById(userId);
            currentUser.UserProfile.AboutUser = Ensure.String.IsNotNullOrWhiteSpace(newAboutUser);
            _userRepository.Update(currentUser);
        }

        public void EditGender(Guid userId, Gender gender)
        {
            var currentUser = _userRepository.GetUserById(userId);
            currentUser.UserProfile.Gender = gender;
            _userRepository.Update(currentUser);
        }

        public void EditAvatarLink(Guid userId, string newAvatarLink)
        {
            var currentUser = _userRepository.GetUserById(userId);
            currentUser.UserProfile.AvatarLink = Ensure.String.IsNotNullOrWhiteSpace(newAvatarLink);
            _userRepository.Update(currentUser);
        }

        public void EditContacts(Guid userId, List<string> newContactData)
        {
            var currentUser = _userRepository.GetUserById(userId);

            if (newContactData.Count != 0 && newContactData.TrueForAll(d => !string.IsNullOrWhiteSpace(d)))
                currentUser.UserProfile.Contacts = newContactData;
            else throw new ArgumentException();

            _userRepository.Update(currentUser);
        }

        public void EditBirthYear(Guid userId, int newYear)
        {
            var currentUser = _userRepository.GetUserById(userId);

            //hardcoded value
            if (newYear > 1900 && newYear < DateTimeOffset.Now.Year) currentUser.UserProfile.BirthYear = newYear;
            else throw new IndexOutOfRangeException();

            _userRepository.Update(currentUser);
        }

        public void BecomeTeacher(Guid userId)
        {
            var currentUser = _userRepository.GetUserById(userId);
            currentUser.UserProfile.IsTeacher = true;
            _userRepository.Update(currentUser);
        }

        public void StopToBeTeacher(Guid userId)
        {
            var currentUser = _userRepository.GetUserById(userId);
            currentUser.UserProfile.IsTeacher = false;
            _userRepository.Update(currentUser);
        }

        #endregion
    }
}