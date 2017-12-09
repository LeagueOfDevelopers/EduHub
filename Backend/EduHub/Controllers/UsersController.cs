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
        [HttpPost]
        [Route("search")]
        public IActionResult SearchUser([FromBody]SearchOfUserRequest user)
        {
            return Ok($"Поиск пользователя с именем {user.Name} осуществлен");
        }

        [HttpPost]
        [Route("{idOfUser}/report")]
        public IActionResult Report([FromRoute]int idOfUser)
        {
            return Ok($"Жалоба на пользователя добавлена");
        }
    }
}