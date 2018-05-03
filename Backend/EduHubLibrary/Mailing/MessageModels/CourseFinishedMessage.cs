namespace EduHubLibrary.Mailing.MessageModels
{
    public class CourseFinishedMessage
    {
        public CourseFinishedMessage(string groupTitle, string teacherName, string receiverName)
        {
            GroupTitle = groupTitle;
            TeacherName = teacherName;
            ReceiverName = receiverName;
        }

        public string GroupTitle { get; }
        public string TeacherName { get; }
        public string ReceiverName { get; }
    }
}