namespace CreatorSystem.Application.Users.Commands.RegisterUser
{
    public class RegisterUserResponse
    {
        public required Guid UserId { get; init; }
        public required string Email { get; init; }
        public string Message { get; init; } = "User registered successfully";
    }
}
