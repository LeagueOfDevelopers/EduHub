using System;
using EduHub.Extensions;
using EduHub.Models;
using EduHubLibrary.Facades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EduHub.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/group/{groupId}/member")]
    public class GroupMemberController : Controller
    {
        private readonly IGroupFacade _groupFacade;

        private readonly IUserFacade _userFacade;

        public GroupMemberController(IUserFacade userFacade, IGroupFacade groupFacade)
        {
            _userFacade = userFacade;
            _groupFacade = groupFacade;
        }

        /// <summary>
        ///     Invites user to group
        /// </summary>
        [Authorize]
        [HttpPost]
        [Route("invitation")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult Invite([FromRoute] Guid groupId, [FromBody] InviteRequest request)
        {
            var userId = Request.GetUserId();
            _userFacade.Invite(userId, request.InvitedId, groupId, request.Role);
            return Ok($"Пользователь {request.InvitedId} приглашен на роль {request.Role}");
        }

        /// <summary>
        ///     Adds user to group as member
        /// </summary>
        /// ]
        [Authorize]
        [HttpPost]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult AddMember([FromRoute] Guid groupId)
        {
            var requestedId = Request.GetUserId();
            _groupFacade.AddMember(groupId, requestedId);
            return Ok();
        }

        /// <summary>
        ///     Deletes member from group
        /// </summary>
        [Authorize]
        [HttpDelete]
        [Route("{memberId}")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult DeleteMember([FromRoute] Guid groupId, [FromRoute] Guid memberId)
        {
            var requestedId = Request.GetUserId();
            _groupFacade.DeleteMember(groupId, requestedId, memberId);
            return Ok();
        }

        /// <summary>
        ///     Deletes teacher from group
        /// </summary>
        [Authorize]
        [HttpDelete]
        [Route("teacher/{memberId}")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult DeleteTeacher([FromRoute] Guid groupId, [FromRoute] Guid memberId)
        {
            var requestedId = Request.GetUserId();
            _groupFacade.DeleteTeacher(groupId, requestedId, memberId);
            return Ok();
        }
    }
}