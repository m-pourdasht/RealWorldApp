namespace RealWorldApp.Shared.Models
{
    public class User
    {
        public int Id { get; set; }  // Primary Key
        public string Username { get; set; }
        public string PasswordHash { get; set; }  // Store hashed passwords
        public string Email { get; set; }  // Optional: Include other fields as needed
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
