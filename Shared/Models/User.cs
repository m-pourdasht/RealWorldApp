namespace RealWorldApp.Shared.Models
{
    public class User
    {
        public int Id { get; set; }  // Primary Key
        public string Username { get; set; }

        // Add the PasswordHash property here
        public string PasswordHash { get; set; }

    }
}
