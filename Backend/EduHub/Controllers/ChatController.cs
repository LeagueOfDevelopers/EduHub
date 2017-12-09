using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EduHub.Models;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/group/{idOfGroup}/chat")]
    public class ChatController : Controller
    {
        [HttpPost]
        [Route("message")]
        public IActionResult SendMessage([FromBody]SendMessageRequest message, [FromRoute]int idOfGroup)
        {
            return Ok($"Чату {message.ChatId} было отправлено сообщение '{message.Text}'");
        }

        [HttpGet]
        [Route("message/{idOfMessage}")]
        public IActionResult GetMessage([FromRoute] int idOfGroup,[FromRoute] int idOfMessage)
        {
            return Ok($"Получено сообщение {idOfMessage} в чате группы {idOfGroup}");
        }

        [HttpPut]
        [Route("message/{idOfMessage}")]
        public IActionResult EditMessage([FromBody]EditMessageRequest message,
                [FromRoute] int idOfGroup, [FromRoute] int idOfMessage)
        {
            return Ok($"Текст сообщения {idOfMessage} исправлен на '{message.NewText}' " +
                $"в чате группы {idOfGroup}");
        }

        [HttpDelete]
        [Route("message/{idOfMessage}")]
        public IActionResult DeleteMessage([FromRoute]int idOfGroup, [FromRoute]int idOfMessage)
        {
            return Ok($"Сообщение {idOfMessage} удалено из чата группы {idOfGroup}");
        }
    }
}