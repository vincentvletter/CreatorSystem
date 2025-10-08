using CreatorSystem.Application.Common.Interfaces;
using CreatorSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CreatorSystem.Application.Users.Commands
{
    public record RegisterUserCommand(string Email, string Password) : IRequest<Guid>;

    public class RegisterUserCommandHandler(IAppDbContext context) : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly PasswordHasher<User> _hasher = new();     

        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = _hasher.HashPassword(null!, request.Password);
            var user = new User(request.Email, passwordHash);

            context.Users.Add(user);
            await context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
