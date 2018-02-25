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
        /// <summary>
        ///     Applies sanctions for user
        /// </summary>
        [Authorize]
        [HttpPost]
        [Route("{userId}")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult ApplySanction([FromRoute] int userId)
        {
            return Ok();
        }

        /// <summary>
        ///     Anulls sanctions for user
        /// </summary>
        [Authorize]
        [HttpDelete]
        [Route("{userId}")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult AnullSanction([FromRoute] int userId)
        {
            return Ok();
        }
    }
}