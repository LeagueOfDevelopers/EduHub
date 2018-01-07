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
    [Route("api/users")]
    public class UsersController : Controller
    {
        /// <summary>
        /// Searches user somehow (for now)
        /// </summary>
        [HttpPost]
        [Route("search")]
        public IActionResult SearchUser([FromBody]SearchOfUserRequest user)
        {
            return Ok($"Поиск пользователя с именем {user.Name} осуществлен");
        }

        /// <summary>
        /// Reports user somehow (for now)
        /// </summary>
        [HttpPost]
        [Route("{idOfUser}/report")]
        public IActionResult Report([FromRoute]int idOfUser)
        {
            return Ok($"Жалоба на пользователя добавлена");
        }
    }
}