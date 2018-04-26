using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Facades.Views
{
    public class SanctionView
    {
        public SanctionView(int id, string brokenRule, int userId, string userName, int moderatorId, bool isTemporary, DateTimeOffset expirationDate, SanctionType type, bool isActive)
        {
            Id = id;
            BrokenRule = brokenRule;
            UserId = userId;
            UserName = userName;
            ModeratorId = moderatorId;
            IsTemporary = isTemporary;
            ExpirationDate = expirationDate;
            Type = type;
            IsActive = isActive;
        }

        public int Id { get; internal set; }
        public string BrokenRule { get; }
        public int UserId { get; }
        public string UserName { get; }
        public int ModeratorId { get; }
        public bool IsTemporary { get; }
        public DateTimeOffset ExpirationDate { get; }
        public SanctionType Type { get; }
        public bool IsActive { get; private set; }
    }
}
