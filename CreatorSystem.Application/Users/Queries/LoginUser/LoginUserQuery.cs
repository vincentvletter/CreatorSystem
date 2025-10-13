using MediatR;

namespace CreatorSystem.Application.Users.Queries.LoginUser
{
    public record LoginUserQuery(string Email, string Password) : IRequest<LoginUserResponse>;
}
