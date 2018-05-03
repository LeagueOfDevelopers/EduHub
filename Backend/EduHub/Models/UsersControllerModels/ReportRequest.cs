namespace EduHub.Models.UsersControllerModels
{
    public class ReportRequest
    {
        public ReportRequest(string reason, string description)
        {
            Reason = reason;
            Description = description;
        }

        public string Reason { get; set; }
        public string Description { get; set; }
    }
}