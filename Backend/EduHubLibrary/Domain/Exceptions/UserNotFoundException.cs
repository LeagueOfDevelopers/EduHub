﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Exceptions
{
    class UserNotFoundException : System.Exception
    {
        public UserNotFoundException(Guid userId) 
            : base($"User with id {userId} not found")
        {
        }
        public UserNotFoundException()
        {
        }

        public UserNotFoundException(string message) : base(message)
        {
        }

        public UserNotFoundException(string message, System.Exception inner) : base(message, inner)
        {
        }

        protected UserNotFoundException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context)
        {
        }
    }
}