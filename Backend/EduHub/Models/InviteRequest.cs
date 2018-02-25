using System;
using EduHubLibrary.Domain;

namespace EduHub.Models
{
    public class InviteRequest
    {
        /// <summary>Inviting User's id</summary>
        public Guid InvitedId { get; set; }
    }
}