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
        [HttpPost]
        [Route("{idOfUser}")]
        public IActionResult ApplySanction()
        {
            return Ok("К пользователю применены санкции");
        }

        [HttpDelete]
        [Route("{idOfUser}")]
        public IActionResult LiftSanction()
        {
            return Ok("Санкции пользователя отменены");
        }
    }
}