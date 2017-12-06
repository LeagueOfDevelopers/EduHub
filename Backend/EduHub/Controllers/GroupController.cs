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
    [Route("api/group")]
    public class GroupController : Controller
    {
        [HttpPost]
        public IActionResult AddQuery([FromBody]Group group)
        {
            return Ok($"Запрос на обучение с названием {group.Name}, описанием {group.Description} и вместимостью {group.Count} отправлен");
        }

        [HttpPost]
        [Route("search")]
        public IActionResult SearchGroup([FromBody]SearchOfGroupRequest request)
        {
            return Ok($"Поиск курса {request.Name} осуществлен");
        }

        [HttpPut]
        [Route("{idOfGroup}")]
        public IActionResult EditGroupDescription([FromBody]EditDescriptionOfGroupRequest request)
        {
            return Ok($"Описание группы изменено на {request.NewText}");
        }

        [HttpDelete]
        [Route("{idOfGroup}")]
        public IActionResult DeleteGroup()
        {
            return Ok("Группа удалена");
        }
    }
}