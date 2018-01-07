using EduHubLibrary.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models
{
    public class ChangeStatusOfInvitationRequest
    {
        [Required]
        public Guid UserId {get; set;}
        [Required]
        public Guid InvitationId { get; set; }
        /// <summary>New invitation status</summary>
        [Required]
        public InvitationStatus Status { get; set; }
    }
}
