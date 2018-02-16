using EduHubLibrary.Domain;
using Microsoft.AspNetCore.Mvc;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/tags")]
    public class TagsController : Controller
    {
        private readonly TagsManager _tagsManager;

        public TagsController(TagsManager tagsManager)
        {
            _tagsManager = tagsManager;
        }

        [HttpPost]
        [Route("search")]
        public IActionResult FindTag([FromBody] string tag)
        {
            return Ok(_tagsManager.FindTag(tag));
        }

        [HttpPost]
        public IActionResult AddTag([FromBody] string tag)
        {
            _tagsManager.AddTag(tag);
            return Ok();
        }
    }
}