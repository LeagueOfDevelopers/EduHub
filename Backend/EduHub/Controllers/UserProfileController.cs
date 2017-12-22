using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EduHub.Models;
using EduHubLibrary.Facades;
using EduHubLibrary.Domain;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/users/{idOfUser}/profile")]
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
        public IActionResult GetNotifies([FromRoute] int idOfUser)
        {
            List<NotifiesResponse> response = new List<NotifiesResponse>();
            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetProfile([FromRoute] Guid idOfUser)
        {
            User user = _userFacade.GetUser(idOfUser);
            UserResponse response = new UserResponse(user.Name, user.Email, user.IsTeacher, user.IsActive);
            return Ok(response);
        }

        private readonly IUserFacade _userFacade;
    }
}