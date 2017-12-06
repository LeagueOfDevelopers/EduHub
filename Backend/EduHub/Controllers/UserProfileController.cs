using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EduHub.Models;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/users/{idOfUser}/profile")]
    public class UserProfileController : Controller
    {
        [HttpDelete]
        public IActionResult DeleteProfile()
        {
            return Ok("Профиль удален");
        }

        [HttpPost]
        public IActionResult RestoreProfile()
        {
            return Ok("Профиль восстановлен");
        }

        [HttpPut]
        public IActionResult EditProfile([FromBody]EditProfileRequest request)
        {
            return Ok($"Новые данные профиля ИМЯ:{request.Name}, ВОЗРАСТ:{request.Age}");
        }

        [HttpPost]
        [Route("teaching")]
        public IActionResult BecomeTeacher()
        {
            return Ok("Пользователь стал преподавателем");
        }

        [HttpDelete]
        [Route("teaching")]
        public IActionResult StopToBeTeacher()
        {
            return Ok("Пользователь перестал быть преподавателем");
        }

        [HttpPost]
        [Route("announcements")]
        public IActionResult TurnOnNotify()
        {
            return Ok("Уведомления включены");
        }

        [HttpDelete]
        [Route("announcements")]
        public IActionResult TurnOffNotify()
        {
            return Ok("Уведомления выключены");
        }

        [HttpGet]
        [Route("announcements")]
        public IActionResult ShowNotifies()
        {
            return Ok("Просмотр уведомлений");
        }
    }
}