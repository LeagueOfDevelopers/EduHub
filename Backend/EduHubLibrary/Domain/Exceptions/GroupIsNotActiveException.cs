﻿using System;
using System.Runtime.Serialization;

namespace EduHubLibrary.Domain.Exceptions
{
    public class GroupIsNotActiveException : Exception
    {
        public GroupIsNotActiveException(int groupId) : base($"Grouup with id {groupId} is not active")
        {
        }

        public GroupIsNotActiveException(string message) : base(message)
        {
        }

        public GroupIsNotActiveException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GroupIsNotActiveException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}