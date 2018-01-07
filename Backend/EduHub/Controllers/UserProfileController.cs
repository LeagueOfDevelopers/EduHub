using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EduHub.Models;
using EduHubLibrary.Facades;
using EduHubLibrary.Domain;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/user/{userId}/profile")]
    public class UserProfileController : Controller
    {
        public UserProfileController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        /// <summary>
        /// Deletes user's profile
        /// </summary>
        [HttpDelete]
        public IActionResult DeleteProfile([FromRoute] int idOfUser)
        {
            return Ok("Профиль удален");
        }

        /// <summary>
        /// Returns all invitations for user
        /// </summary>
        [HttpGet]
        [Route("invitations")]
        [SwaggerResponse(200, Type = typeof(GetInvitationsResponse))]
        public IActionResult GetInvitations([FromRoute] Guid userId)
        {
            IEnumerable<Invitation> invitationsForUser = _userFacade.GetAllInvitationsForUser(userId);
            GetInvitationsResponse response = new GetInvitationsResponse(invitationsForUser);
            return Ok(response);
        }

        /// <summary>
        /// Restores user's profile
        /// </summary>
        [HttpPost]
        public IActionResult RestoreProfile([FromRoute] int idOfUser)
        {
            return Ok("Профиль восстановлен");
        }

        /// <summary>
        /// Edites user's profile
        /// </summary>
        [HttpPut]
        public IActionResult EditProfile([FromBody]EditProfileRequest request, [FromRoute] int idOfUser)
        {
            return Ok($"Новые данные профиля ИМЯ:{request.Name}, ВОЗРАСТ:{request.Age}");
        }

        /// <summary>
        /// Makes user teacher
        /// </summary>
        [HttpPost]
        [Route("teaching")]
        public IActionResult BecomeTeacher([FromRoute] int idOfUser)
        {
            return Ok("Пользователь стал преподавателем");
        }

        /// <summary>
        /// Makes user regular user (not teacher)
        /// </summary>
        [HttpDelete]
        [Route("teaching")]
        public IActionResult StopToBeTeacher([FromRoute] int idOfUser)
        {
            return Ok("Пользователь перестал быть преподавателем");
        }

        /// <summary>
        /// Turns on user's notifies
        /// </summary>
        [HttpPost]
        [Route("notifies")]
        public IActionResult TurnOnNotify([FromRoute] int idOfUser)
        {
            return Ok("Уведомления включены");
        }

        /// <summary>
        /// Turns off user's notifies
        /// </summary>
        [HttpDelete]
        [Route("notifies")]
        public IActionResult TurnOffNotify([FromRoute] int idOfUser)
        {
            return Ok("Уведомления выключены");
        }

        /// <summary>
        /// Returns all notifies for user
        /// </summary>
        [HttpGet]
        [Route("notifies")]
        [SwaggerResponse(200, Type = typeof(List<NotifiesResponse>))]
        public IActionResult GetNotifies([FromRoute] int idOfUser)
        {
            List<NotifiesResponse> response = new List<NotifiesResponse>();
            return Ok(response);
        }

        /// <summary>
        /// Returns profile for user
        /// </summary>
        [HttpGet]
        [SwaggerResponse(200, Type = typeof(UserResponse))]
        public IActionResult GetProfile([FromRoute]Guid userId)
        {
            User user = _userFacade.GetUser(userId);
            UserResponse response = new UserResponse(user.Name, user.Credentials.Email, 
                user.Type, user.IsTeacher, user.TeacherProfile, user.IsActive, _userFacade.GetAllGroupsOfUser(userId));
            return Ok(response);
        }

        private readonly IUserFacade _userFacade;
    }
}