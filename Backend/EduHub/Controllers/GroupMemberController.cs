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
        [Route("invitation")]
        public IActionResult Invite([FromRoute] Guid groupId, [FromBody]InviteRequest request)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            _userFacade.Invite(userId, request.InvitedId, groupId, request.Role);
            return Ok($"Пользователь {request.InvitedId} приглашен на роль {request.Role}");
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