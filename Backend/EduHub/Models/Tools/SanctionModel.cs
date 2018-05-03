using System;
using EduHubLibrary.Domain;

namespace EduHub.Models.Tools
{
    public class SanctionModel
    {
        public SanctionModel(string brokenRule, int userId, string userName, int moderatorId, bool isTemporary,
            DateTimeOffset expirationDate, SanctionType type, bool isActive)
        {
            BrokenRule = brokenRule;
            UserId = userId;
            UserName = userName;
            ModeratorId = moderatorId;
            IsTemporary = isTemporary;
            ExpirationDate = expirationDate;
            Type = type;
            IsActive = isActive;
        }

        public string BrokenRule { get; }
        public int UserId { get; }
        public string UserName { get; }
        public int ModeratorId { get; }
        public bool IsTemporary { get; }
        public DateTimeOffset ExpirationDate { get; }
        public SanctionType Type { get; }
        public bool IsActive { get; }
    }
}