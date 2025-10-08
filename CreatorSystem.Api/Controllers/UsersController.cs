using CreatorSystem.Application.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CreatorSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
            var userId = await mediator.Send(command);
            return Ok(new { Message = "User registered successfully", UserId = userId });
        }
    }
}
