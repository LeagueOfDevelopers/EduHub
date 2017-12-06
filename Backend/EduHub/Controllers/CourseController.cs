using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EduHub.Models;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/group/{idOfGroup}/course")]
    public class CourseController : Controller
    {
        [HttpPost]
        [Route("teacher")]
        public IActionResult InviteTeacher([FromBody]User user)
        {
            return Ok($"Преподаватель {user.Name} приглашен на курс");
        }

        [HttpPut]
        [Route("teacher")]
        public IActionResult AcceptInvintation()
        {
            return Ok("Преподаватель принял приглашение");
        }

        [HttpDelete]
        [Route("teacher")]
        public IActionResult RejectInvintation()
        {
            return Ok("Преподаватель отклонил приглашение");
        }

        [HttpPost]
        [Route("curriculum")]
        public IActionResult SuggestCurriculum([FromBody]Curriculum curriculum)
        {
            return Ok($"Предложен учебный план '{curriculum.Name}' продолжительностью {curriculum.Duration} дней");
        }

        [HttpPut]
        [Route("curriculum")]
        public IActionResult ApproveCurriculum()
        {
            return Ok("Учебный план одобрен");
        }

        [HttpDelete]
        [Route("curriculum")]
        public IActionResult RejectCurriculum()
        {
            return Ok("Учебный план отклонен");
        }

        [HttpDelete]
        public IActionResult CloseCourse()
        {
            return Ok("Курс закрыт");
        }

        [HttpPost]
        [Route("review")]
        public IActionResult LeaveReview([FromBody]Review review)
        {
            return Ok($"Отзыв '{review.Opinion}' с оценкой {review.Rating} был добавлен");
        }
    }
}