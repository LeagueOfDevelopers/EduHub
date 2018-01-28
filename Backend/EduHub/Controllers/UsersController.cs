using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EduHub.Models;
using EduHubLibrary.Facades;
using EduHubLibrary.Domain;

namespace EduHub.Controllers
{
    [Produces("application/json")]
    [Route("api/users")]
    public class UsersController : Controller
    {
        /// <summary>
        /// Searches user somehow (for now)
        /// </summary>
        [HttpPost]
        [Route("search")]
        public IActionResult SearchUser([FromBody]SearchOfUserRequest user)
        {
            if (_userFacade.DoesUserExist(user.Name))
            {
                IEnumerable<User> foundUsers = _userFacade.FindByName(user.Name);
                List<MinItemUserResponse> items = new List<MinItemUserResponse>();
                foundUsers.ToList().ForEach(u => items.Add(new MinItemUserResponse(u.Id, u.Name, u.Credentials.Email,
                    u.IsTeacher, u.TeacherProfile, u.IsActive)));
                MinUserResponse response = new MinUserResponse(items);
                return Ok(response);
            }
            else
            {
                return Ok($"Пользователь с именем {user.Name} не найден");
            }
        }

        /// <summary>
        /// Reports user somehow (for now)
        /// </summary>
        [HttpPost]
        [Route("{idOfUser}/report")]
        public IActionResult Report([FromRoute]int userId)
        {
            return Ok($"Жалоба на пользователя добавлена");
        }

        private readonly IUserFacade _userFacade;

        public UsersController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }
    }
}