using EduHub.Extensions;
using EduHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/group/{idOfGroup}/chat")]
    public class ChatController : Controller
    {
        /// <summary>
        ///     Sends message to group chat
        /// </summary>
        [HttpPost]
        [Authorize]
        [Route("message")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult SendMessage([FromBody] SendMessageRequest message, [FromRoute] int groupId)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            return Ok($"Чату {message.ChatId} было отправлено сообщение '{message.Text}' от пользователя {userId}");
        }

        /// <summary>
        ///     Returns message by id
        /// </summary>
        [HttpGet]
        [SwaggerResponse(200, Type = typeof(MessageResponse))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [Route("message/{messageId}")]
        public IActionResult GetMessage([FromRoute] int groupId, [FromRoute] int messageId)
        {
            var response = new MessageResponse();
            return Ok(response);
        }

        /// <summary>
        ///     Edites message by id
        /// </summary>
        [Authorize]
        [HttpPut]
        [Route("message/{messageId}")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult EditMessage([FromBody] EditMessageRequest message,
            [FromRoute] int groupId, [FromRoute] int messageId)
        {
            return Ok($"Текст сообщения {messageId} исправлен на '{message.NewText}' " +
                      $"в чате группы {groupId}");
        }

        /// <summary>
        ///     Deletes message by id
        /// </summary>
        [Authorize]
        [HttpDelete]
        [Route("message/{messageId}")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult DeleteMessage([FromRoute] int groupId, [FromRoute] int messageId)
        {
            return Ok($"Сообщение {messageId} удалено из чата группы {groupId}");
        }
    }
}