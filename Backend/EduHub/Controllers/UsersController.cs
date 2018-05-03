using System.Collections.Generic;
using System.Linq;
using EduHub.Extensions;
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
        private readonly IReportFacade _reportFacade;
        private readonly IUserFacade _userFacade;

        public UsersController(IUserFacade userFacade, IReportFacade reportFacade)
        {
            _userFacade = userFacade;
            _reportFacade = reportFacade;
        }

        /// <summary>
        ///     Searches user with filters
        /// </summary>
        [HttpGet]
        [Route("search")]
        [SwaggerResponse(200, typeof(MinUserFilterResponse))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult SearchUser([FromQuery] List<string> tags,
            [FromQuery] bool wantToTeach, [FromQuery] TeacherExperience teacherExperience,
            [FromQuery] UserExperience userExperience, [FromQuery] string name = "")
        {
            var foundUsers =
                _userFacade.FindUser(name, wantToTeach, tags, (int) teacherExperience, (int) userExperience);
            var items = new List<UserFilterResponse>();
            foundUsers.ToList().ForEach(u => items.Add(new UserFilterResponse(u.Id, u.UserProfile.Name,
                u.UserProfile.Email, u.UserProfile.AvatarLink, u.UserProfile.IsTeacher,
                u.TeacherProfile, u.IsActive, u.UserProfile.Gender, u.UserProfile.BirthYear)));
            var response = new MinUserFilterResponse(items);

            return Ok(response);
        }

        /// <summary>
        ///     Searches user by name
        /// </summary>
        [HttpGet]
        [Route("search/{name}")]
        [SwaggerResponse(200, typeof(MinUserResponse))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult SearchUserByName([FromRoute] string name)
        {
            var foundUsers = _userFacade.FindByName(name);
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
            var result = _userFacade.FindUsersForInvite(request.Username, request.GroupId, request.WantToTeach);
            var items = new List<MinUserForInvitationItem>();
            result.ToList().ForEach(res => items.Add(
                new MinUserForInvitationItem(res.Invited, res.Username, res.IsTeacher, res.Id, res.AvatarLink)
            ));
            var response = new MinUserForInvitationResponse(items);
            return Ok(response);
        }

        /// <summary>
        ///     Reports user (for now)
        /// </summary>
        [Authorize]
        [HttpPost]
        [Route("{suspectedId}/report")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult Report([FromBody] ReportRequest request, [FromRoute] int suspectedId)
        {
            var userId = Request.GetUserId();
            _reportFacade.Report(userId, suspectedId, request.Reason, request.Description);
            return Ok();
        }
    }
}