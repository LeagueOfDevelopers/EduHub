using System;
using Microsoft.AspNetCore.Mvc;
using EduHub.Models;
using EduHubLibrary.Facades;
using Swashbuckle.AspNetCore.SwaggerGen;
using EduHubLibrary.Common;
using EduHub.Security;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/account")]
    public class AccountController : Controller
    {
        /// <summary>
        /// Adds user to db
        /// </summary>
        [HttpPost]
        [Route("registration")]
        [SwaggerResponse(200, typeof(RegistrationResponse))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult Registrate([FromBody]RegistrationRequest request)
        {
            Guid newId = _userFacade.RegUser(request.Name, Credentials.FromRawData(request.Email, request.Password),
                request.IsTeacher, UserType.User);
            RegistrationResponse response = new RegistrationResponse(newId);
            return Ok(response);
        }

        /// <summary>
        /// Returns user's token and other information
        /// </summary>
        [HttpPost]
        [Route("login")]
        [SwaggerResponse(200, typeof(LoginResponse))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult Login([FromBody]LoginRequest loginRequest)
        {
            var creditials = Credentials.FromRawData(loginRequest.Email, loginRequest.Password);
            var client = _userFacade.FindByCredentials(creditials);
   
            if (client != null)
            {
                LoginResponse response = new LoginResponse(client.UserProfile.Name, client.Credentials.Email, 
                    client.UserProfile.AvatarLink, _jwtIssuer.IssueJwt(Claims.Roles.User, client.Id));
                return Ok(response);
            }

            return Unauthorized();
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