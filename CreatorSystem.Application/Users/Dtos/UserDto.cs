namespace CreatorSystem.Application.Users.Dtos
{
    public class UserDto
    {
        public required Guid Id { get; init; }
        public required string Email { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}
