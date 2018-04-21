namespace EduHubLibrary.SocketTool
{
    public class SocketMessage
    {
        public string Text { get; }
        public int GroupId { get; }

        public SocketMessage(string text, int groupId)
        {
            Text = text;
            GroupId = groupId;
        }
    }
}
