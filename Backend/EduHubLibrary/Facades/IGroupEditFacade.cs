using System.Collections.Generic;
using EduHubLibrary.Domain;

namespace EduHubLibrary.Facades
{
    public interface IGroupEditFacade
    {
        void ChangeGroupTitle(int groupId, int changerId, string newTitle);
        void ChangeGroupDescription(int groupId, int changerId, string newDescription);
        void ChangeGroupTags(int groupId, int changerId, List<string> newTags);
        void ChangeGroupSize(int groupId, int changerId, int newSize);
        void ChangeGroupPrice(int groupId, int changerId, double newPrice);
        void ChangeGroupPrivacy(int groupId, int changerId, bool privacy);
        void ChangeGroupType(int groupId, int changerId, GroupType newType);
    }
}