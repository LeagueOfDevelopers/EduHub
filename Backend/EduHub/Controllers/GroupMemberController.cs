using System;
using Microsoft.AspNetCore.Mvc;
using EduHub.Models;
using EduHubLibrary.Facades;
using EduHubLibrary.Domain;
using Microsoft.AspNetCore.Authorization;
using EduHub.Extensions;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EduHub.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/group/{groupId}/member")]
    public class GroupMemberController : Controller
    {
        public GroupMemberController(IUserFacade userFacade, IGroupFacade groupFacade)
        {
            _userFacade = userFacade;
            _groupFacade = groupFacade;
        }

        /// <summary>
        /// Invites user to group
        /// </summary>
        [HttpPost]
        [Route("invitation")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult Invite([FromRoute] Guid groupId, [FromBody]InviteRequest request)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            _userFacade.Invite(userId, request.InvitedId, groupId, request.Role);
            return Ok($"Пользователь {request.InvitedId} приглашен на роль {request.Role}");
        }

        /// <summary>
        /// Adds user to group as member
        /// </summary>]
        [HttpPost]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
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
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
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
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult DeleteTeacher([FromRoute] Guid groupId, [FromRoute] Guid memberId)
        {
            string a = Request.Headers["Authorization"];
            var requestedId = a.GetUserId();
            _groupFacade.DeleteTeacher(groupId, requestedId, memberId);
            return Ok();
        }

        private readonly IUserFacade _userFacade;
        private readonly IGroupFacade _groupFacade;
    }
}