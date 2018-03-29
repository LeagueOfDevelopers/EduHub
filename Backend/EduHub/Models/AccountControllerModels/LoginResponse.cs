namespace EduHub.Models
{
    public class LoginResponse
    {
        public LoginResponse(string name, string email, string avatarLink, string token,
            bool isTeacher)
        {
            Name = name;
            Email = email;
            AvatarLink = avatarLink;
            Token = token;
            IsTeacher = isTeacher;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string AvatarLink { get; set; }
        public string Token { get; set; }
        public bool IsTeacher { get; set; }
    }
}