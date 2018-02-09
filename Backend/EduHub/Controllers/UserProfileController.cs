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
using EduHub.Models.Tools;

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
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
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
        [SwaggerResponse(200, Type = typeof(InvitationsResponse))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult GetInvitations()
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            List<InvitationModel> allInv = new List<InvitationModel>();
            var currentUsername = _userFacade.GetUser(userId).UserProfile.Name;
            _userFacade.GetAllInvitationsForUser(userId).ToList().ForEach(inv =>
            {
                if (inv.Status == InvitationStatus.InProgress) { 
                    var fromUsername = _userFacade.GetUser(inv.FromUser).UserProfile.Name;
                    var toGroupTitle = _groupFacade.GetGroup(inv.GroupId).GroupInfo.Title;
                    InvitationModel invitation = new InvitationModel(inv.Id, inv.FromUser, fromUsername, inv.ToUser,
                        currentUsername, inv.GroupId, toGroupTitle, inv.SuggestedRole);
                    allInv.Add(invitation);
                }
            });
            InvitationsResponse response = new InvitationsResponse(allInv);
            return Ok(response);
        }

        /// <summary>
        /// Changes status of invitation, add user to group
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
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult RestoreProfile([FromRoute] int userId)
        {
            return Ok("Профиль восстановлен");
        }

        /// <summary>
        /// Edites user's name
        /// </summary>
        [HttpPut]
        [Authorize]
        [Route("name")]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult EditName([FromBody] string newName)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            _userFacade.EditName(userId, newName);
            return Ok($"Новое имя пользователя '{newName}'");
        }

        /// <summary>
        /// Edites user's aboutInfo
        /// </summary>
        [HttpPut]
        [Authorize]
        [Route("about")]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult EditAboutUser([FromBody] string newAboutUser)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            _userFacade.EditAboutUser(userId, newAboutUser);
            return Ok($"Новое описание пользователя '{newAboutUser}'");
        }

        /// <summary>
        /// Edites user's gender
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
        /// Edites user's avatar link
        /// </summary>
        [HttpPut]
        [Authorize]
        [Route("avatar")]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult EditAvatarLink([FromBody] string newAvatarLink)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            _userFacade.EditAvatarLink(userId, newAvatarLink);
            return Ok($"Новая ссылка на аватарку пользователя '{newAvatarLink}'");
        }

        /// <summary>
        /// Edites user's contacts
        /// </summary>
        [HttpPut]
        [Authorize]
        [Route("contacts")]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult EditContacts([FromBody] List<string> newContacts)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            _userFacade.EditContacts(userId, newContacts);
            return Ok(newContacts);
        }

        /// <summary>
        /// Edites user's birth year
        /// </summary>
        [HttpPut]
        [Authorize]
        [Route("birthyear")]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult EditBirthYear([FromBody] int newBirthYear)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            _userFacade.EditBirthYear(userId, newBirthYear);
            return Ok($"Новый год рождения пользователя '{newBirthYear}'");
        }

        /// <summary>
        /// Makes user teacher
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
        /// Makes user regular user (not teacher)
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
        /// Turns on user's notifies
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
        /// Turns off user's notifies
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
        /// Returns all notifies for user
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
            List<string> notifies = _userFacade.GetNotifies(userId).ToList();
            return Ok(notifies);
        }

        /// <summary>
        /// Returns all user groups
        /// </summary>
        [Authorize]
        [HttpGet]
        [Route("groups")]
        [SwaggerResponse(200, Type = typeof(MinGroupResponse))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult GetGroups()
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            IEnumerable<Group> groups = _userFacade.GetAllGroupsOfUser(userId);
            List<MinItemGroupResponse> items = new List<MinItemGroupResponse>();
            groups.ToList().ForEach(g => 
            {
                int memberAmount = _groupFacade.GetGroupMembers(g.GroupInfo.Id).ToList().Count;
                MinGroupInfo groupInfo = new MinGroupInfo(g.GroupInfo.Id, g.GroupInfo.Title, memberAmount, g.GroupInfo.Size, 
                    g.GroupInfo.MoneyPerUser, g.GroupInfo.GroupType, g.GroupInfo.Tags);
                items.Add(new MinItemGroupResponse(groupInfo));
            });
            MinGroupResponse response = new MinGroupResponse(items);
            return Ok(response);
        }

        /// <summary>
        /// Returns user profile
        /// </summary>
        [HttpGet]
        [Route("{userId}")]
        [SwaggerResponse(200, Type = typeof(ProfileResponse))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult GetProfile([FromRoute]Guid userId)
        {
            User user = _userFacade.GetUser(userId);
            ProfileResponse response;
            UserProfileModel userProfile = new UserProfileModel(user.UserProfile.Name, user.UserProfile.Email,
                user.UserProfile.AboutUser, user.UserProfile.BirthYear, user.UserProfile.IsMan,
                user.UserProfile.IsTeacher, user.UserProfile.AvatarLink, user.UserProfile.Contacts);
            if (user.UserProfile.IsTeacher)
            {
                List<ReviewModel> reviews = new List<ReviewModel>();
                user.TeacherProfile.Reviews.ForEach(r => 
                {
                    reviews.Add(new ReviewModel(r.FromUser, r.Title, r.Text, r.Date));
                });
                TeacherProfileModel teacherProfile = new TeacherProfileModel(reviews,
                    user.TeacherProfile.Skills);
                response = new ProfileResponse(userProfile, teacherProfile);
            }
            else
            {
                response = new ProfileResponse(userProfile);
            }
            return Ok(response);
        }

        private readonly IUserFacade _userFacade;
        private readonly IGroupFacade _groupFacade;
    }
}