using CreatorSystem.Application.Common.Interfaces;
using CreatorSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CreatorSystem.Application.Users.Commands
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IAppDbContext _context;
        private readonly PasswordHasher<User> _hasher = new();

        public RegisterUserCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = _hasher.HashPassword(null!, request.Password);
            var user = new User(request.Email, passwordHash);

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
