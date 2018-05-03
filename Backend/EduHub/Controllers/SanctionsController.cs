using System;
using EduHub.Extensions;
using EduHub.Models.SanctionsController;
using EduHub.Models.Tools;
using EduHubLibrary.Facades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EduHub.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/sanctions")]
    public class SanctionController : Controller
    {
        private readonly ISanctionFacade _sanctionFacade;

        public SanctionController(ISanctionFacade sanctionFacade)
        {
            _sanctionFacade = sanctionFacade;
        }

        /// <summary>
        ///     Applies sanctions for user
        /// </summary>
        [Authorize(Policy = "AdminAndModeratorsOnly")]
        [HttpPost]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult ApplySanction([FromBody] ApplySanctionRequest request)
        {
            var moderatorId = Request.GetUserId();
            int sanctionId;

            if (request.ExpirationDate != DateTimeOffset.MinValue)
                sanctionId = _sanctionFacade.AddSanction(request.BrokenRule, request.UserId, moderatorId,
                    request.SanctionType, request.ExpirationDate);
            else
                sanctionId = _sanctionFacade.AddSanction(request.BrokenRule, request.UserId, moderatorId,
                    request.SanctionType);

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
            var response = new SanctionsResponse(sanctions);
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
            var response = new SanctionsResponse(sanctions);
            return Ok(response);
        }

        /// <summary>
        ///     Get sanction
        /// </summary>
        [Authorize(Policy = "AdminAndModeratorsOnly")]
        [HttpGet]
        [Route("{sanctionId}")]
        [SwaggerResponse(200, Type = typeof(SanctionModel))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult GetSanction([FromRoute] int sanctionId)
        {
            var sanction = _sanctionFacade.Get(sanctionId);
            var response = new SanctionModel(sanction.BrokenRule, sanction.UserId, sanction.UserName,
                sanction.ModeratorId, sanction.IsTemporary, sanction.ExpirationDate, sanction.Type, sanction.IsActive);
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
    }
}