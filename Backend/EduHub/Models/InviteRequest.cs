using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models
{
    public class InviteRequest
    {
        /// <summary>Inviting User's id</summary>
        public Guid InvitedId { get; set; }
        /// <summary>Proposed role</summary>
        public MemberRole Role { get; set; }
    }
}
