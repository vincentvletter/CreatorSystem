using CreatorSystem.Application.Common.Responses;
using CreatorSystem.Application.Users.Commands.RegisterUser;
using CreatorSystem.Application.Users.Queries.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CreatorSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<RegisterUserResponse>>> Register([FromBody] RegisterUserCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(ApiResponse<RegisterUserResponse>.SuccessResponse(result));
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<LoginUserResponse>>> Login([FromBody] LoginUserQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(ApiResponse<LoginUserResponse>.SuccessResponse(result));
        }
    }
}
