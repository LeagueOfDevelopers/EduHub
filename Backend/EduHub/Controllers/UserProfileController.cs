﻿using System.Collections.Generic;
using System.Linq;
using EduHub.Extensions;
using EduHub.Models;
using EduHub.Models.NotificationsModels;
using EduHub.Models.Tools;
using EduHub.Models.UserProfileControllerModels;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.NotificationService.Notifications;
using EduHubLibrary.Domain.NotificationService.UserSettings;
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
        private readonly ISanctionFacade _sanctionFacade;
        private readonly IUserEditFacade _userEditFacade;
        private readonly IUserFacade _userFacade;

        public UserProfileController(IUserFacade userFacade, IGroupFacade groupFacade, IUserEditFacade userEditFacade,
            ISanctionFacade sanctionFacade)
        {
            _userFacade = userFacade;
            _userEditFacade = userEditFacade;
            _groupFacade = groupFacade;
            _sanctionFacade = sanctionFacade;
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
            return Ok();
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
            var userId = Request.GetUserId();
            var allInv = _userFacade.GetAllInvitationsForUser(userId);
            var invitationModels = new List<InvitationModel>();
            allInv.ToList().ForEach(i => invitationModels.Add(new InvitationModel(i.Id,
                i.FromUser, i.FromUserName, i.ToUser, i.ToUserName, i.ToGroup,
                i.ToGroupTitle, i.SuggestedRole)));
            var response = new InvitationsResponse(invitationModels);
            return Ok(response);
        }

        /// <summary>
        ///     Changes status of invitation, add user to group
        /// </summary>
        [Authorize]
        [HttpPut]
        [Route("invitations")]
        [SwaggerResponse(200, Type = typeof(ChangeInvitationStatusResponse))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult ChangeStatusOfInvitation([FromBody] ChangeStatusOfInvitationRequest changer)
        {
            var userId = Request.GetUserId();
            _userFacade.ChangeInvitationStatus(userId, changer.InvitationId, changer.Status);
            var invitation = _userFacade.GetAllInvitationsForUser(userId).First(i => i.Id.Equals(changer.InvitationId));

            var response = new ChangeInvitationStatusResponse(invitation.ToGroup);
            return Ok(response);
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
            return Ok();
        }

        /// <summary>
        ///     Edites user's profile
        /// </summary>
        [Authorize]
        [HttpPut]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult EditProfile([FromBody] EditProfileRequest request)
        {
            var userId = Request.GetUserId();
            _userEditFacade.EditProfile(userId, request.Name, request.AboutUser, request.Gender, request.AvatarLink,
                request.Contacts, request.BirthYear);
            return Ok();
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
            var userId = Request.GetUserId();
            _userEditFacade.EditName(userId, request.UserName);
            return Ok();
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
            var userId = Request.GetUserId();
            _userEditFacade.EditAboutUser(userId, request.AboutUser);
            return Ok();
        }

        /// <summary>
        ///     Edites user's gender
        /// </summary>
        [HttpPut]
        [Authorize]
        [Route("gender")]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult EditGender([FromBody] EditUserGenderRequest request)
        {
            var userId = Request.GetUserId();
            _userEditFacade.EditGender(userId, request.Gender);
            return Ok();
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
            var userId = Request.GetUserId();
            _userEditFacade.EditAvatarLink(userId, request.AvatarLink);
            return Ok();
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
            var userId = Request.GetUserId();
            _userEditFacade.EditContacts(userId, request.Contacts);
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
            var userId = Request.GetUserId();
            _userEditFacade.EditBirthYear(userId, request.BirthYear);
            return Ok();
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
            var userId = Request.GetUserId();
            _userEditFacade.BecomeTeacher(userId);
            return Ok();
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
            var userId = Request.GetUserId();
            _userEditFacade.StopToBeTeacher(userId);
            return Ok();
        }

        /// <summary>
        ///     Edites teacher's skills
        /// </summary>
        [Authorize]
        [HttpPut]
        [Route("teaching/skills")]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult EditTeacherProfile([FromBody] EditTeacherProfileRequest request)
        {
            var userId = Request.GetUserId();
            _userEditFacade.EditTeacherProfile(userId, request.NewSkills);
            return Ok();
        }


        /// <summary>
        ///     Configures notifications' settings
        /// </summary>
        [Authorize]
        [HttpPut]
        [Route("notifications/settings")]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult ConfigureNotifications([FromBody] ConfigureNotificationsRequest request)
        {
            var userId = Request.GetUserId();
            _userEditFacade.ConfigureNotificationsSettings(userId, request.ConfiguringNotification, request.NewValue);
            return Ok();
        }

        /// <summary>
        ///     Returns user's notifications' settings
        /// </summary>
        [Authorize]
        [HttpGet]
        [Route("notifications/settings")]
        [SwaggerResponse(200, Type = typeof(IReadOnlyDictionary<NotificationType, NotificationValue>))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult GetNotificationsSettings()
        {
            var userId = Request.GetUserId();
            var response = _userFacade.GetUser(userId).NotificationsSettings.Settings;
            return Ok(response);
        }

        /// <summary>
        ///     Returns all notifications for user
        /// </summary>
        [Authorize]
        [HttpGet]
        [Route("notifications")]
        [SwaggerResponse(200, Type = typeof(AllPossibleNotifies))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult GetNotifications()
        {
            var userId = Request.GetUserId();
            var notifies = _userFacade.GetNotifies(userId).ToList();
            return Ok(notifies);
        }

        /// <summary>
        ///     Returns all user groups
        /// </summary>
        [HttpGet]
        [Route("groups/{userId}")]
        [SwaggerResponse(200, Type = typeof(MinGroupResponse))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult GetGroups([FromRoute] int userId)
        {
            var groups = _userFacade.GetAllGroupsOfUser(userId);
            var items = new List<MinItemGroupResponse>();
            groups.ToList().ForEach(g =>
            {
                var memberAmount = _groupFacade.GetGroupMembers(g.GroupInfo.Id).ToList().Count;
                var groupInfo = new MinGroupInfo(g.GroupInfo.Id, g.GroupInfo.Title, memberAmount, g.GroupInfo.Size,
                    g.GroupInfo.Price, g.GroupInfo.GroupType, g.GroupInfo.Tags);
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
        public IActionResult GetProfile([FromRoute] int userId)
        {
            var user = _userFacade.GetUser(userId);
            var sanctions = _sanctionFacade.GetAllActiveOfUser(userId).ToList();

            ProfileResponse response;
            var userProfile = new UserProfileModel(user.UserProfile.Name, user.UserProfile.Email,
                user.UserProfile.AboutUser, user.UserProfile.BirthYear, user.UserProfile.Gender,
                user.UserProfile.IsTeacher, user.UserProfile.AvatarLink, user.UserProfile.Contacts, sanctions);
            if (user.UserProfile.IsTeacher)
            {
                var reviews = new List<ReviewModel>();
                user.TeacherProfile.Reviews.ToList().ForEach(r =>
                {
                    reviews.Add(new ReviewModel(r.FromUser, r.Title, r.Text, r.Date, r.FromGroup));
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