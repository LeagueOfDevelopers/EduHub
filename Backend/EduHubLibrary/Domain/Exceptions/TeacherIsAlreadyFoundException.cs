﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EduHubLibrary.Domain.Exceptions
{
    public class TeacherIsAlreadyFoundException : Exception
    {
        public TeacherIsAlreadyFoundException() : base("Teacher is already approved")
        {
        }

        public TeacherIsAlreadyFoundException(string message) : base(message)
        {
        }

        public TeacherIsAlreadyFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TeacherIsAlreadyFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
