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
            _groupFacade.CreateGroup(newGroup.IdOfCreator, newGroup.Title, newGroup.Tags, newGroup.Description,
                newGroup.Size, newGroup.totalValue);
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
        [SwaggerResponse(200, Type = typeof(GroupResponse))]
        [SwaggerResponse(400, Type = typeof(ErrorResponse))]
        [Route("{idOfGroup}")]
        public IActionResult GetGroup([FromRoute] Guid idOfGroup)
        {
            try
            {
                Group group = _groupFacade.GetGroup(idOfGroup);
                IEnumerable<Member> membersOfGroup = _groupFacade.GetMembersOfGroup(idOfGroup);
                GroupResponse response = new GroupResponse(group.Title, group.Description, group.IsActive, group.Tags,
                    membersOfGroup, group.TotalValue, group.Size);
                return Ok(response);
            }
            catch (Exception e)
            {
                ErrorResponse err = new ErrorResponse() { Message = e.Message };
                return BadRequest(err);
            }
        }

        public GroupController(IGroupFacade groupFacade)
        {
            _groupFacade = groupFacade;
        }
        
        private readonly IGroupFacade _groupFacade;
    }
}