
using EduHubLibrary.Domain.Tools;

namespace EduHubLibrary.Domain
{
    public class Course
    {
        public string CourseDescription { get; set; }
        public CourseStatus CourseStatus { get; set; }

        public Course(string courseDescription)
        {
            CourseDescription = courseDescription;
            CourseStatus = CourseStatus.InProgress;
        }
    }
}
