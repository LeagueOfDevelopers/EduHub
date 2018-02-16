using System;
using System.Collections.Generic;
using EduHubLibrary.Domain;

namespace EduHubLibrary.Facades
{
    public interface IGroupEditFacade
    {
        void ChangeGroupTitle(Guid groupId, Guid changerId, string newTitle);
        void ChangeGroupDescription(Guid groupId, Guid changerId, string newDescription);
        void ChangeGroupTags(Guid groupId, Guid changerId, List<string> newTags);
        void ChangeGroupSize(Guid groupId, Guid changerId, int newSize);
        void ChangeGroupPrice(Guid groupId, Guid changerId, double newPrice);
        void ChangeGroupPrivacy(Guid groupId, Guid changerId, bool privacy);
        void ChangeGroupType(Guid groupId, Guid changerId, GroupType newType);
    }
}