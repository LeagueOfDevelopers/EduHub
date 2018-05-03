using Microsoft.AspNetCore.Mvc;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/sockets")]
    public class SocketController : Controller
    {
        [HttpPost]
        [Route("creation")]
        public IActionResult OpenConnection()
        {
            return Ok();
        }
    }
}