using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduHub.Extensions;
using EduHub.Middleware;
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
        private readonly NotificationsMessageHandler _notificationsMessageHandler;


        public ChatController(IChatFacade chatFacade, IGroupFacade groupFacade,
            NotificationsMessageHandler notificationsMessageHandler)
        {
            _chatFacade = chatFacade;
            _groupFacade = groupFacade;
            _notificationsMessageHandler = notificationsMessageHandler;
        }

        /// <summary>
        ///     Sends message to group chat
        /// </summary>
        [HttpPost]
        [Authorize]
        [SwaggerResponse(200, Type = typeof(MessageSentResponse))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest messageRequest,
            [FromRoute] int groupId)
        {
            var userId = Request.GetUserId();

            var users = _groupFacade.GetGroupMembers(groupId).ToList();
            var userIds = new List<int>();
            users.ForEach(m => userIds.Add(m.UserId));
            var response = new MessageSentResponse(_chatFacade.SendMessage(userId, groupId, messageRequest.Text));
            await _notificationsMessageHandler.SendMessageToAllAsync(messageRequest.Text, groupId, userIds);
            return Ok(response);
        }

        /// <summary>
        ///     Returns message by id
        /// </summary>
        [Authorize]
        [HttpGet]
        [SwaggerResponse(200, Type = typeof(MessageInfoResponse))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [Route("{messageId}")]
        public IActionResult GetMessage([FromRoute] int groupId, [FromRoute] int messageId)
        {
            var userId = Request.GetUserId();

            var message = _chatFacade.GetMessage(messageId, groupId, userId);
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
            var userId = Request.GetUserId();

            var response = new List<MessageInfoResponse>();
            var messageView = _chatFacade.GetMessagesForGroup(groupId, userId);
            messageView.ToList().ForEach(
                m => response.Add(new MessageInfoResponse(m.Id, m.SenderId, m.SenderName, m.SentOn, m.Text)));
            return Ok(response);
        }
    }
}