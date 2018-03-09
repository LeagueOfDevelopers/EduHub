using System;
using EduHub.Extensions;
using EduHub.Models;
using EduHubLibrary.Facades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EduHub.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/group/{groupId}/course")]
    public class CourseController : Controller
    {
        private readonly IGroupFacade _groupFacade;
        private readonly IUserFacade _userFacade;

        public CourseController(IGroupFacade groupFacade, IUserFacade userFacade)
        {
            _groupFacade = groupFacade;
            _userFacade = userFacade;
        }

        /// <summary>
        ///     Adds suggesting plan for group
        /// </summary>
        [HttpPost]
        [Route("curriculum")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult SuggestCurriculum([FromBody] OfferCurriculum curriculum, [FromRoute] int groupId)
        {
            var userId = Request.GetUserId();
            _groupFacade.OfferCurriculum(userId, groupId, curriculum.Description);
            return Ok();
        }

        /// <summary>
        ///     Approves plan for group
        /// </summary>
        [HttpPut]
        [Route("curriculum")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult ApproveCurriculum([FromRoute] int groupId)
        {
            var userId = Request.GetUserId();
            _groupFacade.AcceptCurriculum(userId, groupId);
            return Ok();
        }

        /// <summary>
        ///     Rejects plan for group
        /// </summary>
        [HttpDelete]
        [Route("curriculum")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult RejectCurriculum([FromRoute] int groupId,
            [FromBody] DeclineCurriculumRequest request)
        {
            var userId = Request.GetUserId();
            _groupFacade.DeclineCurriculum(userId, groupId, request.Reason);
            return Ok();
        }

        /// <summary>
        ///     Closes course for group
        /// </summary>
        [HttpDelete]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult CloseCourse([FromRoute] int groupId)
        {
            var userId = Request.GetUserId();
            _groupFacade.FinishCurriculum(groupId, userId);
            return Ok();
        }

        /// <summary>
        ///     Adds review for teacher by course
        /// </summary>
        [HttpPost]
        [Route("review")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult LeaveReview([FromBody] ReviewRequest review, [FromRoute] int groupId)
        {
            var userId = Request.GetUserId();
            _groupFacade.AddReview(groupId, userId, review.Title, review.Text);
            return Ok();
        }
    }
}