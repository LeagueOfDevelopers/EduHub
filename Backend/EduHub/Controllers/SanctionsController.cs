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
        [Route("{idOfUser}")]
        public IActionResult ApplySanction([FromRoute] int idOfUser)
        {
            return Ok($"К пользователю {idOfUser} применены санкции");
        }
        /// <summary>
        /// Anulls sanctions for user
        /// </summary>
        [HttpDelete]
        [Route("{idOfUser}")]
        public IActionResult AnullSanction([FromRoute] int idOfUser)
        {
            return Ok($"Санкции с пользователя {idOfUser} сняты");
        }
    }
}