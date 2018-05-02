using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Data.GroupDtos
{
    public class KickedId
    {
        public KickedId(int id, int userId)
        {
            Id = id;
            UserId = userId;
        }

        internal KickedId()
        {
        }

        public int Id { get; set; }
        public int UserId { get; set; }
    }
}
