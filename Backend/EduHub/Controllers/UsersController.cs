using System.Collections.Generic;
using System.Linq;
using EduHub.Models;
using EduHub.Models.UsersControllerModels;
using EduHubLibrary.Facades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly IUserFacade _userFacade;

        public UsersController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        /// <summary>
        ///     Searches user somehow (for now)
        /// </summary>
        [HttpGet]
        [Route("search")]
        [SwaggerResponse(200, typeof(MinUserResponse))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult SearchUser([FromQuery] List<string> tags, [FromQuery] string name,
            [FromQuery] bool wantToTeach, [FromQuery] TeacherExperience teacherExperience,
            [FromQuery] UserExperience userExperience)
        {
            var foundUsers = _userFacade.FindUser(name, wantToTeach, tags, (int)teacherExperience, (int)userExperience);
            var items = new List<MinItemUserResponse>();
            foundUsers.ToList().ForEach(u => items.Add(new MinItemUserResponse(u.Id, u.UserProfile.Name,
                u.UserProfile.Email,
                u.UserProfile.IsTeacher, u.IsActive, u.UserProfile.AvatarLink)));
            var response = new MinUserResponse(items);

            return Ok(response);
        }


        /// <summary>
        ///     Searches user for invitation
        /// </summary>
        [HttpPost]
        [Route("searchForInvitation")]
        [SwaggerResponse(200, typeof(MinUserForInvitationResponse))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult SearchUserForInvitation([FromBody] SearchUserForInvitationRequest request)
        {
            var result = _userFacade.FindUsersForInvite(request.Username, request.GroupId);
            var items = new List<MinUserForInvitationItem>();
            result.ToList().ForEach(res => items.Add(
                new MinUserForInvitationItem(res.Invited, res.Username, res.IsTeacher,
                    res.Id, res.Email, res.AvatarLink)
            ));
            var response = new MinUserForInvitationResponse(items);
            return Ok(response);
        }

        /// <summary>
        ///     Reports user somehow (for now)
        /// </summary>
        [Authorize]
        [HttpPost]
        [Route("{userId}/report")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult Report([FromRoute] int userId)
        {
            return Ok();
        }
    }
}