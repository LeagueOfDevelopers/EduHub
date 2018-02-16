using System;

namespace EduHub.Models
{
    public class SearchUserForInvitationRequest
    {
        public Guid GroupId { get; set; }
        public string Username { get; set; }
    }
}