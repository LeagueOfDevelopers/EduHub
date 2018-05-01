using System.Collections.Generic;

namespace EduHubLibrary.Facades.Views.GroupViews
{
    public class FullGroupView
    {
        public FullGroupView(GroupInfoView groupInfoView, IEnumerable<GroupMemberInfoView> groupMemberInfo,
            IEnumerable<ReviewView> reviewView)
        {
            GroupInfoView = groupInfoView;
            GroupMemberInfo = groupMemberInfo;
            ReviewView = reviewView;
        }

        public GroupInfoView GroupInfoView { get; }
        public IEnumerable<GroupMemberInfoView> GroupMemberInfo { get; }
        public IEnumerable<ReviewView> ReviewView { get; }
    }
}