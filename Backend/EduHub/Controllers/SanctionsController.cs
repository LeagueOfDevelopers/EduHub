using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/sanctions")]
    public class SanctionController : Controller
    {
        /// <summary>
        /// Applies sanctions for user
        /// </summary>
        [HttpPost]
        [Route("{userId}")]
        public IActionResult ApplySanction([FromRoute] int userId)
        {
            return Ok($"К пользователю {userId} применены санкции");
        }
        /// <summary>
        /// Anulls sanctions for user
        /// </summary>
        [HttpDelete]
        [Route("{userId}")]
        public IActionResult AnullSanction([FromRoute] int userId)
        {
            return Ok($"Санкции с пользователя {userId} сняты");
        }
    }
}