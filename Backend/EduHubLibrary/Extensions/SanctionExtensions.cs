using EduHubLibrary.Data.SanctionDtos;
using EduHubLibrary.Domain;

namespace EduHubLibrary.Extensions
{
    public static class SanctionExtensions
    {
        public static Sanction ParseFromSanctionDto(SanctionDto source)
        {
            return new Sanction(source.Id, source.BrokenRule, source.UserId,
                source.ModeratorId, source.IsTemporary, source.ExpirationDate, source.Type, source.IsActive);
        }
    }
}