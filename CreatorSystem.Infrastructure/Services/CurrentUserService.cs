using CreatorSystem.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CreatorSystem.Infrastructure.Services
{
    public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
    {
        public Guid UserId
        {
            get
            {
                var id = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                return Guid.TryParse(id, out var guid) ? guid : Guid.Empty;
            }
        }
    }
}
