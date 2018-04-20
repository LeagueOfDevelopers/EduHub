using System;
using System.Collections.Generic;
using System.Linq;
using EduHub.Extensions;
using EduHub.Models;
using EduHubLibrary.Facades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/group/{groupId}/chat")]
    public class ChatController : Controller
    {
        private readonly IChatFacade _chatFacade;
        private readonly IGroupFacade _groupFacade;

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
        [SwaggerResponse(200, Type = typeof(MessageSentResponse))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult SendMessage([FromBody] SendMessageRequest messageRequest, [FromRoute] int groupId)
        {
            var userId = Request.GetUserId();
            var response = new MessageSentResponse(_chatFacade.SendMessage(userId, groupId, messageRequest.Text));
            return Ok(response);
        }

        /// <summary>
        ///     Returns message by id
        /// </summary>
        [HttpGet]
        [SwaggerResponse(200, Type = typeof(MessageInfoResponse))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [Route("{messageId}")]
        public IActionResult GetMessage([FromRoute] int groupId, [FromRoute] int messageId)
        {
            var message = _chatFacade.GetMessage(messageId, groupId);
            var response = new MessageInfoResponse(message.Id, message.SenderId, message.SenderName, 
                message.SentOn, message.Text);
            return Ok(response);
        }


        /// <summary>
        ///     Return all messages of group
        /// </summary>
        [Authorize]
        [HttpGet]
        [SwaggerResponse(200, Type = typeof(MessageInfoResponse))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult GetAllMessages([FromRoute] int groupId)
        {
            var response = new List<MessageInfoResponse>();
            var messagesView = _groupFacade.GetGroup(groupId);
            messagesView.MessageView.ToList().ForEach(
                m => response.Add(new MessageInfoResponse(m.Id, m.SenderId, m.SenderName, m.SentOn, m.Text)));
            return Ok(response);
        }
    }
}