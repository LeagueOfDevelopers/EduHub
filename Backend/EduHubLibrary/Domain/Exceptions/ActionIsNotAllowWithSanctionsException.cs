﻿using System;
using System.Runtime.Serialization;

namespace EduHubLibrary.Domain.Exceptions
{
    public class ActionIsNotAllowWithSanctionsException : Exception
    {
        public ActionIsNotAllowWithSanctionsException(SanctionType sanctionType)
            : base($"Sanctions forbid this action, type of sanctionn : {sanctionType}")
        {
        }

        public ActionIsNotAllowWithSanctionsException(string message) : base(message)
        {
        }

        public ActionIsNotAllowWithSanctionsException(string message, Exception innerException) : base(message,
            innerException)
        {
        }

        protected ActionIsNotAllowWithSanctionsException(SerializationInfo info, StreamingContext context) : base(info,
            context)
        {
        }
    }
}