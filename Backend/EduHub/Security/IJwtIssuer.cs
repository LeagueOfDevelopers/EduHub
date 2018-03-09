using System;

namespace EduHub.Security
{
    public interface IJwtIssuer
    {
        string IssueJwt(string role, int id);
    }
}