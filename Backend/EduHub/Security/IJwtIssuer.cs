using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Security
{
    public interface IJwtIssuer
    {
        string IssueJwt(string role, Guid? id);
    }
}
