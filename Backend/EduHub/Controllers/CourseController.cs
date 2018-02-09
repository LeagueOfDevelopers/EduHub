 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EduHub.Models;
using EduHubLibrary.Facades;
using EnsureThat;
using EduHubLibrary.Domain;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using EduHub.Extensions;
using EduHubLibrary.Domain.Tools;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EduHub.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/group/{groupId}/course")]
    public class CourseController : Controller
    {
        /// <summary>
        /// Adds suggesting plan for group
        /// </summary>
        [HttpPost]
        [Route("curriculum")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult SuggestCurriculum([FromBody]OfferCurriculum curriculum, [FromRoute] Guid groupId)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            _groupFacade.OfferCurriculum(userId, groupId, curriculum.Description);
            return Ok($"В группу был предложен учебный план '{curriculum.Description}'");
        }

        /// <summary>
        /// Approves plan for group
        /// </summary>
        [HttpPut]
        [Route("curriculum")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult ApproveCurriculum([FromRoute] Guid groupId)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();
            _groupFacade.AcceptCurriculum(userId, groupId);
            return Ok("Учебный план утвержден");
        }

        /// <summary>
        /// Rejects plan for group
        /// </summary>
        [HttpDelete]
        [Route("curriculum")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult RejectCurriculum([FromRoute] Guid groupId)
        {
            return Ok("Учебный план отклонен");
        }

        /// <summary>
        /// Closes course for group
        /// </summary>
        [HttpDelete]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult CloseCourse([FromRoute] Guid groupId)
        {
            //_groupFacade.GetGroup(groupId).Status = CourseStatus.Finished;
            return Ok("Курс закрыт");
        }

        /// <summary>
        /// Adds review for teacher by course
        /// </summary>
        [HttpPost]
        [Route("review")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult LeaveReview([FromBody]ReviewRequest review, [FromRoute] Guid groupId)
        {
            return Ok($"Отзыв '{review.Opinion}' с оценкой {review.Rating} был добавлен");
        }

        public CourseController(IGroupFacade groupFacade, IUserFacade userFacade)
        {
            _groupFacade = groupFacade;
            _userFacade = userFacade;
        }

        private readonly IGroupFacade _groupFacade;
        private readonly IUserFacade _userFacade;
    }
}