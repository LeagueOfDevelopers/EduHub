using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/administrate")]
    public class AdminController : Controller
    {
        [HttpPost]
        [Route("{idOfUser}/invitation")]
        public IActionResult GenerateInvitation([FromRoute] int idOfUser)
        {
            return Ok($"Приглашение {idOfUser} сгенерировано");
        }

        [HttpPost]
        [Route("{idOfUser}")]
        public IActionResult AddAdmin([FromRoute] int idOfUser)
        {
            return Ok($"Администратор {idOfUser} добавлен");
        }

        [HttpDelete]
        [Route("{idOfUser}")]
        public IActionResult DeleteAdmin([FromRoute] int idOfUser)
        {
            return Ok($"Администратор {idOfUser} удален");
        }
    }
}