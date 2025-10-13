using CreatorSystem.Application.Common.Responses;
using CreatorSystem.Application.Posts.Commands.CreatePost;
using CreatorSystem.Application.Posts.Dtos;
using CreatorSystem.Application.Posts.Queries.GetAllPosts;
using CreatorSystem.Application.Users.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CreatorSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<ApiResponse<CreatePostResponse>>> Create([FromBody] CreatePostCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(ApiResponse<CreatePostResponse>.SuccessResponse(result));
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<PostDto>>>> GetAll()
        {
            var result = await mediator.Send(new GetAllPostsQuery());
            return Ok(ApiResponse<List<PostDto>>.SuccessResponse(result));
        }
    }
}
