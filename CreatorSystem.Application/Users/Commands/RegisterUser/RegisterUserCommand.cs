using MediatR;

namespace CreatorSystem.Application.Users.Commands.RegisterUser
{
    public record RegisterUserCommand(string Email, string Password, string ConfirmPassword) : IRequest<Guid>;
}
