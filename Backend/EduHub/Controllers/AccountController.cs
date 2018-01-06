using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EduHub.Models;
using EduHubLibrary.Facades;
using Swashbuckle.AspNetCore.SwaggerGen;
using EduHubLibrary.Domain;
using EduHubLibrary.Common;
using EduHub.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/account")]
    public class AccountController : Controller
    {
        [HttpPost]
        [Route("registration")]
        [SwaggerResponse(200, typeof(RegistrationResponse))]
        public IActionResult Registrate([FromBody]RegistrationRequest request)
        {
            Guid newId = _userFacade.RegUser(request.Name, Credentials.FromRawData(request.Email, request.Password), request.IsTeacher, TypeOfUser.User, request.AvatarLink);
            RegistrationResponse response = new RegistrationResponse(newId);
            return Ok(response);
        }

        [HttpPost]
        [Route("login")]
        [SwaggerResponse(200, typeof(LoginRequest))]
        public IActionResult Login([FromBody]LoginRequest loginRequest)
        {
            var creditials = Credentials.FromRawData(loginRequest.Email, loginRequest.Password);
            var client = _userFacade.FindByCredentials(creditials);
   
            if (client != null)
            {
                LoginResponse response = new LoginResponse(client.Name, client.AvatarLink, _jwtIssuer.IssueJwt(Claims.Roles.User, client.Id));
                return Ok(response);
            }

            return Unauthorized();
        }

        //TODO delete
        [HttpGet]
        public IActionResult All()
        {
            return Ok(_userFacade.GetUsers());
        }
        
        public AccountController(IUserFacade userFacade, SecuritySettings securitySettings, IJwtIssuer jwtIssuer)
        {
            _userFacade = userFacade;
            _jwtIssuer = jwtIssuer;
            _securitySettings = securitySettings;
        }

        private readonly IJwtIssuer _jwtIssuer;
        private readonly SecuritySettings _securitySettings;
        private readonly IUserFacade _userFacade;
    }
}