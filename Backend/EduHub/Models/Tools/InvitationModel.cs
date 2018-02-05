using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models.Tools
{
    public class InvitationModel
    {
        /// <summary>
        /// invitation id
        /// </summary>
        public Guid Id { get; set; }
        public Guid FromUser { get; set; }
        public string FromUserName { get; set; }
        public Guid ToUser { get; set; }
        public string ToUserName { get; set; }
        public Guid ToGroup { get; set; }
        public string ToGroupTitle { get; set; }
        public MemberRole SuggestedRole { get; set; }

        public InvitationModel(Guid id, Guid fromUser, string fromUserName, Guid toUser, 
            string toUserName, Guid toGroup, string toGroupTitle, MemberRole suggestedRole)
        {
            Id = id;
            FromUser = fromUser;
            FromUserName = fromUserName;
            ToUser = toUser;
            ToUserName = toUserName;
            ToGroup = toGroup;
            ToGroupTitle = toGroupTitle;
            SuggestedRole = suggestedRole;
        }
    }
}
