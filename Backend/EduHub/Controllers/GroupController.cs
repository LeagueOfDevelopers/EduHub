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
using Microsoft.AspNetCore.Authorization;
using EduHub.Security;
using Microsoft.AspNetCore.Identity;
using EnsureThat;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/group")]
    public class GroupController : Controller
    {
        [HttpPost]
        [SwaggerResponse(200, typeof(CreateGroupResponse))]
        public IActionResult AddGroup([FromBody]CreateGroupRequest newGroup)
        {
            Ensure.Any.IsNotNull(newGroup, nameof(newGroup), 
                opt=> opt.WithException(new ArgumentNullException(nameof(newGroup))));
            Guid newId =_groupFacade.CreateGroup(newGroup.CreatorId, newGroup.Title, newGroup.Tags, newGroup.Description,
                newGroup.Size, newGroup.TotalValue);
            CreateGroupResponse response = new CreateGroupResponse(newId);
            return Ok(response);
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
        [Authorize]
        public IActionResult All()
        {
            return Ok(_groupFacade.GetGroups());
        }

        [HttpGet]
        [SwaggerResponse(200, Type = typeof(GroupResponse))]
        [Route("{idOfGroup}")]
        public IActionResult GetGroup([FromRoute] Guid idOfGroup)
        {
            Group group = _groupFacade.GetGroup(idOfGroup);
            IEnumerable<Member> membersOfGroup = _groupFacade.GetMembersOfGroup(idOfGroup);
            GroupResponse response = new GroupResponse(group.Title, group.Description, group.IsActive, group.Tags,
                membersOfGroup, group.TotalValue, group.Size);
            return Ok(response);
        }

        public GroupController(IGroupFacade groupFacade)
        {
            _groupFacade = groupFacade;
        }
        
        private readonly IGroupFacade _groupFacade;
    }
}