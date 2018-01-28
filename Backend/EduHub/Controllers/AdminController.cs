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
        [Route("{userId}/invitation")]
        public IActionResult GenerateInvitation([FromRoute] int userId)
        {
            return Ok($"Приглашение {userId} сгенерировано");
        }

        /// <summary>
        /// Makes user admin
        /// </summary>
        [HttpPost]
        [Route("{userId}")]
        public IActionResult AddAdmin([FromRoute] int userId)
        {
            return Ok($"Администратор {userId} добавлен");
        }

        /// <summary>
        /// Makes user regular user (not admin)
        /// </summary>
        [HttpDelete]
        [Route("{userId}")]
        public IActionResult DeleteAdmin([FromRoute] int userId)
        {
            return Ok($"Администратор {userId} удален");
        }
    }
}