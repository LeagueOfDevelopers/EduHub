using System.Collections.Generic;

namespace EduHub.Models.UsersControllerModels
{
    public class MinUserFilterResponse
    {
        public MinUserFilterResponse(List<UserFilterResponse> users)
        {
            Users = users;
        }

        public List<UserFilterResponse> Users { get; }
    }
}