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
using Swashbuckle.AspNetCore.Examples;
using EduHub.Models.Examples;
using EduHub.Extensions;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/group")]
    public class GroupController : Controller
    {
        /// <summary>
        /// Adds groups with all parameters
        /// </summary>
        ///<response code="200">Group Created!</response>
        [Authorize]
        [HttpPost]
        [SwaggerResponse(200, typeof(CreateGroupResponse))]
        [SwaggerRequestExample(typeof(CreateGroupRequest), typeof(CreateGroupRequestExample))]
        public IActionResult AddGroup([FromBody]CreateGroupRequest newGroup)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            Ensure.Any.IsNotNull(newGroup, nameof(newGroup), 
                opt=> opt.WithException(new ArgumentNullException(nameof(newGroup))));
            Guid newId =_groupFacade.CreateGroup(userId, newGroup.Title, newGroup.Tags, newGroup.Description,
                newGroup.Size, newGroup.MoneyPerUser, newGroup.IsPrivate, newGroup.GroupType);
            CreateGroupResponse response = new CreateGroupResponse(newId);
            return Ok(response);
        }

        /// <summary>
        /// Searches groups 
        /// </summary>
        [HttpPost]
        [Route("search")]
        public IActionResult SearchGroupByTag([FromBody]SearchOfGroupRequest request)
        {
            return Ok($"Поиск групп с тегом {request.Tag} осуществлен");
        }

        /// <summary>
        /// Changes group title
        /// </summary>
        [Authorize]
        [HttpPut]
        [Route("{groupId}/title")]
        public IActionResult EditGroupTitle([FromBody]string newTitle, [FromRoute]Guid groupId)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            _groupFacade.ChangeGroupTitle(groupId, userId, newTitle);
            return Ok($"Название группы изменено на {newTitle}");
        }

        /// <summary>
        /// Changes group description
        /// </summary>
        [Authorize]
        [HttpPut]
        [Route("{groupId}/description")]
        public IActionResult EditGroupDescription([FromBody]string newDescription, [FromRoute]Guid groupId)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            _groupFacade.ChangeGroupDescription(groupId, userId, newDescription);
            return Ok($"Описание группы изменено на {newDescription}");
        }

        /// <summary>
        /// Changes group tags
        /// </summary>
        [Authorize]
        [HttpPut]
        [Route("{groupId}/tags")]
        public IActionResult EditGroupTags([FromBody]List<string> newTags, [FromRoute]Guid groupId)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            _groupFacade.ChangeGroupTags(groupId, userId, newTags);
            return Ok($"Теги группы изменены");
        }
        
        /// <summary>
        /// Changes group size
        /// </summary>
        [Authorize]
        [HttpPut]
        [Route("{groupId}/size")]
        public IActionResult EditGroupSize([FromBody]int newSize, [FromRoute]Guid groupId)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            _groupFacade.ChangeGroupSize(groupId, userId, newSize);
            return Ok($"Размер группы изменен на {newSize}");
        }

        /// <summary>
        /// Changes group price
        /// </summary>
        [Authorize]
        [HttpPut]
        [Route("{groupId}/price")]
        public IActionResult EditGroupPrice([FromBody]double newPrice, [FromRoute]Guid groupId)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            _groupFacade.ChangeGroupPrice(groupId, userId, newPrice);
            return Ok($"Оплата за обучение в группе изменена на {newPrice}");
        }

        /// <summary>
        /// Deletes group
        /// </summary>
        [Authorize]
        [HttpDelete]
        [Route("{groupId}")]
        public IActionResult DeleteGroup([FromRoute] int groupId)
        {
            return Ok($"Группа {groupId} удалена");
        }

        /// <summary>
        /// Returns all groups without any filters (for now)
        /// </summary>
        ///<response code="200">Get your groups</response>
        [HttpGet]
        [SwaggerResponse(200, Type = typeof(MinGroupResponse))]
        public IActionResult All()
        {
            IEnumerable<Group> groups =  _groupFacade.GetGroups();
            List<MinItemGroupResponse> items = new List<MinItemGroupResponse>();
            groups.ToList().ForEach(g => items.Add(new MinItemGroupResponse(g.GroupInfo, _groupFacade.GetMembersOfGroup(g.GroupInfo.Id).Count<Member>())));
            MinGroupResponse response = new MinGroupResponse(items);
            return Ok(response);
        }

        /// <summary>
        /// Returns full information about one group
        /// </summary>
        [HttpGet]
        [Route("{groupId}")]
        [SwaggerResponse(200, Type = typeof(GroupResponse))]
        public IActionResult GetGroup([FromRoute] Guid groupId)
        {
            Group group = _groupFacade.GetGroup(groupId);
            IEnumerable<Member> membersOfGroup = _groupFacade.GetMembersOfGroup(groupId);
            GroupResponse response = new GroupResponse(group.GroupInfo, group.Status, group.Teacher, membersOfGroup, _userFacade);
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