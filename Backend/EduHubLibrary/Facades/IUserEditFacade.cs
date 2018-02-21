using System;
using System.Collections.Generic;
using EduHubLibrary.Domain.Tools;

namespace EduHubLibrary.Facades
{
    public interface IUserEditFacade
    {
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