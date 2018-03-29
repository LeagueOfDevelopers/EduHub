namespace EduHubLibrary.Data.UserDtos
{
    public class NotifiesDto
    {
        public NotifiesDto(int id, string notifie)
        {
            Id = id;
            Notifie = notifie;
        }

        public NotifiesDto()
        {
        }

        public int Id { get; set; }
        public string Notifie { get; set; }
    }
}