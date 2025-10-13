using CreatorSystem.Application.Users.Commands.RegisterUser;
using CreatorSystem.Application.Users.Dtos;
using CreatorSystem.Domain.Entities;

namespace CreatorSystem.Application.Users.Mappings
{
    public static class UserMapper
    {
        public static UserDto ToDto(this User user) => new()
        {
            Id = user.Id,
            Email = user.Email,
            CreatedAt = user.CreatedAt
        };

        public static RegisterUserResponse ToRegisterResponse(this User user) => new()
        {
            UserId = user.Id,
            Email = user.Email
        };
    }
}
