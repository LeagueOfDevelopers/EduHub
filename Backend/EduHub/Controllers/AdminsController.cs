using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/admins")]
    public class AdminsController : Controller
    {
        [HttpPost]
        [Route("{idOfUser}/invitation")]
        public IActionResult GenerateInvitation()
        {
            return Ok("Приглашение сгенерировано");
        }

        [HttpPost]
        [Route("{idOfUser}")]
        public IActionResult AddAdmin()
        {
            return Ok("Администратор добавлен");
        }

        [HttpDelete]
        [Route("{idOfUser}")]
        public IActionResult DeleteAdmin()
        {
            return Ok("Администратор удален");
        }
    }
}