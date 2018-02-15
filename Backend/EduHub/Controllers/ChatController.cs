using EduHub.Extensions;
using EduHub.Models;
using EduHubLibrary.Facades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/group/{groupId}/chat")]
    public class ChatController : Controller
    {
        public ChatController(IChatFacade chatFacade, IGroupFacade groupFacade)
        {
            _chatFacade = chatFacade;
            _groupFacade = groupFacade;
        }

        /// <summary>
        ///     Sends message to group chat
        /// </summary>
        [HttpPost]
        [Authorize]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult SendMessage([FromBody] SendMessageRequest messageRequest, [FromRoute] Guid groupId)
        {
            var userId = Request.GetUserId();
            var response = new MessageSentResponse(_chatFacade.SendMessage(userId, groupId, messageRequest.Text));
            return Ok(response);
        }

        /// <summary>
        ///     Returns message by id
        /// </summary>
        [HttpGet]
        [SwaggerResponse(200, Type = typeof(MessageResponse))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [Route("{messageId}")]
        public IActionResult GetMessage([FromRoute] Guid groupId, [FromRoute] Guid messageId)
        {
            var message = _chatFacade.GetMessage(messageId, groupId);
            var response = new MessageResponse(message.Text, message.SenderId, message.SentOn);
            return Ok(response);
        }

        /// <summary>
        ///     Edites message by id
        /// </summary>
        [Authorize]
        [HttpPut]
        [Route("{messageId}")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult EditMessage([FromBody] EditMessageRequest message,
            [FromRoute] Guid groupId, [FromRoute] Guid messageId)
        {
            var userId = Request.GetUserId();
            _chatFacade.EditMessage(messageId, groupId, message.NewText);
            return Ok();
        }

        /// <summary>
        ///     Deletes message by id
        /// </summary>
        [Authorize]
        [HttpDelete]
        [Route("{messageId}")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult DeleteMessage([FromRoute] Guid groupId, [FromRoute] Guid messageId)
        {
            var userId = Request.GetUserId();
            _chatFacade.DeleteMessage(messageId, groupId);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllMessages([FromRoute] Guid groupId)
        {
            return Ok(_groupFacade.GetGroup(groupId).Chat);
        }

        private readonly IChatFacade _chatFacade;
        private readonly IGroupFacade _groupFacade;
    }
}