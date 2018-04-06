using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Facades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EduHub.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/administrate")]
    public class AdminController : Controller
    {
        private readonly IReportFacade _reportFacade;

        public AdminController(IReportFacade reportFacade)
        {
            _reportFacade = reportFacade;
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
        ///     Makes user regular user (not admin)
        /// </summary>
        [HttpDelete]
        [Route("{userId}")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult DeleteAdmin([FromRoute] int userId)
        {
            return Ok();
        }

        [HttpGet]
        [Route("reports")]
        [SwaggerResponse(200, Type = typeof(Event))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult GetAllReports()
        {
            var response = _reportFacade.GetAll();
            return Ok(response);
        }
    }
}