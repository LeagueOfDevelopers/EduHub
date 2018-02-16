using System;

namespace EduHubLibrary.Domain
{
    public class Key
    {
        public Key(Guid userId)
        {
            Value = Guid.NewGuid();
            Used = false;
            UserId = userId;
        }

        public bool Used { get; private set; }
        public Guid Value { get; }
        public Guid UserId { get; }

        public void UseKey()
        {
            Used = true;
        }
    }
}