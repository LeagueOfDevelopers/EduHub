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
        
        [HttpDelete]
        public IActionResult DeleteProfile([FromRoute] int idOfUser)
        {
            return Ok("Профиль удален");
        }

        [HttpGet]
        [Route("invitations")]
        [SwaggerResponse(200, Type = typeof(GetInvitationsResponse))]
        public IActionResult GetInvitations([FromRoute] Guid userId)
        {
            IEnumerable<Invitation> invitationsForUser = _userFacade.GetAllInvitationsForUser(userId);
            GetInvitationsResponse response = new GetInvitationsResponse(invitationsForUser);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult RestoreProfile([FromRoute] int idOfUser)
        {
            return Ok("Профиль восстановлен");
        }

        [HttpPut]
        public IActionResult EditProfile([FromBody]EditProfileRequest request, [FromRoute] int idOfUser)
        {
            return Ok($"Новые данные профиля ИМЯ:{request.Name}, ВОЗРАСТ:{request.Age}");
        }

        [HttpPost]
        [Route("teaching")]
        public IActionResult BecomeTeacher([FromRoute] int idOfUser)
        {
            return Ok("Пользователь стал преподавателем");
        }

        [HttpDelete]
        [Route("teaching")]
        public IActionResult StopToBeTeacher([FromRoute] int idOfUser)
        {
            return Ok("Пользователь перестал быть преподавателем");
        }

        [HttpPost]
        [Route("notifies")]
        public IActionResult TurnOnNotify([FromRoute] int idOfUser)
        {
            return Ok("Уведомления включены");
        }

        [HttpDelete]
        [Route("notifies")]
        public IActionResult TurnOffNotify([FromRoute] int idOfUser)
        {
            return Ok("Уведомления выключены");
        }

        [HttpGet]
        [Route("notifies")]
        [SwaggerResponse(200, Type = typeof(List<NotifiesResponse>))]
        public IActionResult GetNotifies([FromRoute] int idOfUser)
        {
            List<NotifiesResponse> response = new List<NotifiesResponse>();
            return Ok(response);
        }

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