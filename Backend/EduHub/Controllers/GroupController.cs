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
using EduHub.Models.GroupControllerModels;

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
        ///     Searches groups with tags
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
        ///     Searches groups
        /// </summary>
        [HttpGet]
        [Route("search")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult SearchGroup(
            [FromQuery] double minPrice,
            [FromQuery] double maxPrice,
            [FromQuery] string title = "",
            [FromQuery] List<string> tags = null,
            [FromQuery] GroupType type = GroupType.Default,
            [FromQuery] bool formed = false)
        {
            var response = new List<MinItemGroupResponse>();
            var foundGroups = _groupFacade.FindGroup(title, tags, type, minPrice, maxPrice, formed);

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
        public IActionResult EditGroupTitle([FromBody] EditGroupTitleRequest request, [FromRoute] int groupId)
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
            [FromRoute] int groupId)
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
        public IActionResult EditGroupTags([FromBody] EditGroupTagsRequest request, [FromRoute] int groupId)
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
        public IActionResult EditGroupSize([FromBody] EditGroupSizeRequest request, [FromRoute] int groupId)
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
        public IActionResult EditGroupPrice([FromBody] EditGroupPriceRequest request, [FromRoute] int groupId)
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
        public IActionResult EditGroupType([FromBody] EditGroupTypeRequest request, [FromRoute] int groupId)
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
        public IActionResult EditGroupPrivacy([FromBody] EditGroupPrivacyRequest request, [FromRoute] int groupId)
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
                var groupInfo = new MinGroupInfo(g.Id, g.Title, g.MemberAmount, g.Size,
                    g.Cost, g.GroupType, g.Tags);
                if (g.MemberAmount == g.Size)
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
        public IActionResult GetGroup([FromRoute] int groupId)
        {
            var group = _groupFacade.GetGroup(groupId);
            var groupInfoView = group.GroupInfoView;
            var groupMembersInfo = group.GroupMemberInfo;
            var fullGroupInfo = new FullGroupInfo(groupInfoView.Title, groupInfoView.Size,
                groupInfoView.MemberAmount, groupInfoView.Price, groupInfoView.GroupType,
                groupInfoView.Tags, groupInfoView.Description, groupInfoView.CourseStatus, groupInfoView.IsPrivate,
                groupInfoView.Curriculum, groupInfoView.VotersAmount);
            var membersInfo = new List<MemberInfo>();
            groupMembersInfo.ToList().ForEach(m =>
                membersInfo.Add(new MemberInfo(m.UserId, m.Username, m.AvatarLink, m.MemberRole,
                    m.Paid, m.CurriculumStatus)));
            var response = new GroupResponse(fullGroupInfo, membersInfo);
            return Ok(response);
        }

        /// <summary>
        ///     Unform group (for admins only)
        /// </summary>
        [Authorize(Policy = "AdminOnly")]
        [HttpDelete]
        [Route("{groupId}")]
        [SwaggerResponse(200, Type = typeof(GroupResponse))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult UnformGroup([FromRoute] int groupId)
        {
            _groupFacade.DeleteGroup(groupId);
            return Ok();
        }
    }
}