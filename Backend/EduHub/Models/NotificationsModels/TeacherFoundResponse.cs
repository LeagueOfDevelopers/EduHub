namespace EduHub.Models.NotificationsModels
{
    public class TeacherFoundResponse
    {
        public TeacherFoundResponse(string teacherName, string groupTitle)
        {
            TeacherName = teacherName;
            GroupTitle = groupTitle;
        }

        public string TeacherName { get; }
        public string GroupTitle { get; }
    }
}