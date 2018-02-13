﻿using System;
using System.Collections.Generic;
using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Tools;

namespace EduHubLibrary.Facades
{
    public interface IUserFacade
    {
        Guid RegUser(string username, Credentials credentials, bool IsTeacher, UserType type);
        IEnumerable<User> GetUsers();
        User GetUser(Guid id);
        User FindByCredentials(Credentials credentials);
        void ChangeInvitationStatus(Guid userId, Guid invitationId, InvitationStatus status);
        void Invite(Guid inviterId, Guid invitedId, Guid groupId, MemberRole suggestedRole);
        IEnumerable<Invitation> GetAllInvitationsForUser(Guid userId);
        IEnumerable<Group> GetAllGroupsOfUser(Guid userId);
        bool DoesUserExist(string name);
        IEnumerable<User> FindByName(string name);
        IEnumerable<string> GetNotifies(Guid userId);
        void AddNotify(Guid userId, string notify);
        void EditName(Guid userId, string newName);
        void EditAboutUser(Guid userId, string newAboutUser);
        void EditGender(Guid userId, Gender gender);
        void EditAvatarLink(Guid userId, string newAvatarLink);
        void EditContacts(Guid userId, List<string> newContactData);
        void EditBirthYear(Guid userId, int newYear);
        void BecomeTeacher(Guid userId);
        void StopToBeTeacher(Guid userId);
    }
}