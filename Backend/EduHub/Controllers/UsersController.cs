using System.Collections.Generic;
using System.Linq;
using EduHub.Models;
using EduHubLibrary.Facades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly IUserFacade _userFacade;

        public UsersController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        /// <summary>
        ///     Searches user somehow (for now)
        /// </summary>
        [HttpPost]
        [Route("search")]
        [SwaggerResponse(200, typeof(MinUserResponse))]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        public IActionResult SearchUser([FromBody] SearchOfUserRequest user)
        {
            if (_userFacade.DoesUserExist(user.Name))
            {
                var foundUsers = _userFacade.FindByName(user.Name);
                var items = new List<MinItemUserResponse>();
                foundUsers.ToList().ForEach(u => items.Add(new MinItemUserResponse(u.Id, u.UserProfile.Name,
                    u.UserProfile.Email,
                    u.UserProfile.IsTeacher, u.IsActive, u.UserProfile.AvatarLink)));
                var response = new MinUserResponse(items);
                return Ok(response);
            }

            return Ok($"Пользователь с именем {user.Name} не найден");
        }

        /// <summary>
        ///     Reports user somehow (for now)
        /// </summary>
        [Authorize]
        [HttpPost]
        [Route("{userId}/report")]
        [SwaggerResponse(400, Type = typeof(BadRequestObjectResult))]
        [SwaggerResponse(401, Type = typeof(UnauthorizedResult))]
        public IActionResult Report([FromRoute] int userId)
        {
            return Ok($"Жалоба на пользователя добавлена");
        }
    }
}