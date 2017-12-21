using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EduHub.Models;
using EduHubLibrary.Facades;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/account")]
    public class AccountController : Controller
    {
        [HttpPost]
        [Route("registration")]
        public IActionResult Registrate([FromBody]RegistrationRequest request)
        {
            _userFacade.RegUser(request.Name, request.Email, request.Password, request.IsTeacher);
            return Ok("Пользователь зарегистрирован");
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]LoginRequest loginRequest)
        {
            return Ok("Пользователь авторизован");
        }

        //TODO delete
        [HttpGet]
        [Route("")]
        public IActionResult All()
        {
            return Ok(_userFacade.GetUsers());
        }
        

        public AccountController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        private readonly IUserFacade _userFacade;
    }
}