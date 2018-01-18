using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EduHub.Models;
using EduHubLibrary.Facades;
using Swashbuckle;
using Swashbuckle.AspNetCore.SwaggerGen;
using EduHubLibrary.Domain;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using EduHub.Extensions;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/group/{groupId}/member")]
    public class GroupMemberController : Controller
    {
        /// <summary>
        /// Invites user to group
        /// </summary>
        [Authorize]
        [HttpPost]
        [Route("invite/{invitedId}")]
        public IActionResult InviteUser([FromRoute] Guid invitedId, [FromRoute] Guid groupId)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            _userFacade.Invite(userId, invitedId, groupId, MemberRole.Member);
            return Ok("Пользователь приглашен");
        }

        /// <summary>
        /// Changes status of invitation, add user to group
        /// </summary>
        [Authorize]
        [HttpPut]
        public IActionResult ChangeStatusOfInvitation([FromBody] ChangeStatusOfInvitationRequest changer)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            _userFacade.ChangeStatusOfInvitation(userId, changer.InvitationId, changer.Status);
            return Ok("Приглашение принято");
        }

        /// <summary>
        /// Deletes member from group
        /// </summary>
        [HttpDelete]
        [Route("{idOfUser}")]
        public IActionResult DeleteMember([FromRoute] int idOfGroup)
        {
            return Ok("Пользователь удален");
        }

        public GroupMemberController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        private readonly IUserFacade _userFacade;

    }
}