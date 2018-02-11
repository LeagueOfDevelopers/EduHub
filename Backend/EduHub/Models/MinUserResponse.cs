using System.Collections.Generic;

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