using EduHub.Extensions;
using EduHub.Models.AdminControllerModels;
using EduHub.Models.Tools;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Facades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace EduHub.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/administration")]
    public class AdminController : Controller
    {
        private readonly IReportFacade _reportFacade;
        private readonly IUserFacade _userFacade;

        public AdminController(IReportFacade reportFacade, IUserFacade userFacade)
        {
            _reportFacade = reportFacade;
            _userFacade = userFacade;
        }

        /// <summary>
        ///     Generates invitation for admin account
        /// </summary>
        [HttpPost]
        [Route("{userId}/invitation")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult GenerateInvitation([FromRoute] int userId)
        {
            return Ok();
        }

        /// <summary>
        ///     Makes user admin
        /// </summary>
        [HttpPost]
        [Route("{userId}")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult AddAdmin([FromRoute] int userId)
        {
            return Ok();
        }

        /// <summary>
        ///     Demotes moderator to user
        /// </summary>
        [HttpDelete]
        [Route("{userId}")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult DemoteModerator([FromRoute] int userId)
        {
            var pastModerator = Request.GetUserId();
            _userFacade.DemoteModerator(pastModerator);
            return Ok();
        }

        [HttpGet]
        [Route("reports")]
        [SwaggerResponse(200, Type = typeof(ReportModel))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult GetAllReports()
        {
            var reports = new List<ReportModel>();
            _reportFacade.GetAll().ToList().ForEach(r => reports.Add(new ReportModel(r.SenderName, 
                r.SuspectedName, r.Reason, r.Description)));
            var response = new AllReportsResponse(reports);

            return Ok(response);
        }
    }
}