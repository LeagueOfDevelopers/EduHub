using System;
using EduHub.Models;
using EduHub.Security;
using EduHubLibrary.Common;
using EduHubLibrary.Facades;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly IAuthUserFacade _authUserFacade;
        private readonly IJwtIssuer _jwtIssuer;
        private readonly SecuritySettings _securitySettings;
        private readonly IUserFacade _userFacade;


        public AccountController(IUserFacade userFacade, SecuritySettings securitySettings,
            IJwtIssuer jwtIssuer, IAuthUserFacade authUserFacade)
        {
            _userFacade = userFacade;
            _jwtIssuer = jwtIssuer;
            _securitySettings = securitySettings;
            _authUserFacade = authUserFacade;
        }

        /// <summary>
        ///     Adds user to db
        /// </summary>
        [HttpPost]
        [Route("registration")]
        [SwaggerResponse(200, typeof(RegistrationResponse))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult Registrate([FromBody] RegistrationRequest request)
        {
            var newId = _authUserFacade.RegUser(request.Name, Credentials.FromRawData(request.Email, request.Password),
                request.IsTeacher, UserType.UnConfirmed);
            var response = new RegistrationResponse(newId);
            return Ok(response);
        }

        /// <summary>
        ///     Returns user's token and other information
        /// </summary>
        [HttpPost]
        [Route("login")]
        [SwaggerResponse(200, typeof(LoginResponse))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            var creditials = Credentials.FromRawData(loginRequest.Email, loginRequest.Password);
            var client = _userFacade.FindByCredentials(creditials);

            if (client != null)
            {
                var response = new LoginResponse(client.UserProfile.Name, client.Credentials.Email,
                    client.UserProfile.AvatarLink, _jwtIssuer.IssueJwt(Claims.Roles.User, client.Id));
                return Ok(response);
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("confirm/{key}")]
        [SwaggerResponse(200, typeof(LoginResponse))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult Confirm([FromRoute] Guid key)
        {
            _authUserFacade.ConfirmUser(key);
            return Ok();
        }
    }
}