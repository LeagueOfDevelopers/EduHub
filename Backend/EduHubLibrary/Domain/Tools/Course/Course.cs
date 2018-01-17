
using EduHubLibrary.Domain.Tools;
using EduHubLibrary.Domain.Tools.Course;
using System.Collections.Generic;

namespace EduHubLibrary.Domain
{
    public class Course
    {
        public string CourseDescription { get; set; }
        public CourseStatus CourseStatus { get; set; }
        
        private List<Lesson> Lessons { get; set; }

        public Course(string courseDescription)
        {
            CourseDescription = courseDescription;
            CourseStatus = CourseStatus.InProgress;
        }
    }
}
