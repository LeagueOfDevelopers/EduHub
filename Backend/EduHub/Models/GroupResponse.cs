using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
namespace EduHub.Models
{
    public class GroupResponse
    {
        public GroupResponse(GroupInfo groupInfo, Course course, User teacher,IEnumerable<Member> members)
        {
            GroupInfo = groupInfo;
            Members = members;
            Course = course;
            Teacher = teacher;
        }

        public IEnumerable<Member> Members { get; set; }
        public GroupInfo GroupInfo { get; set; }
        public Course Course { get; set; }
        public User Teacher { get; set; }

    }
}
