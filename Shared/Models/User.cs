namespace RealWorldApp.Shared.Models
{
    public class User
    {
        public int Id { get; set; }  // Primary Key
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }

    }
}
