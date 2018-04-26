using EduHub.Extensions;
using EduHub.Models.SanctionsController;
using EduHub.Models.Tools;
using EduHubLibrary.Domain;
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
    [Route("api/sanctions")]
    public class SanctionController : Controller
    {
        public SanctionController(ISanctionFacade sanctionFacade)
        {
            _sanctionFacade = sanctionFacade;
        }

        /// <summary>
        ///     Applies sanctions for user
        /// </summary>
        [Authorize(Policy = "AdminAndModeratorsOnly")]
        [HttpPost]
        [Route("{userId}")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult ApplySanction([FromBody] SanctionModel request)
        {
            var moderatorId = Request.GetUserId();
            int sanctionId;

            if (request.ExpirationDate != null)
            {
                sanctionId = _sanctionFacade.AddSanction(request.BrokenRule, request.UserId, moderatorId,
                    request.SanctionType, request.ExpirationDate);
            }
            else
            {
                sanctionId = _sanctionFacade.AddSanction(request.BrokenRule, request.UserId, moderatorId,
                    request.SanctionType);
            }

            return Ok(sanctionId);
        }

        /// <summary>
        ///     Get sanctions' history
        /// </summary>
        [Authorize(Policy = "AdminAndModeratorsOnly")]
        [HttpGet]
        [SwaggerResponse(200, Type = typeof(SanctionsResponse))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult GetAll()
        {
            var sanctions = _sanctionFacade.GetAll();
            SanctionsResponse response = new SanctionsResponse(sanctions);
            return Ok(response);
        }

        /// <summary>
        ///     Get all moderators' sanctions
        /// </summary>
        [Authorize(Policy = "AdminAndModeratorsOnly")]
        [HttpGet]
        [Route("active")]
        [SwaggerResponse(200, Type = typeof(SanctionsResponse))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult GetAllActive()
        {
            var sanctions = _sanctionFacade.GetAllActive();
            SanctionsResponse response = new SanctionsResponse(sanctions);
            return Ok(response);
        }

        /// <summary>
        ///     Anulls sanction
        /// </summary>
        [Authorize(Policy = "AdminAndModeratorsOnly")]
        [HttpDelete]
        [Route("{sanctionId}")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult AnullSanction([FromRoute] int sanctionId)
        {
            _sanctionFacade.CancelSanction(sanctionId);
            return Ok();
        }

        private readonly ISanctionFacade _sanctionFacade;
    }
}