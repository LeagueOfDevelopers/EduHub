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
using EduHubLibrary.Domain.NotificationService;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/user/profile")]
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
        [Authorize]
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
        /// Changes status of invitation, add user to group
        /// </summary>
        [Authorize]
        [HttpPut]
        [Route("invitations")]
        [SwaggerResponse(200, Type = typeof(ChangeStatusOfInvitationRequest))]
        public IActionResult ChangeStatusOfInvitation([FromBody] ChangeStatusOfInvitationRequest changer)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            _userFacade.ChangeInvitationStatus(userId, changer.InvitationId, changer.Status);
            Invitation invitation = _userFacade.GetAllInvitationsForUser(userId).First(i => i.Id.Equals(changer.InvitationId));

            if (invitation.SuggestedRole == MemberRole.Teacher && changer.Status == InvitationStatus.Accepted)
            {
                _groupFacade.ApproveTeacher(userId, invitation.GroupId);
            }

            return Ok($"Текущий статус приглашения {changer.Status}");
        }

        /// <summary>
        /// Restores user's profile
        /// </summary>
        [Authorize]
        [HttpPost]
        public IActionResult RestoreProfile([FromRoute] int userId)
        {
            return Ok("Профиль восстановлен");
        }

        /// <summary>
        /// Edites user's profile
        /// </summary>
        [HttpPut]
        [Authorize]
        public IActionResult EditProfile([FromBody]EditProfileRequest request, [FromRoute] int userId)
        {
            return Ok($"Новые данные профиля ИМЯ:{request.Name}, ВОЗРАСТ:{request.Age}");
        }

        /// <summary>
        /// Makes user teacher
        /// </summary>
        [Authorize]
        [HttpPost]
        [Route("teaching")]
        public IActionResult BecomeTeacher([FromRoute] int userId)
        {
            return Ok("Пользователь стал преподавателем");
        }

        /// <summary>
        /// Makes user regular user (not teacher)
        /// </summary>
        [Authorize]
        [HttpDelete]
        [Route("teaching")]
        public IActionResult StopToBeTeacher([FromRoute] int userId)
        {
            return Ok("Пользователь перестал быть преподавателем");
        }

        /// <summary>
        /// Turns on user's notifies
        /// </summary>
        [Authorize]
        [HttpPost]
        [Route("notifies")]
        public IActionResult TurnOnNotify([FromRoute] int userId)
        {
            return Ok("Уведомления включены");
        }

        /// <summary>
        /// Turns off user's notifies
        /// </summary>
        [Authorize]
        [HttpDelete]
        [Route("notifies")]
        public IActionResult TurnOffNotify([FromRoute] int userId)
        {
            return Ok("Уведомления выключены");
        }

        /// <summary>
        /// Returns all notifies for user
        /// </summary>
        [Authorize]
        [HttpGet]
        [Route("notifies")]
        [SwaggerResponse(200, Type = typeof(List<NotifiesResponse>))]
        public IActionResult GetNotifies()
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            List<Event> notifies = _userFacade.GetUser(userId).GetNotifies().ToList();
            return Ok(notifies);
        }

        /// <summary>
        /// Returns all user groups
        /// </summary>
        [Authorize]
        [HttpGet]
        [Route("groups")]
        [SwaggerResponse(200, Type = typeof(MinGroupResponse))]
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
        /// Returns user profile
        /// </summary>
        [HttpGet]
        [Route("{userId}")]
        [SwaggerResponse(200, Type = typeof(ProfileResponse))]
        public IActionResult GetProfile([FromRoute]Guid userId)
        {
            User user = _userFacade.GetUser(userId);
            ProfileResponse response;
            if (user.UserProfile.IsTeacher)
            {
                response = new ProfileResponse(user.UserProfile, user.TeacherProfile);
            }
            else
            {
                response = new ProfileResponse(user.UserProfile);
            }
            return Ok(response);
        }

        private readonly IUserFacade _userFacade;
        private readonly IGroupFacade _groupFacade;
    }
}