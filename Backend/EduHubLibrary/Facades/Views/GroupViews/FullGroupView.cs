using System.Collections.Generic;

namespace EduHubLibrary.Facades.Views.GroupViews
{
    public class FullGroupView
    {
        public FullGroupView(GroupInfoView groupInfoView, IEnumerable<GroupMemberInfoView> groupMemberInfo,
            IEnumerable<MessageView> messageView)
        {
            GroupInfoView = groupInfoView;
            GroupMemberInfo = groupMemberInfo;
            MessageView = messageView;
        }

        public GroupInfoView GroupInfoView { get; }
        public IEnumerable<GroupMemberInfoView> GroupMemberInfo { get; }
        public IEnumerable<MessageView> MessageView { get; }
    }
}