using System;
using EduHub.Extensions;
using EduHub.Models;
using EduHub.Security;
using EduHubLibrary.Common;
using EduHubLibrary.Facades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly IAccountFacade _userAccountFacade;
        private readonly IJwtIssuer _jwtIssuer;
        private readonly SecuritySettings _securitySettings;
        private readonly IUserFacade _userFacade;


        public AccountController(IUserFacade userFacade, SecuritySettings securitySettings,
            IJwtIssuer jwtIssuer, IAccountFacade authUserFacade)
        {
            _userFacade = userFacade;
            _jwtIssuer = jwtIssuer;
            _securitySettings = securitySettings;
            _userAccountFacade = authUserFacade;
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
            int newId;
            
            if (!request.InviteCode.Equals(Guid.Empty))
                newId = _userAccountFacade.RegUser(request.Name, Credentials.FromRawData(request.Email, request.Password),
                request.IsTeacher, request.InviteCode);
            else newId = _userAccountFacade.RegUser(request.Name, Credentials.FromRawData(request.Email, request.Password),
                request.IsTeacher);
            
            var response = new RegistrationResponse(newId);
            return Ok(response);
        }

        /// <summary>
        ///     Returns user's token and another information
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
                string roleClaim;
                if (client.Type.Equals(UserType.Admin)) roleClaim = Claims.Roles.Admin;
                else if (client.Type.Equals(UserType.Moderator)) roleClaim = Claims.Roles.Moderator;
                else if (client.Type.Equals(UserType.User)) roleClaim = Claims.Roles.User;
                else roleClaim = Claims.Roles.UnConfirmed;

                var response = new LoginResponse(client.UserProfile.Name, client.Credentials.Email,
                    client.UserProfile.AvatarLink, _jwtIssuer.IssueJwt(roleClaim, client.Id),
                    client.UserProfile.IsTeacher);
                return Ok(response);
            }

            return Unauthorized();
        }

        /// <summary>
        ///     Refresh user's token and returns a new one with another information
        /// </summary>
        [Authorize]
        [HttpGet]
        [Route("refresh")]
        [SwaggerResponse(200, Type = typeof(LoginResponse))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult RefreshToken()
        {
            var userId = Request.GetUserId();
            var user = _userFacade.GetUser(userId);

            if (user != null)
            {
                var response = new LoginResponse(user.UserProfile.Name, user.Credentials.Email,
                    user.UserProfile.AvatarLink, _jwtIssuer.IssueJwt(Claims.Roles.User, userId),
                    user.UserProfile.IsTeacher);
                return Ok(response);
            }

            return Unauthorized();
        }

        /// <summary>
        ///     Confirm user with key
        /// </summary>
        [HttpPost]
        [Route("confirm/{key}")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult Confirm([FromRoute] Guid key)
        {
            _userAccountFacade.ConfirmUser(key);
            return Ok();
        }

        /// <summary>
        ///     Send message to email with code to set password
        /// </summary>
        [HttpPost]
        [Route("password/restore")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult RestorePassword([FromBody] string email)
        {
            _userAccountFacade.SendQueryToChangePassword(email);
            return Ok();
        }

        /// <summary>
        ///     Set new password with token
        /// </summary>
        [Authorize]
        [HttpPut]
        [Route("password")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult ChangePassword([FromBody] string newPassword)
        {
            var userId = Request.GetUserId();
            _userAccountFacade.ChangePassword(userId, newPassword);
            return Ok();
        }

        /// <summary>
        ///     Set new password with key
        /// </summary>
        [HttpPut]
        [Route("password/{key}")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult ChangePassword([FromRoute] Guid key, [FromBody] string newPassword)
        {
            _userAccountFacade.ChangePassword(newPassword, key);
            return Ok();
        }


        //TODO: for admin only
        /// <summary>
        ///     Send token for registration to new moderator
        /// </summary>
        [HttpPost]
        [Route("moderator")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult SendTokenToModerator([FromBody] string email)
        {
            _userAccountFacade.SendTokenToModerator(email);
            return Ok();
        }
    }
}