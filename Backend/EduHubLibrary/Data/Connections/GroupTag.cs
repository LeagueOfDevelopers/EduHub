using EduHubLibrary.Data.GroupDtos;
using EduHubLibrary.Data.TagDtos;

namespace EduHubLibrary.Data.Connections
{
    public class GroupTag
    {
        public int Id { get; set; }

        public string TagId { get; set; }
        public TagDto Tag { get; set; }

        public int GroupId { get; set; }
        public GroupDto Group { get; set; }

        public GroupTag(string tagId, TagDto tag, int groupId, GroupDto group)
        {
            TagId = tagId;
            Tag = tag;
            GroupId = groupId;
            Group = group;
        }

        public GroupTag()
        {
        }
    }
}
