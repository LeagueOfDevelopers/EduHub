namespace EduHub.Models
{
    public class AddFileResponse
    {
        public AddFileResponse(string filename)
        {
            Filename = filename;
        }

        public string Filename { get; set; }
    }
}