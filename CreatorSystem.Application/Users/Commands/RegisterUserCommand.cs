using MediatR;

namespace CreatorSystem.Application.Users.Commands
{
    public record RegisterUserCommand(string Email, string Password) : IRequest<Guid>;
}
