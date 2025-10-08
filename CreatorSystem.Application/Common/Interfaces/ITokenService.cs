using CreatorSystem.Domain.Entities;

namespace CreatorSystem.Application.Common.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
