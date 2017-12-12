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
    public class GroupMemberController : Controller
    {
        [HttpPost]
        [Route("{idOfUser}/invitation")]
        public IActionResult InviteUser([FromRoute] int idOfGroup, [FromRoute] int idOfUser)
        {
            return Ok("Пользователь приглашен");
        }

        [HttpPut]
        public IActionResult AcceptInvitation([FromRoute] int idOfGroup)
        {
            return Ok("Приглашение принято");
        }

        [HttpDelete]
        public IActionResult RejectInvitation([FromRoute] int idOfGroup)
        {
            return Ok("Приглашение отклонено");
        }

        [HttpPost]
        [Route("{idOfUser}")]
        public IActionResult AddMember([FromRoute] int idOfGroup)
        {
            return Ok("Пользователь добавлен");
        }

        [HttpDelete]
        [Route("{idOfUser}")]
        public IActionResult DeleteMember([FromRoute] int idOfGroup)
        {
            return Ok("Пользователь удален");
        }
    }
}