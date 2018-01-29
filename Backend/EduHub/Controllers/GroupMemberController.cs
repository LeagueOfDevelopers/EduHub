using System;
using Microsoft.AspNetCore.Mvc;
using EduHub.Models;
using EduHubLibrary.Facades;
using EduHubLibrary.Domain;
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
        [Route("invitation/{invitedId}")]
        public IActionResult InviteUser([FromRoute] Guid invitedId, [FromRoute] Guid groupId)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            _userFacade.Invite(userId, invitedId, groupId, MemberRole.Member);
            return Ok("Пользователь приглашен");
        }

        /// <summary>
        /// Adds user to group as member
        /// </summary>
        [HttpPost]
        public IActionResult AddMember([FromRoute] Guid groupId)
        {
            string a = Request.Headers["Authorization"];
            var requestedId = a.GetUserId();
            _groupFacade.AddMember(groupId, requestedId);
            return Ok();
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
            _userFacade.ChangeInvitationStatus(userId, changer.InvitationId, changer.Status);
            return Ok("Приглашение принято");
        }

        /// <summary>
        /// Deletes member from group
        /// </summary>
        [HttpDelete]
        [Route("{memberId}")]
        public IActionResult DeleteMember([FromRoute] Guid groupId, [FromRoute] Guid memberId)
        {
            string a = Request.Headers["Authorization"];
            var requestedId = a.GetUserId();
            _groupFacade.DeleteMember(groupId, requestedId, memberId);
            return Ok();
        }

        /// <summary>
        /// Deletes teacher from group
        /// </summary>
        [HttpDelete]
        [Route("teacher/{memberId}")]
        public IActionResult DeleteTeacher([FromRoute] Guid groupId, [FromRoute] Guid memberId)
        {
            string a = Request.Headers["Authorization"];
            var requestedId = a.GetUserId();
            _groupFacade.DeleteTeacher(groupId, requestedId, memberId);
            return Ok();
        }

        public GroupMemberController(IUserFacade userFacade, IGroupFacade groupFacade)
        {
            _userFacade = userFacade;
            _groupFacade = groupFacade;
        }

        private readonly IUserFacade _userFacade;
        private readonly IGroupFacade _groupFacade;

    }
}