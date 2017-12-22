using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EduHub.Models;
using EduHubLibrary.Facades;
using EduHubLibrary.Domain;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/group")]
    public class GroupController : Controller
    {
        [HttpPost]
        public IActionResult AddQuery([FromBody]CreateGroupRequest newGroup)
        {
            _groupFacade.CreateGroup(newGroup.IdOfCreator, newGroup.Title, newGroup.Tags, newGroup.Description);
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
        //TODO delete
        [HttpGet]
        public IActionResult All()
        {
            return Ok(_groupFacade.GetGroups());
        }

        [HttpGet]
        [SwaggerResponse(400, Type = typeof(GroupResponse))]
        [Route("{idOfGroup}")]
        public IActionResult GetGroup([FromRoute] Guid idOfGroup)
        {
            Group group = _groupFacade.GetGroup(idOfGroup);
            GroupResponse response = new GroupResponse(group.Title, group.Description, group.IsActive, group.Tags);
            return Ok(response);
        }

        public GroupController(IGroupFacade groupFacade)
        {
            _groupFacade = groupFacade;
        }
        
        private readonly IGroupFacade _groupFacade;
    }
}