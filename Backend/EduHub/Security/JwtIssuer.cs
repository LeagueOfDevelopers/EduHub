﻿using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EduHub.Security
{
    public class JwtIssuer : IJwtIssuer
    {
        public JwtIssuer(SecuritySettings securitySettings)
        {
            _securitySettings = securitySettings;
        }

        public string IssueJwt(string role, Guid id)
        {
            var claims = new[]
             {
                new Claim(Claims.Roles.RoleClaim, role),
                new Claim(Claims.IdClaim, id.ToString())
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securitySettings.EncryptionKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: _securitySettings.Issue, claims: claims, expires: DateTime.Now.Add(_securitySettings.ExpirationPeriod),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private readonly SecuritySettings _securitySettings;
    }
}
