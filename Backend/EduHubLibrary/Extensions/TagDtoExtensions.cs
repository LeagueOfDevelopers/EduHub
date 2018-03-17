using EduHubLibrary.Data.TagDtos;
using EduHubLibrary.Domain.Tools;

namespace EduHubLibrary.Extensions
{
    internal static class TagDtoExtensions
    {
        public static void ParseFromTag(this TagDto tagDto, Tag tag)
        {
            tagDto.Name = tagDto.Name;
            tagDto.Popularity = tag.Popularity;
        }
    }
}
