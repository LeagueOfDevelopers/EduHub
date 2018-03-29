using EduHubLibrary.Data.SanctionDtos;
using EduHubLibrary.Domain;

namespace EduHubLibrary.Extensions
{
    public static class SanctionDtoExtenstions
    {
        public static void ParseFromSanction(this SanctionDto result, Sanction sourse)
        {
            result.BrokenRule = sourse.BrokenRule;
            result.UserId = sourse.UserId;
            result.ModeratorId = sourse.ModeratorId;
            result.IsTemporary = sourse.IsTemporary;
            result.ExpirationDate = sourse.ExpirationDate;
            result.Type = sourse.Type;
            result.IsActive = sourse.IsActive;
        }
    }
}