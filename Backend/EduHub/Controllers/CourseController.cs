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

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/group/{groupId}/course")]
    public class CourseController : Controller
    {
        [HttpPost]
        [Route("teacher")]
        public IActionResult InviteTeacher([FromBody]Guid userId, [FromRoute] Guid groupId)
        {
            _userFacade.Invite(Guid.NewGuid(), userId, groupId, MemberRole.Teacher);
            return Ok($"Преподаватель приглашен");
        }

        [HttpPut]
        [Route("teacher")]
        public IActionResult AcceptInvintation([FromRoute] Guid groupId)
        {
            return Ok("Преподаватель принял приглашение");
        }

        [HttpDelete]
        [Route("teacher")]
        public IActionResult RejectInvintation([FromRoute] Guid groupId)
        {
            return Ok("Преподаватель отклонил приглашение");
        }

        [HttpPost]
        [Route("curriculum")]
        public IActionResult SuggestCurriculum([FromBody]OfferCurriculum curriculum, [FromRoute] Guid groupId)
        {
            Ensure.Any.IsNotNull(curriculum);
            _groupFacade.OfferCourse(curriculum.userId, groupId, curriculum.Description);
            return Ok();
        }

        [HttpPut]
        [Route("curriculum/{userId}")]
        public IActionResult ApproveCurriculum([FromRoute] Guid groupId, [FromRoute] Guid userId)
        {
            _groupFacade.AcceptCourse(userId, groupId);
            return Ok();
        }

        [HttpDelete]
        [Route("curriculum")]
        public IActionResult RejectCurriculum([FromRoute] Guid groupId)
        {
            return Ok("Учебный план отклонен");
        }

        [HttpDelete]
        public IActionResult CloseCourse([FromRoute] Guid groupId)
        {
            return Ok("Курс закрыт");
        }

        [HttpPost]
        [Route("review")]
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