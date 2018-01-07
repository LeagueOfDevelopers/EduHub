using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/administrate")]
    public class AdminController : Controller
    {
        /// <summary>
        /// Generates invitation for admin account
        /// </summary>
        [HttpPost]
        [Route("{idOfUser}/invitation")]
        public IActionResult GenerateInvitation([FromRoute] int idOfUser)
        {
            return Ok($"Приглашение {idOfUser} сгенерировано");
        }

        /// <summary>
        /// Makes user admin
        /// </summary>
        [HttpPost]
        [Route("{idOfUser}")]
        public IActionResult AddAdmin([FromRoute] int idOfUser)
        {
            return Ok($"Администратор {idOfUser} добавлен");
        }

        /// <summary>
        /// Makes user regular user (not admin)
        /// </summary>
        [HttpDelete]
        [Route("{idOfUser}")]
        public IActionResult DeleteAdmin([FromRoute] int idOfUser)
        {
            return Ok($"Администратор {idOfUser} удален");
        }
    }
}