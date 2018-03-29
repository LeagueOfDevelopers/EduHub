using EduHubLibrary.Data.TagDtos;
using EduHubLibrary.Domain.Tools;

namespace EduHubLibrary.Extensions
{
    internal static class TagExtensions
    {
        internal static Tag ParseFromTagDto(TagDto tagDto)
        {
            return new Tag(tagDto.Name, tagDto.Popularity);
        }
    }
}