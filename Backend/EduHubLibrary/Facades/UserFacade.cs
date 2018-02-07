using System;
using System.Collections.Generic;
using EduHubLibrary.Domain;
using EnsureThat;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Common;
using System.Linq;
using EduHubLibrary.Domain.Tools;
using EduHubLibrary.Domain.NotificationService;

namespace EduHubLibrary.Facades
{
    public class UserFacade : IUserFacade
    {
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
            User user = new User(username, credentials, IsTeacher, type);
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
            User currentUser = _userRepository.GetUserById(userId);
            if (status.Equals(InvitationStatus.Accepted))
            {
                currentUser.AcceptInvitation(invitationId);
                Invitation currentInvitation = currentUser.GetInvitationById(invitationId);
                if (currentInvitation.SuggestedRole == MemberRole.Member)
                {
                    Group currentGroup = _groupRepository.GetGroupById(currentInvitation.GroupId);
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
            Group currentGroup = _groupRepository.GetGroupById(groupId);
            User invitedUser = _userRepository.GetUserById(invitedId);

            Ensure.Bool.IsTrue(currentGroup.IsMember(inviterId), nameof(Invite),
                opt => opt.WithException(new NotEnoughPermissionsException(inviterId)));
            Ensure.Bool.IsFalse(currentGroup.IsMember(invitedId), nameof(Invite),
                opt => opt.WithException(new AlreadyMemberException(invitedId, groupId)));
            Ensure.Bool.IsFalse(invitedUser.GetAllInvitation().Any(c => c.GroupId == groupId), 
                nameof(Invite), opt => opt.WithException(new AlreadyInvitedException(invitedId, groupId)));
            if (suggestedRole == MemberRole.Teacher)
            {
                Ensure.Bool.IsTrue(invitedUser.UserProfile.IsTeacher, nameof(Invite), 
                    opt => opt.WithException(new UserIsNotTeacher(invitedId)));
            }
            Ensure.Bool.IsFalse(suggestedRole == MemberRole.Teacher && _groupRepository.GetGroupById(groupId).Teacher != null,
                nameof(Invite), opt => opt.WithException(new TeacherIsAlreadyFoundException()));
            Invitation newInvintation = new Invitation(inviterId, invitedId, groupId, suggestedRole, InvitationStatus.InProgress);

            invitedUser.AddInvitation(newInvintation);
        }

        public IEnumerable<Invitation> GetAllInvitationsForUser(Guid userId)
        {
            Ensure.Guid.IsNotEmpty(userId);
            User currentUser = _userRepository.GetUserById(userId);
            return currentUser.GetAllInvitation();
        }

        public IEnumerable<Group> GetAllGroupsOfUser(Guid userId)
        {
            List<Group> groupsOfUser = new List<Group>();

            foreach (Group group in _groupRepository.GetAll())
            {
                if (group.GetAllMembers().Any(member => member.UserId.Equals(userId)))
                {
                    groupsOfUser.Add(group);
                }
            }

            return groupsOfUser;
        }

        public IEnumerable<User> FindByName(string name)
        {
            IEnumerable<User> result = _userRepository.GetAll().Where(u => u.UserProfile.Name.Contains(name));
            
            return result.OrderBy(u => u.UserProfile.Name.Length);
        }

        public bool DoesUserExist(string name)
        { 
            return _userRepository.GetAll().Any(user => user.UserProfile.Name.Contains(name));
        }

        public IEnumerable<string> GetNotifies(Guid userId)
        {
            return _userRepository.GetUserById(userId).GetNotifies();
        }

        public void AddNotify(Guid userId, string notify)
        {
            _userRepository.GetUserById(userId).AddNotify(notify);
        }

        #region Edit Profile Data Methods

        public void EditName(Guid userId, string newName)
        {
            _userRepository.GetUserById(userId).UserProfile.Name = Ensure.String.IsNotNullOrWhiteSpace(newName);
        }

        public void EditAboutUser(Guid userId, string newAboutUser)
        {
            _userRepository.GetUserById(userId).UserProfile.AboutUser = Ensure.String.IsNotNullOrWhiteSpace(newAboutUser);
        }

        public void EditGender(Guid userId, bool isMan)
        {
            _userRepository.GetUserById(userId).UserProfile.IsMan = isMan;
        }

        public void EditAvatarLink(Guid userId, string newAvatarLink)
        {
            _userRepository.GetUserById(userId).UserProfile.AvatarLink = Ensure.String.IsNotNullOrWhiteSpace(newAvatarLink);
        }

        public void EditContacts(Guid userId, List<string> newContactData)
        {
            if (newContactData.Count != 0)
            {
                _userRepository.GetUserById(userId).UserProfile.Contacts = newContactData;
            }
            else throw new System.ArgumentException();
        }

        public void EditBirthYear(Guid userId, string newYear)
        {
            string dataFormat = @"\d{4}";
            //System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(dataFormat);
            if (System.Text.RegularExpressions.Regex.IsMatch(newYear, dataFormat))
            {
                _userRepository.GetUserById(userId).UserProfile.BirthYear = newYear;
            }
            else throw new System.ArgumentException();
        }

        public void BecomeTeacher(Guid userId)
        {
            _userRepository.GetUserById(userId).UserProfile.IsTeacher = true;
        }

        public void StopToBeTeacher(Guid userId)
        {
            _userRepository.GetUserById(userId).UserProfile.IsTeacher = false;
        }

        #endregion

        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
    }
}
