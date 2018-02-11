using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace EduHub.Extensions
{
    public static class StringException
    {
        public static Guid GetUserId(this string auth)
        {
            var handler = new JwtSecurityTokenHandler();
            var userId =
                Guid.Parse(handler.ReadJwtToken(auth.Substring(7)).Claims.First(c => c.Type == "UserId").Value);
            return userId;
        }
    }
}