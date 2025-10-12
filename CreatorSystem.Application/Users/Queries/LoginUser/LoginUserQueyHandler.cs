using CreatorSystem.Application.Common.Interfaces;
using CreatorSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CreatorSystem.Application.Users.Queries.LoginUser
{
    public class LoginUserQueryHandler(IAppDbContext context, ITokenService tokenService) : IRequestHandler<LoginUserQuery, string>
    {
        private readonly PasswordHasher<User> _hasher = new();

        public async Task<string> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var email = request.Email.Trim().ToLowerInvariant();
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
            if (user == null)
            {
                throw new Exception("Invalid email or password.");
            }

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new Exception("Invalid email or password.");
            }

            return tokenService.CreateToken(user);
        }
    }
}
