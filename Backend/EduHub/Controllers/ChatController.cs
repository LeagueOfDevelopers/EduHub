using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EduHub.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using EduHub.Extensions;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/group/{idOfGroup}/chat")]
    public class ChatController : Controller
    {
        /// <summary>
        /// Sends message to group chat
        /// </summary>
        [HttpPost]
        [Authorize]
        [Route("message")]
        public IActionResult SendMessage([FromBody]SendMessageRequest message, [FromRoute]int groupId)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            return Ok($"Чату {message.ChatId} было отправлено сообщение '{message.Text}' от пользователя {userId}");
        }

        /// <summary>
        /// Returns message by id
        /// </summary>
        [HttpGet]
        [SwaggerResponse(200, Type = typeof(MessageResponse))]
        [Route("message/{messageId}")]
        public IActionResult GetMessage([FromRoute] int groupId,[FromRoute] int messageId)
        {
            MessageResponse response = new MessageResponse();
            return Ok(response);
        }

        /// <summary>
        /// Edites message by id
        /// </summary>
        [HttpPut]
        [Route("message/{messageId}")]
        public IActionResult EditMessage([FromBody]EditMessageRequest message,
                [FromRoute] int groupId, [FromRoute] int messageId)
        {
            return Ok($"Текст сообщения {messageId} исправлен на '{message.NewText}' " +
                $"в чате группы {groupId}");
        }

        /// <summary>
        /// Deletes message by id
        /// </summary>
        [HttpDelete]
        [Route("message/{messageId}")]
        public IActionResult DeleteMessage([FromRoute]int groupId, [FromRoute]int messageId)
        {
            return Ok($"Сообщение {messageId} удалено из чата группы {groupId}");
        }
    }
}