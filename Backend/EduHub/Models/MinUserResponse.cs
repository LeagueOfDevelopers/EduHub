using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models
{
    public class MinUserResponse
    {
        public MinUserResponse(IEnumerable<MinItemUserResponse> users)
        {
            Users = users;
        }

        public IEnumerable<MinItemUserResponse> Users { get; set; }
    }
}
