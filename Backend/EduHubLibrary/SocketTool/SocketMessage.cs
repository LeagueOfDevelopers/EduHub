namespace EduHubLibrary.SocketTool
{
    public class SocketMessage
    {
        public SocketMessage(string text, int groupId)
        {
            Text = text;
            GroupId = groupId;
        }

        public string Text { get; }
        public int GroupId { get; }
    }
}