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
        public IActionResult SendMessage([FromBody]SendMessageRequest message)
        {
            return Ok($"Пользователю {message.Receiver} было отправлено сообщение '{message.Text}'");
        }

        [HttpGet]
        [Route("message/{idOfMessage}")]
        public IActionResult GetMessage()
        {
            return Ok($"Получено сообщение");
        }

        [HttpPut]
        [Route("message/{idOfMessage}")]
        public IActionResult EditMessage([FromBody]EditMessageRequest message)
        {
            return Ok($"Текст сообщения исправлен на '{message.NewText}'");
        }

        [HttpDelete]
        [Route("message/{idOfMessage}")]
        public IActionResult DeleteMessage()
        {
            return Ok($"Сообщение удалено");
        }
    }
}