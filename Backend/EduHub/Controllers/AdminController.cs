using System.Collections.Generic;
using System.Linq;
using EduHub.Extensions;
using EduHub.Models.AdminControllerModels;
using EduHub.Models.Tools;
using EduHubLibrary.Facades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

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
        ///     Returns history of moderators' actions
        /// </summary>
        [HttpGet]
        [Route("history")]
        [SwaggerResponse(200, Type = typeof(AllPossibleModeratorsActions))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult GetModeratorsHistory()
        {
            var response = _userFacade.GetModeratorsHistory().ToList();
            return Ok(response);
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
            _reportFacade.GetAll().ToList().ForEach(r => reports.Add(new ReportModel(r.ReportId, r.SenderName,
                r.SuspectedName, r.Reason, r.Description)));
            var response = new AllReportsResponse(reports);

            return Ok(response);
        }

        [HttpGet]
        [Route("reports/{reportId}")]
        [SwaggerResponse(200, Type = typeof(ReportModel))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult GetReport([FromRoute] int reportId)
        {
            var report = _reportFacade.Get(reportId);
            var response = new ReportModel(report.ReportId, report.SenderName, report.SuspectedName, report.Reason,
                report.Description);
            return Ok();
        }
    }
}