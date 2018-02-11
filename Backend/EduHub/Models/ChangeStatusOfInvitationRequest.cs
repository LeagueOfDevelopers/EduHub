﻿using System;
using System.ComponentModel.DataAnnotations;
using EduHubLibrary.Domain;

namespace EduHub.Models
{
    public class ChangeStatusOfInvitationRequest
    {
        [Required] public Guid InvitationId { get; set; }

        /// <summary>New invitation status</summary>
        [Required]
        public InvitationStatus Status { get; set; }
    }
}