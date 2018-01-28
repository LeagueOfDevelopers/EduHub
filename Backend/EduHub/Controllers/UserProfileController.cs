using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EduHub.Models;
using EduHubLibrary.Facades;
using EduHubLibrary.Domain;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using EduHub.Extensions;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/user/profile")]
    [Authorize]
    public class UserProfileController : Controller
    {
        public UserProfileController(IUserFacade userFacade, IGroupFacade groupFacade)
        {
            _userFacade = userFacade;
            _groupFacade = groupFacade;
        }

        /// <summary>
        /// Deletes user's profile
        /// </summary>
        [HttpDelete]
        public IActionResult DeleteProfile([FromRoute] int userId)
        {
            return Ok("Профиль удален");
        }

        /// <summary>
        /// Returns all invitations for user
        /// </summary>
        [Authorize]
        [HttpGet]
        [Route("invitations")]
        [SwaggerResponse(200, Type = typeof(GetInvitationsResponse))]
        public IActionResult GetInvitations()
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            IEnumerable<Invitation> invitationsForUser = _userFacade.GetAllInvitationsForUser(userId);
            GetInvitationsResponse response = new GetInvitationsResponse(invitationsForUser);
            return Ok(response);
        }

        /// <summary>
        /// Restores user's profile
        /// </summary>
        [HttpPost]
        public IActionResult RestoreProfile([FromRoute] int userId)
        {
            return Ok("Профиль восстановлен");
        }

        /// <summary>
        /// Edites user's profile
        /// </summary>
        [HttpPut]
        public IActionResult EditProfile([FromBody]EditProfileRequest request, [FromRoute] int userId)
        {
            return Ok($"Новые данные профиля ИМЯ:{request.Name}, ВОЗРАСТ:{request.Age}");
        }

        /// <summary>
        /// Makes user teacher
        /// </summary>
        [HttpPost]
        [Route("teaching")]
        public IActionResult BecomeTeacher([FromRoute] int userId)
        {
            return Ok("Пользователь стал преподавателем");
        }

        /// <summary>
        /// Makes user regular user (not teacher)
        /// </summary>
        [HttpDelete]
        [Route("teaching")]
        public IActionResult StopToBeTeacher([FromRoute] int userId)
        {
            return Ok("Пользователь перестал быть преподавателем");
        }

        /// <summary>
        /// Turns on user's notifies
        /// </summary>
        [HttpPost]
        [Route("notifies")]
        public IActionResult TurnOnNotify([FromRoute] int userId)
        {
            return Ok("Уведомления включены");
        }

        /// <summary>
        /// Turns off user's notifies
        /// </summary>
        [HttpDelete]
        [Route("notifies")]
        public IActionResult TurnOffNotify([FromRoute] int userId)
        {
            return Ok("Уведомления выключены");
        }

        /// <summary>
        /// Returns all notifies for user
        /// </summary>
        [HttpGet]
        [Route("notifies")]
        [SwaggerResponse(200, Type = typeof(List<NotifiesResponse>))]
        public IActionResult GetNotifies([FromRoute] int userId)
        {
            List<NotifiesResponse> response = new List<NotifiesResponse>();
            return Ok(response);
        }

        /// <summary>
        /// Returns all groups of user
        /// </summary>
        [Authorize]
        [HttpGet]
        [Route("groups")]
        [SwaggerResponse(200, Type = typeof(GroupsOfUserResponse))]
        public IActionResult GetGroups()
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            IEnumerable<Group> groups = _userFacade.GetAllGroupsOfUser(userId);
            List<MinItemGroupResponse> items = new List<MinItemGroupResponse>();
            groups.ToList().ForEach(g => items.Add(new MinItemGroupResponse(g.GroupInfo, _groupFacade.GetMembersOfGroup(g.GroupInfo.Id).Count<Member>())));
            MinGroupResponse response = new MinGroupResponse(items);
            return Ok(response);
        }

        /// <summary>
        /// Returns profile for user
        /// </summary>
        [Authorize]
        [HttpGet]
        [SwaggerResponse(200, Type = typeof(MinUserResponse))]
        public IActionResult GetProfile()
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            User user = _userFacade.GetUser(userId);
            UserResponse response = new UserResponse(user.Name, user.Credentials.Email, 
                user.Type, user.IsTeacher, user.TeacherProfile, user.IsActive);
            return Ok(response);
        }

        private readonly IUserFacade _userFacade;
        private readonly IGroupFacade _groupFacade;
    }
}