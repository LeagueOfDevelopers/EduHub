namespace EduHub.Models
{
    public class CreateGroupResponse
    {
        public CreateGroupResponse(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}