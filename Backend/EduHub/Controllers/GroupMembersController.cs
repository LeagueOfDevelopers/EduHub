using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/group/{idOfGroup}/members")]
    public class GroupMembersController : Controller
    {
        [HttpPost]
        [Route("{idOfUser}/invitation")]
        public IActionResult InviteUser()
        {
            return Ok("Пользователь приглашен");
        }

        [HttpPut]
        public IActionResult AcceptInvitation()
        {
            return Ok("Приглашение принято");
        }

        [HttpDelete]
        public IActionResult RejectInvitation()
        {
            return Ok("Приглашение отвергнуто");
        }

        [HttpPost]
        [Route("{idOfUser}")]
        public IActionResult AddUser()
        {
            return Ok("Пользователь добавлен");
        }

        [HttpDelete]
        [Route("{idOfUser}")]
        public IActionResult DeleteUser()
        {
            return Ok("Пользователь удален");
        }
    }
}