using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EduHub.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

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
        [Route("message")]
        public IActionResult SendMessage([FromBody]SendMessageRequest message, [FromRoute]int idOfGroup)
        {
            return Ok($"Чату {message.ChatId} было отправлено сообщение '{message.Text}'");
        }

        /// <summary>
        /// Returns message by id
        /// </summary>
        [HttpGet]
        [SwaggerResponse(200, Type = typeof(MessageResponse))]
        [Route("message/{idOfMessage}")]
        public IActionResult GetMessage([FromRoute] int idOfGroup,[FromRoute] int idOfMessage)
        {
            MessageResponse response = new MessageResponse();
            return Ok(response);
        }

        /// <summary>
        /// Edites message by id
        /// </summary>
        [HttpPut]
        [Route("message/{idOfMessage}")]
        public IActionResult EditMessage([FromBody]EditMessageRequest message,
                [FromRoute] int idOfGroup, [FromRoute] int idOfMessage)
        {
            return Ok($"Текст сообщения {idOfMessage} исправлен на '{message.NewText}' " +
                $"в чате группы {idOfGroup}");
        }

        /// <summary>
        /// Deletes message by id
        /// </summary>
        [HttpDelete]
        [Route("message/{idOfMessage}")]
        public IActionResult DeleteMessage([FromRoute]int idOfGroup, [FromRoute]int idOfMessage)
        {
            return Ok($"Сообщение {idOfMessage} удалено из чата группы {idOfGroup}");
        }
    }
}