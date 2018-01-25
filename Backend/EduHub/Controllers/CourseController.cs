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

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/group/{groupId}/course")]
    public class CourseController : Controller
    {
        /// <summary>
        /// Invites teacher to group
        /// </summary>
        [Authorize]
        [HttpPost]
        [Route("teacher")]
        public IActionResult InviteTeacher([FromBody]Guid teacherId, [FromRoute] Guid groupId)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();

            _userFacade.Invite(userId, teacherId, groupId, MemberRole.Teacher);
            return Ok($"Преподаватель {teacherId} приглашен");
        }

        /// <summary>
        /// Accept invitation
        /// </summary>
        [Authorize]
        [HttpPut]
        [Route("teacher")]
        public IActionResult AcceptInvitation([FromRoute] Guid groupId)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();

            Invitation invitation = _userFacade.GetUser(userId).ListOfInvitations.Find(o => o.GroupId.Equals(groupId));
            _groupFacade.ApproveTeacher(userId, groupId);
            _userFacade.ChangeStatusOfInvitation(userId, invitation.Id, InvitationStatus.Accepted);
            
            return Ok("Преподаватель принял приглашение");
        }

        /// <summary>
        /// Rejects invitation
        /// </summary>
        [HttpDelete]
        [Route("teacher")]
        public IActionResult RejectInvintation([FromRoute] Guid groupId)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();

            Invitation invitation = _userFacade.GetUser(userId).ListOfInvitations.Find(o => o.GroupId.Equals(groupId));
            _userFacade.ChangeStatusOfInvitation(userId, invitation.Id, InvitationStatus.Declined);
            return Ok("Преподаватель отклонил приглашение");
        }

        /// <summary>
        /// Adds suggesting plan for group
        /// </summary>
        [HttpPost]
        [Route("curriculum")]
        public IActionResult SuggestCurriculum([FromBody]OfferCurriculum curriculum, [FromRoute] Guid groupId)
        {
            Ensure.Any.IsNotNull(curriculum);
            _groupFacade.OfferCurriculum(curriculum.UserId, groupId, curriculum.Description);
            return Ok($"В группу был предложен учебный план '{curriculum.Description}'");
        }

        /// <summary>
        /// Approves plan for group
        /// </summary>
        [HttpPut]
        [Route("curriculum/{userId}")]
        public IActionResult ApproveCurriculum([FromRoute] Guid groupId, [FromRoute] Guid userId)
        {
            _groupFacade.AcceptCurriculum(userId, groupId);
            return Ok("Учебный план утвержден");
        }

        /// <summary>
        /// Rejects plan for group
        /// </summary>
        [HttpDelete]
        [Route("curriculum")]
        public IActionResult RejectCurriculum([FromRoute] Guid groupId)
        {
            return Ok("Учебный план отклонен");
        }

        /// <summary>
        /// Closes course for group
        /// </summary>
        [HttpDelete]
        public IActionResult CloseCourse([FromRoute] Guid groupId)
        {
            _groupFacade.GetGroup(groupId).Status = CourseStatus.Finished;
            return Ok("Курс закрыт");
        }

        /// <summary>
        /// Adds review for teacher by course
        /// </summary>
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