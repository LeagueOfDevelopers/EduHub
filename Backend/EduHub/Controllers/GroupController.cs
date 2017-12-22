using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EduHub.Models;
using EduHubLibrary.Facades;
using EduHubLibrary.Domain;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/group")]
    public class GroupController : Controller
    {
        [HttpPost]
        public IActionResult AddQuery([FromBody]User user)
        {
            _groupFacade.CreateGroup(user);
            return Ok($"Группа создана");
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

        [HttpGet]
        public IActionResult All()
        {
            return Ok(_groupFacade.GetGroups());
        }

        [HttpGet]
        [Route("{idOfGroup}")]
        public IActionResult GetGroup([FromRoute] Guid idOfGroup)
        {
            return Ok(_groupFacade.GetGroup(idOfGroup));
        }

        public GroupController(IGroupFacade groupFacade)
        {
            _groupFacade = groupFacade;
        }
        
        private readonly IGroupFacade _groupFacade;
    }
}