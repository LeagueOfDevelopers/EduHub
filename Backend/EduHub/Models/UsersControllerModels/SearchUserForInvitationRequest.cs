using System;

namespace EduHub.Models
{
    public class SearchUserForInvitationRequest
    {
        public int GroupId { get; set; }
        public string Username { get; set; }
    }
}