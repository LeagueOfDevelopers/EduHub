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
        public IActionResult SearchGroupByTag([FromBody]SearchOfGroupRequest request)
        {
            return Ok($"Поиск групп с тегом {request.Tag} осуществлен");
        }

        [HttpPut]
        [Route("{idOfGroup}")]
        public IActionResult EditGroupDescription([FromBody]EditDescriptionOfGroupRequest request, [FromRoute] int idOfGroup)
        {
            return Ok($"Описание группы с id {idOfGroup} изменено на {request.NewText}");
        }

        [HttpDelete]
        [Route("{idOfGroup}")]
        public IActionResult DeleteGroup([FromRoute] int idOfGroup)
        {
            return Ok($"Группа {idOfGroup} удалена");
        }
    }
}