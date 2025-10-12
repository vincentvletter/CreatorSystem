namespace CreatorSystem.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public string Role { get; private set; } = "User"; // Default role
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        private User() { } // EF Core

        public User(string email, string passwordHash)
        {
            Email = email;
            PasswordHash = passwordHash;
        }

        public void SetRole(string role) => Role = role;
    }
}
