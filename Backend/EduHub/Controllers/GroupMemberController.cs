using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EduHub.Models;
using EduHubLibrary.Facades;
using EduHubLibrary.Domain;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/group/{GroupId}/members")]
    public class GroupMemberController : Controller
    {
        [HttpPost]
        [Route("{UserId}/invite/{InvitedId}")]
        public IActionResult InviteUser([FromRoute] Guid InvitedId, [FromRoute] Guid UserId,
            [FromRoute] Guid GroupId)
        {
            _userFacade.Invite(UserId, InvitedId, GroupId);
            return Ok("Пользователь приглашен");
        }

        [HttpPut]
        public IActionResult ChangeStatusOfInvitation([FromBody] ChangeStatusOfInvitationRequest changer)
        {
            _userFacade.ChangeStatusOfInvitation(changer.UserId, changer.InvitationId, changer.Status);
            return Ok("Приглашение принято");
        }

        [HttpGet]
        [Route("{UserId}/invitations")]
        [SwaggerResponse(200, Type = typeof(GetInvitationsResponse))]
        public IActionResult GetInvitations([FromRoute] Guid UserId)
        {
            IEnumerable<Invitation> invitationsForUser =  _userFacade.GetAllInvitationsForUser(UserId);
            GetInvitationsResponse response = new GetInvitationsResponse(invitationsForUser);
            return Ok(response);
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