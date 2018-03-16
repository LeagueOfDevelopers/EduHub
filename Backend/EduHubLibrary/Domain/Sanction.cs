﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain
{
    public class Sanction
    {
        public Sanction(string brokenRule, int userId, int moderatorId, SanctionType type)
        {
            BrokenRule = brokenRule;
            UserId = userId;
            ModeratorId = moderatorId;
            Type = type;
            IsActive = true;
            IsTemporary = false;
        }

        public Sanction(string brokenRule, int userId, int moderatorId, SanctionType type, DateTimeOffset expirationDate)
        {
            BrokenRule = brokenRule;
            UserId = userId;
            ModeratorId = moderatorId;
            Type = type;
            IsActive = true;
            IsTemporary = true;
            ExpirationDate = expirationDate;
        }

        public int Id { get; internal set; }
        public string BrokenRule { get; }
        public int UserId { get; }
        public int ModeratorId { get; }
        public bool IsTemporary { get; }
        public DateTimeOffset ExpirationDate { get; }
        public SanctionType Type { get; }
        public bool IsActive { get; private set; }

        public void Cancel()
        {
            IsActive = false;
        }
    }
}
