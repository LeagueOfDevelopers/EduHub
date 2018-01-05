using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EduHub.Models;
using EduHubLibrary.Facades;
using EduHubLibrary.Domain;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Authorization;
using EnsureThat;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/group")]
    public class GroupController : Controller
    {
        [Authorize]
        [HttpPost]
        [SwaggerResponse(200, typeof(CreateGroupResponse))]
        public IActionResult AddGroup([FromBody]CreateGroupRequest newGroup)
        {
            var handler = new JwtSecurityTokenHandler();
            string a = Request.Headers["Authorization"];
            var userId = Guid.Parse(handler.ReadJwtToken(a.Substring(7)).Claims.First(c => c.Type == "UserId").Value);
            Ensure.Any.IsNotNull(newGroup, nameof(newGroup), 
                opt=> opt.WithException(new ArgumentNullException(nameof(newGroup))));
            Guid newId =_groupFacade.CreateGroup(userId, newGroup.Title, newGroup.Tags, newGroup.Description,
                newGroup.Size, newGroup.MoneyPerUser, newGroup.IsPrivate, newGroup.GroupType);
            CreateGroupResponse response = new CreateGroupResponse(newId);
            return Ok(response);
        }

        [HttpPost]
        [Route("search")]
        public IActionResult SearchGroupByTag([FromBody]SearchOfGroupRequest request)
        {
            return Ok($"Поиск групп с тегом {request.Tag} осуществлен");
        }

        [Authorize]
        [HttpPut]
        [Route("{idOfGroup}")]
        public IActionResult EditGroupDescription([FromBody]EditDescriptionOfGroupRequest request, [FromRoute] int idOfGroup)
        {
            return Ok($"Описание группы с id {idOfGroup} изменено на {request.NewText}");
        }

        [Authorize]
        [HttpDelete]
        [Route("{idOfGroup}")]
        public IActionResult DeleteGroup([FromRoute] int idOfGroup)
        {
            return Ok($"Группа {idOfGroup} удалена");
        }
        
        //TODO delete
        [HttpGet]
        [SwaggerResponse(200, Type = typeof(MinGroupResponse))]
        public IActionResult All()
        {
            IEnumerable<Group> groups =  _groupFacade.GetGroups();
            List<MinItemGroupResponse> items = new List<MinItemGroupResponse>();
            groups.ToList().ForEach(g => items.Add(new MinItemGroupResponse(g.GroupInfo)));
            MinGroupResponse response = new MinGroupResponse(items);
            return Ok(response);
        }

        [HttpGet]
        [Route("{idOfGroup}")]
        [SwaggerResponse(200, Type = typeof(GroupResponse))]
        public IActionResult GetGroup([FromRoute] Guid idOfGroup)
        {
            Group group = _groupFacade.GetGroup(idOfGroup);
            IEnumerable<Member> membersOfGroup = _groupFacade.GetMembersOfGroup(idOfGroup);
            GroupResponse response = new GroupResponse(group.GroupInfo, group.Course, group.Teacher, membersOfGroup);
            return Ok(response);
        }

        public GroupController(IGroupFacade groupFacade, IUserFacade userFacade)
        {
            _groupFacade = groupFacade;
            _userFacade = userFacade;
        }
        
        private readonly IGroupFacade _groupFacade;
        private readonly IUserFacade _userFacade;

    }
}