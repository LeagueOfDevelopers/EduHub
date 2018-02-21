using System;
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
        private readonly IGroupEditFacade _groupEditFacade;
        private readonly IGroupFacade _groupFacade;

        private readonly IUserFacade _userFacade;

        public GroupController(IGroupFacade groupFacade, IUserFacade userFacade,
            IGroupEditFacade groupEditFacade)
        {
            _groupFacade = groupFacade;
            _userFacade = userFacade;
            _groupEditFacade = groupEditFacade;
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
            var userId = Request.GetUserId();
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
                    group.GroupInfo.Size, group.GroupInfo.Price, group.GroupInfo.GroupType,
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
        public IActionResult EditGroupTitle([FromBody] EditGroupTitleRequest request, [FromRoute] Guid groupId)
        {
            var userId = Request.GetUserId();
            _groupEditFacade.ChangeGroupTitle(groupId, userId, request.GroupTitle);
            return Ok();
        }

        /// <summary>
        ///     Changes group description
        /// </summary>
        [Authorize]
        [HttpPut]
        [Route("{groupId}/description")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult EditGroupDescription([FromBody] EditGroupDescriptionRequest request,
            [FromRoute] Guid groupId)
        {
            var userId = Request.GetUserId();
            _groupEditFacade.ChangeGroupDescription(groupId, userId, request.GroupDescription);
            return Ok();
        }

        /// <summary>
        ///     Changes group tags
        /// </summary>
        [Authorize]
        [HttpPut]
        [Route("{groupId}/tags")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult EditGroupTags([FromBody] EditGroupTagsRequest request, [FromRoute] Guid groupId)
        {
            var userId = Request.GetUserId();
            _groupEditFacade.ChangeGroupTags(groupId, userId, request.GroupTags);
            return Ok();
        }

        /// <summary>
        ///     Changes group size
        /// </summary>
        [Authorize]
        [HttpPut]
        [Route("{groupId}/size")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult EditGroupSize([FromBody] EditGroupSizeRequest request, [FromRoute] Guid groupId)
        {
            var userId = Request.GetUserId();
            _groupEditFacade.ChangeGroupSize(groupId, userId, request.GroupSize);
            return Ok();
        }

        /// <summary>
        ///     Changes group price
        /// </summary>
        [Authorize]
        [HttpPut]
        [Route("{groupId}/price")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult EditGroupPrice([FromBody] EditGroupPriceRequest request, [FromRoute] Guid groupId)
        {
            var userId = Request.GetUserId();
            _groupEditFacade.ChangeGroupPrice(groupId, userId, request.GroupPrice);
            return Ok();
        }

        /// <summary>
        ///     Changes group type
        /// </summary>
        [Authorize]
        [HttpPut]
        [Route("{groupId}/type")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult EditGroupType([FromBody] EditGroupTypeRequest request, [FromRoute] Guid groupId)
        {
            var userId = Request.GetUserId();
            _groupEditFacade.ChangeGroupType(groupId, userId, request.GroupType);
            return Ok();
        }

        /// <summary>
        ///     Changes group privacy
        /// </summary>
        [Authorize]
        [HttpPut]
        [Route("{groupId}/privacy")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult EditGroupPrivacy([FromBody] EditGroupPrivacyRequest request, [FromRoute] Guid groupId)
        {
            var userId = Request.GetUserId();
            _groupEditFacade.ChangeGroupPrivacy(groupId, userId, request.IsPrivate);
            return Ok();
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
            var fillingGroupList = new List<MinItemGroupResponse>();
            var fullGroupList = new List<MinItemGroupResponse>();
            groups.ToList().ForEach(g =>
            {
                var memberAmount = _groupFacade.GetGroupMembers(g.GroupInfo.Id).ToList().Count;
                var groupInfo = new MinGroupInfo(g.GroupInfo.Id, g.GroupInfo.Title, memberAmount, g.GroupInfo.Size,
                    g.GroupInfo.Price, g.GroupInfo.GroupType, g.GroupInfo.Tags);
                if (memberAmount == g.GroupInfo.Size)
                    fullGroupList.Add(new MinItemGroupResponse(groupInfo));
                else
                    fillingGroupList.Add(new MinItemGroupResponse(groupInfo));
            });
            var response = new MinFilterGroupResponse(fullGroupList, fillingGroupList);
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
                listOfMembers.Count, group.GroupInfo.Price, group.GroupInfo.GroupType, group.GroupInfo.Tags,
                group.GroupInfo.Description, group.Status, group.GroupInfo.IsPrivate);
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