using CreatorSystem.Application.Common.Interfaces;
using CreatorSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CreatorSystem.Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler(IAppDbContext context) : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly PasswordHasher<User> _hasher = new();

        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var normalizedEmail = request.Email.Trim().ToLowerInvariant();

            var passwordHash = _hasher.HashPassword(null!, request.Password);
            var user = new User(normalizedEmail, passwordHash);
            user.SetRole("User"); // of user.Role = "User" afhankelijk van jouw entity

            context.Users.Add(user);
            await context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
