using EduHub.Extensions;
using EduHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/file")]
    public class FileController : Controller
    {

        [HttpPost]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult AddFile(IFormFile file)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();

            var fileName = userId + "_" + Guid.NewGuid() + "_" + file.FileName;
            var uploadPath =  Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);
            var filePath = Path.Combine(uploadPath, fileName);

            file.CopyTo(new FileStream(filePath, FileMode.Create));

            var request = Request;
            var host = request.Host.ToUriComponent();
            var pathBase = request.PathBase.ToUriComponent();
            var path =  $"{request.Scheme}://{host}{pathBase}/wwwroot/uploads/{fileName}";

            return Ok(path);
        }
        public FileController(IHostingEnvironment environment)
        {
            _hostingEnvironment = environment;
        }
        private readonly IHostingEnvironment _hostingEnvironment;
    }
}
