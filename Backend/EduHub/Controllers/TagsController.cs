using System.Collections.Generic;
using System.Linq;
using EduHub.Models.Tools;
using EduHubLibrary.Domain;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/tags")]
    public class TagsController : Controller
    {
        private readonly ITagFacade _tagsManager;

        public TagsController(ITagFacade tagsManager)
        {
            _tagsManager = tagsManager;
        }

        /// <summary>
        ///     Find tag among existing tags
        /// </summary>
        [HttpPost]
        [SwaggerResponse(200, Type = typeof(TagModel))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [Route("search")]
        public IActionResult FindTag([FromBody] string tag)
        {
            var foundTags = _tagsManager.FindTag(tag);
            var response = new List<TagModel>();
            foundTags.ToList().ForEach(t => response.Add(new TagModel(t)));
            return Ok(response);
        }
    }
}