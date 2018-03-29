using EduHub.Extensions;
using EduHub.Models;
using EduHubLibrary.Domain;
using EduHubLibrary.Facades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/group/{groupId}/teacher")]
    public class GroupTeacherController : Controller
    {
        private readonly IGroupFacade _groupFacade;

        private readonly IUserFacade _userFacade;

        public GroupTeacherController(IGroupFacade groupFacade, IUserFacade userFacade)
        {
            _groupFacade = groupFacade;
            _userFacade = userFacade;
        }

        /// <summary>
        ///     Deletes teacher from group
        /// </summary>
        [Authorize]
        [HttpDelete]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult DeleteTeacher([FromRoute] int groupId)
        {
            var requestedId = Request.GetUserId();
            _groupFacade.DeleteTeacher(groupId, requestedId);
            return Ok();
        }

        /// <summary>
        ///     Join group as teacher
        /// </summary>
        [Authorize]
        [HttpPost]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult AddTeacher([FromRoute] int groupId)
        {
            var requestedId = Request.GetUserId();
            _groupFacade.ApproveTeacher(requestedId, groupId);
            return Ok();
        }

        /// <summary>
        ///     Invites user to group as teacher
        /// </summary>
        [Authorize]
        [HttpPost]
        [Route("invitation")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult Invite([FromRoute] int groupId, [FromBody] InviteRequest request)
        {
            var userId = Request.GetUserId();
            _userFacade.Invite(userId, request.InvitedId, groupId, MemberRole.Teacher);
            return Ok();
        }
    }
}