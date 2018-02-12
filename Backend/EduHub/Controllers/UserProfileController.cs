using System;
using System.Collections.Generic;
using System.Linq;
using EduHub.Extensions;
using EduHub.Models;
using EduHub.Models.Tools;
using EduHubLibrary.Domain;
using EduHubLibrary.Facades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/user/profile")]
    public class UserProfileController : Controller
    {
        private readonly IGroupFacade _groupFacade;

        private readonly IUserFacade _userFacade;

        public UserProfileController(IUserFacade userFacade, IGroupFacade groupFacade)
        {
            _userFacade = userFacade;
            _groupFacade = groupFacade;
        }

        /// <summary>
        ///     Deletes user's profile
        /// </summary>
        [Authorize]
        [HttpDelete]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult DeleteProfile([FromRoute] int userId)
        {
            return Ok("Профиль удален");
        }

        /// <summary>
        ///     Returns all invitations for user
        /// </summary>
        [Authorize]
        [HttpGet]
        [Route("invitations")]
        [SwaggerResponse(200, Type = typeof(InvitationsResponse))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult GetInvitations()
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            var allInv = new List<InvitationModel>();
            var currentUsername = _userFacade.GetUser(userId).UserProfile.Name;
            _userFacade.GetAllInvitationsForUser(userId).ToList().ForEach(inv =>
            {
                if (inv.Status == InvitationStatus.InProgress)
                {
                    var fromUsername = _userFacade.GetUser(inv.FromUser).UserProfile.Name;
                    var toGroupTitle = _groupFacade.GetGroup(inv.GroupId).GroupInfo.Title;
                    var invitation = new InvitationModel(inv.Id, inv.FromUser, fromUsername, inv.ToUser,
                        currentUsername, inv.GroupId, toGroupTitle, inv.SuggestedRole);
                    allInv.Add(invitation);
                }
            });
            var response = new InvitationsResponse(allInv);
            return Ok(response);
        }

        /// <summary>
        ///     Changes status of invitation, add user to group
        /// </summary>
        [Authorize]
        [HttpPut]
        [Route("invitations")]
        [SwaggerResponse(200, Type = typeof(ChangeStatusOfInvitationRequest))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult ChangeStatusOfInvitation([FromBody] ChangeStatusOfInvitationRequest changer)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            _userFacade.ChangeInvitationStatus(userId, changer.InvitationId, changer.Status);
            var invitation = _userFacade.GetAllInvitationsForUser(userId).First(i => i.Id.Equals(changer.InvitationId));

            if (invitation.SuggestedRole == MemberRole.Teacher && changer.Status == InvitationStatus.Accepted)
                _groupFacade.ApproveTeacher(userId, invitation.GroupId);

            return Ok($"Текущий статус приглашения {changer.Status}");
        }

        /// <summary>
        ///     Restores user's profile
        /// </summary>
        [Authorize]
        [HttpPost]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult RestoreProfile([FromRoute] int userId)
        {
            return Ok("Профиль восстановлен");
        }

        /// <summary>
        ///     Edites user's name
        /// </summary>
        [HttpPut]
        [Authorize]
        [Route("name")]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult EditName([FromBody] ChangeUserNameRequest request)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            _userFacade.EditName(userId, request.UserName);
            return Ok($"Новое имя пользователя '{request.UserName}'");
        }

        /// <summary>
        ///     Edites user's aboutInfo
        /// </summary>
        [HttpPut]
        [Authorize]
        [Route("about")]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult EditAboutUser([FromBody] EditAboutUserRequest request)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            _userFacade.EditAboutUser(userId, request.AboutUser);
            return Ok($"Новое описание пользователя '{request.AboutUser}'");
        }

        /// <summary>
        ///     Edites user's gender
        /// </summary>
        [HttpPut]
        [Authorize]
        [Route("gender")]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult EditGender([FromBody] bool newGender)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            _userFacade.EditGender(userId, newGender);
            return Ok($"Новый пол пользователя '{newGender}'(is man?)");
        }

        /// <summary>
        ///     Edites user's avatar link
        /// </summary>
        [HttpPut]
        [Authorize]
        [Route("avatar")]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult EditAvatarLink([FromBody] EditAvatarLinkRequest request)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            _userFacade.EditAvatarLink(userId, request.AvatarLink);
            return Ok($"Новая ссылка на аватарку пользователя '{request.AvatarLink}'");
        }

        /// <summary>
        ///     Edites user's contacts
        /// </summary>
        [HttpPut]
        [Authorize]
        [Route("contacts")]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult EditContacts([FromBody] EditContactListRequest request)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            _userFacade.EditContacts(userId, request.Contacts);
            return Ok();
        }

        /// <summary>
        ///     Edites user's birth year
        /// </summary>
        [HttpPut]
        [Authorize]
        [Route("birthyear")]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult EditBirthYear([FromBody] EditBirthYearRequest request)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            _userFacade.EditBirthYear(userId, request.BirthYear);
            return Ok($"Новый год рождения пользователя '{request.BirthYear}'");
        }

        /// <summary>
        ///     Makes user teacher
        /// </summary>
        [Authorize]
        [HttpPost]
        [Route("teaching")]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult BecomeTeacher()
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            _userFacade.BecomeTeacher(userId);
            return Ok("Пользователь стал преподавателем");
        }

        /// <summary>
        ///     Makes user regular user (not teacher)
        /// </summary>
        [Authorize]
        [HttpDelete]
        [Route("teaching")]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult StopToBeTeacher()
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            _userFacade.StopToBeTeacher(userId);
            return Ok("Пользователь перестал быть преподавателем");
        }

        /// <summary>
        ///     Turns on user's notifies
        /// </summary>
        [Authorize]
        [HttpPost]
        [Route("notifies")]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult TurnOnNotify([FromRoute] int userId)
        {
            return Ok("Уведомления включены");
        }

        /// <summary>
        ///     Turns off user's notifies
        /// </summary>
        [Authorize]
        [HttpDelete]
        [Route("notifies")]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult TurnOffNotify([FromRoute] int userId)
        {
            return Ok("Уведомления выключены");
        }

        /// <summary>
        ///     Returns all notifies for user
        /// </summary>
        [Authorize]
        [HttpGet]
        [Route("notifies")]
        [SwaggerResponse(200, Type = typeof(List<NotifyResponse>))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult GetNotifies()
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            var notifies = _userFacade.GetNotifies(userId).ToList();
            return Ok(notifies);
        }

        /// <summary>
        ///     Returns all user groups
        /// </summary>
        [Authorize]
        [HttpGet]
        [Route("groups/{userId}")]
        [SwaggerResponse(200, Type = typeof(MinGroupResponse))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult GetGroups([FromRoute] Guid userId)
        {
            var groups = _userFacade.GetAllGroupsOfUser(userId);
            var items = new List<MinItemGroupResponse>();
            groups.ToList().ForEach(g =>
            {
                var memberAmount = _groupFacade.GetGroupMembers(g.GroupInfo.Id).ToList().Count;
                var groupInfo = new MinGroupInfo(g.GroupInfo.Id, g.GroupInfo.Title, memberAmount, g.GroupInfo.Size,
                    g.GroupInfo.MoneyPerUser, g.GroupInfo.GroupType, g.GroupInfo.Tags);
                items.Add(new MinItemGroupResponse(groupInfo));
            });
            var response = new MinGroupResponse(items);
            return Ok(response);
        }

        /// <summary>
        ///     Returns user profile
        /// </summary>
        [HttpGet]
        [Route("{userId}")]
        [SwaggerResponse(200, Type = typeof(ProfileResponse))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult GetProfile([FromRoute] Guid userId)
        {
            var user = _userFacade.GetUser(userId);
            ProfileResponse response;
            var userProfile = new UserProfileModel(user.UserProfile.Name, user.UserProfile.Email,
                user.UserProfile.AboutUser, user.UserProfile.BirthYear, user.UserProfile.IsMan,
                user.UserProfile.IsTeacher, user.UserProfile.AvatarLink, user.UserProfile.Contacts);
            if (user.UserProfile.IsTeacher)
            {
                var reviews = new List<ReviewModel>();
                user.TeacherProfile.Reviews.ForEach(r =>
                {
                    reviews.Add(new ReviewModel(r.FromUser, r.Title, r.Text, r.Date));
                });
                var teacherProfile = new TeacherProfileModel(reviews,
                    user.TeacherProfile.Skills);
                response = new ProfileResponse(userProfile, teacherProfile);
            }
            else
            {
                response = new ProfileResponse(userProfile);
            }

            return Ok(response);
        }
    }
}