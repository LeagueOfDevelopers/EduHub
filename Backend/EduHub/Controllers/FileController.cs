using EduHub.Extensions;
using EduHub.Models;
using EduHubLibrary.Domain;
using EduHubLibrary.Facades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;

namespace EduHub.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/file")]
    public class FileController : Controller
    {
        public FileController(IHostingEnvironment environment, IFileFacade fileFacade)
        {
            _hostingEnvironment = environment;
            _fileFacade = fileFacade;
        }

        [HttpPost]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        [SwaggerResponse(200, Type = typeof(AddFileResponse))]
        [RequestSizeLimit(20_000_000)]
        public IActionResult AddFile(IFormFile file)
        {
            string a = Request.Headers["Authorization"];
            var userId = a.GetUserId();

            if (!file.IsSupportedFile())
            {
                throw new NotSupportedException();
            }

            var fileName = userId + "_" + Guid.NewGuid() + "_" + file.FileName;
            var uploadPath =  Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadPath))
            { 
                Directory.CreateDirectory(uploadPath);
            }
            var filePath = Path.Combine(uploadPath, fileName);
            using (FileStream filestream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(filestream);
            }

            _fileFacade.AddFile(fileName, file.ContentType);

            AddFileResponse response = new AddFileResponse(fileName);

            return Ok(response);
        }

        [HttpGet]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(200, Type = typeof(File))]
        [Route("{filename}")]
        public IActionResult GetFile([FromRoute]string filename)
        {
            UserFile file = _fileFacade.GetFile(filename);
            var downloadPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            var filePath = Path.Combine(downloadPath, file.Filename);
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, file.ContentType);
        }

        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IFileFacade _fileFacade;
    }
}
