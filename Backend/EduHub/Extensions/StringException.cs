using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Http;

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

        public static int GetUserId(this HttpRequest request)
        {
            var auth = request.Headers["Authorization"].ToString();
            var handler = new JwtSecurityTokenHandler();
            var userId =
                Int32.Parse(handler.ReadJwtToken(auth.Substring(7)).Claims.First(c => c.Type == "UserId").Value);
            return userId;
        }
    }
}