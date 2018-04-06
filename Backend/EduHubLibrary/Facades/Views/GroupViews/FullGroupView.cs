using System.Collections.Generic;

namespace EduHubLibrary.Facades.Views.GroupViews
{
    public class FullGroupView
    {
        public FullGroupView(GroupInfoView groupInfoView, IEnumerable<GroupMemberInfoView> groupMemberInfo,
            IEnumerable<MessageView> messageView, IEnumerable<ReviewView> reviewView)
        {
            GroupInfoView = groupInfoView;
            GroupMemberInfo = groupMemberInfo;
            MessageView = messageView;
            ReviewView = reviewView;
        }

        public GroupInfoView GroupInfoView { get; }
        public IEnumerable<GroupMemberInfoView> GroupMemberInfo { get; }
        public IEnumerable<MessageView> MessageView { get; }
        public IEnumerable<ReviewView> ReviewView { get; }
    }
}