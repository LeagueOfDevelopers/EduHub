﻿using System;
using System.Collections.Generic;
using System.Linq;
using EduHub.Extensions;
using EduHub.Models;
using EduHub.Models.Examples;
using EduHub.Models.Tools;
using EduHubLibrary.Domain;
using EduHubLibrary.Facades;
using EnsureThat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Examples;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/group")]
    public class GroupController : Controller
    {
        private readonly IGroupFacade _groupFacade;
        private readonly IUserFacade _userFacade;

        public GroupController(IGroupFacade groupFacade, IUserFacade userFacade)
        {
            _groupFacade = groupFacade;
            _userFacade = userFacade;
        }

        /// <summary>
        ///     Adds groups with all parameters
        /// </summary>
        /// <response code="200">Group Created!</response>
        [Authorize]
        [HttpPost]
        [SwaggerResponse(200, typeof(CreateGroupResponse))]
        [SwaggerRequestExample(typeof(CreateGroupRequest), typeof(CreateGroupRequestExample))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult AddGroup([FromBody] CreateGroupRequest newGroup)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            Ensure.Any.IsNotNull(newGroup, nameof(newGroup),
                opt => opt.WithException(new ArgumentNullException(nameof(newGroup))));
            var newId = _groupFacade.CreateGroup(userId, newGroup.Title, newGroup.Tags, newGroup.Description,
                newGroup.Size, newGroup.MoneyPerUser, newGroup.IsPrivate, newGroup.GroupType);
            var response = new CreateGroupResponse(newId);
            return Ok(response);
        }

        /// <summary>
        ///     Searches groups
        /// </summary>
        [HttpPost]
        [Route("search")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult SearchGroupByTag([FromBody] SearchOfGroupRequest request)
        {
            var response = new List<MinItemGroupResponse>();
            var foundGroups = _groupFacade.FindByTags(request.Tags).ToList();

            foreach (var group in foundGroups)
            {
                var countOfMembers = _groupFacade.GetGroupMembers(group.GroupInfo.Id).Count();
                response.Add(new MinItemGroupResponse(new MinGroupInfo(group.GroupInfo.Id, group.GroupInfo.Title,
                    countOfMembers,
                    group.GroupInfo.Size, group.GroupInfo.MoneyPerUser, group.GroupInfo.GroupType,
                    group.GroupInfo.Tags)));
            }

            return Ok(response);
        }

        /// <summary>
        ///     Changes group title
        /// </summary>
        [Authorize]
        [HttpPut]
        [Route("{groupId}/title")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult EditGroupTitle([FromBody] string newTitle, [FromRoute] Guid groupId)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            _groupFacade.ChangeGroupTitle(groupId, userId, newTitle);
            return Ok($"Название группы изменено на {newTitle}");
        }

        /// <summary>
        ///     Changes group description
        /// </summary>
        [Authorize]
        [HttpPut]
        [Route("{groupId}/description")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult EditGroupDescription([FromBody] string newDescription, [FromRoute] Guid groupId)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            _groupFacade.ChangeGroupDescription(groupId, userId, newDescription);
            return Ok($"Описание группы изменено на {newDescription}");
        }

        /// <summary>
        ///     Changes group tags
        /// </summary>
        [Authorize]
        [HttpPut]
        [Route("{groupId}/tags")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult EditGroupTags([FromBody] List<string> newTags, [FromRoute] Guid groupId)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            _groupFacade.ChangeGroupTags(groupId, userId, newTags);
            return Ok($"Теги группы изменены");
        }

        /// <summary>
        ///     Changes group size
        /// </summary>
        [Authorize]
        [HttpPut]
        [Route("{groupId}/size")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult EditGroupSize([FromBody] int newSize, [FromRoute] Guid groupId)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            _groupFacade.ChangeGroupSize(groupId, userId, newSize);
            return Ok($"Размер группы изменен на {newSize}");
        }

        /// <summary>
        ///     Changes group price
        /// </summary>
        [Authorize]
        [HttpPut]
        [Route("{groupId}/price")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult EditGroupPrice([FromBody] double newPrice, [FromRoute] Guid groupId)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            _groupFacade.ChangeGroupPrice(groupId, userId, newPrice);
            return Ok($"Оплата за обучение в группе изменена на {newPrice}");
        }

        /// <summary>
        ///     Deletes group
        /// </summary>
        [Authorize]
        [HttpDelete]
        [Route("{groupId}")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult DeleteGroup([FromRoute] int groupId)
        {
            return Ok($"Группа {groupId} удалена");
        }

        /// <summary>
        ///     Returns full and filling groups
        /// </summary>
        /// <response code="200">Get groups</response>
        [HttpGet]
        [SwaggerResponse(200, Type = typeof(MinFilterGroupResponse))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult All()
        {
            var groups = _groupFacade.GetGroups();
            var FillingGroupList = new List<MinItemGroupResponse>();
            var fullGroupList = new List<MinItemGroupResponse>();
            groups.ToList().ForEach(g =>
            {
                var memberAmount = _groupFacade.GetGroupMembers(g.GroupInfo.Id).ToList().Count;
                var groupInfo = new MinGroupInfo(g.GroupInfo.Id, g.GroupInfo.Title, memberAmount, g.GroupInfo.Size,
                    g.GroupInfo.MoneyPerUser, g.GroupInfo.GroupType, g.GroupInfo.Tags);
                if (memberAmount == g.GroupInfo.Size)
                    fullGroupList.Add(new MinItemGroupResponse(groupInfo));
                else
                    FillingGroupList.Add(new MinItemGroupResponse(groupInfo));
            });
            var response = new MinFilterGroupResponse(fullGroupList, FillingGroupList);
            return Ok(response);
        }

        /// <summary>
        ///     Returns full information about one group
        /// </summary>
        [HttpGet]
        [Route("{groupId}")]
        [SwaggerResponse(200, Type = typeof(GroupResponse))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult GetGroup([FromRoute] Guid groupId)
        {
            var group = _groupFacade.GetGroup(groupId);
            var listOfMembers = _groupFacade.GetGroupMembers(groupId).ToList();
            var groupInfo = new FullGroupInfo(group.GroupInfo.Title, group.GroupInfo.Size,
                listOfMembers.Count, group.GroupInfo.MoneyPerUser, group.GroupInfo.GroupType, group.GroupInfo.Tags,
                group.GroupInfo.Description, group.Status);
            var memberInfoList = new List<MemberInfo>();
            if (group.Teacher != null)
            {
                var info = new MemberInfo(group.Teacher.Id, group.Teacher.UserProfile.Name,
                    group.Teacher.UserProfile.AvatarLink, MemberRole.Teacher, false);
                memberInfoList.Add(info);
            }

            listOfMembers.ForEach(m =>
            {
                var userName = _userFacade.GetUser(m.UserId).UserProfile.Name;
                var avatarLink = _userFacade.GetUser(m.UserId).UserProfile.AvatarLink;

                var memberInfo = new MemberInfo(m.UserId, userName, avatarLink, m.MemberRole, m.Paid);
                memberInfoList.Add(memberInfo);
            });
            var response = new GroupResponse(groupInfo, memberInfoList);
            return Ok(response);
        }
    }
}