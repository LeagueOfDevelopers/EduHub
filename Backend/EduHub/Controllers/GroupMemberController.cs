using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EduHub.Models;
using EduHubLibrary.Facades;
using Swashbuckle;
using Swashbuckle.AspNetCore.SwaggerGen;
using EduHubLibrary.Domain;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/group/{groupId}/member")]
    public class GroupMemberController : Controller
    {
        [HttpPost]
        [Route("{inviterId}/invite/{invitedId}")]
        public IActionResult InviteUser([FromRoute] Guid invitedId, [FromRoute] Guid inviterId,
            [FromRoute] Guid groupId)
        {
            _userFacade.Invite(inviterId, invitedId, groupId, MemberRole.Member);
            return Ok("Пользователь приглашен");
        }

        [HttpPut]
        public IActionResult ChangeStatusOfInvitation([FromBody] ChangeStatusOfInvitationRequest changer)
        {
            _userFacade.ChangeStatusOfInvitation(changer.UserId, changer.InvitationId, changer.Status);
            return Ok("Приглашение принято");
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

        public GroupMemberController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        private readonly IUserFacade _userFacade;

    }
}