using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EduHub.Models;

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
            return Ok("Пользователь зарегистрирован");
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]LoginRequest loginRequest)
        {
            return Ok("Пользователь авторизован");
        }
    }
}