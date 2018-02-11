namespace EduHub.Models
{
    public class EditProfileRequest
    {
        /// <summary>New user's name</summary>
        public string Name { get; set; }

        /// <summary>New user's age</summary>
        public int Age { get; set; }
    }
}