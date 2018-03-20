using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Data.GroupDtos
{
    public class TeacherDto
    {
        
        public int Id { get; set; }
        public string Name { get; set; }

        public TeacherDto(int id)
        {
            Id = id;
        }

        public TeacherDto()
        {
        }
    }
}
