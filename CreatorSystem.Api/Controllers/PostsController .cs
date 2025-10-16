using CreatorSystem.Application.Common.Responses;
using CreatorSystem.Application.Posts.Commands.CreatePost;
using CreatorSystem.Application.Posts.Commands.DeletePost;
using CreatorSystem.Application.Posts.Commands.UpdatePost;
using CreatorSystem.Application.Posts.Dtos;
using CreatorSystem.Application.Posts.Queries.GetAllPosts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
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

        [HttpPost("{id:guid}")]
        public async Task<ActionResult<ApiResponse<UpdatePostResponse>>> Update(Guid id, [FromBody] UpdatePostCommand command)
        {
            if (id != command.Id)
                return BadRequest(ApiResponse<UpdatePostResponse>.FailResponse("Route ID and body ID do not match."));

            var result = await mediator.Send(command);
            return Ok(ApiResponse<UpdatePostResponse>.SuccessResponse(result));
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ApiResponse<DeletePostResponse>>> Delete(Guid id, [FromBody] DeletePostCommand command)
        {
            if (id != command.Id)
                return BadRequest(ApiResponse<UpdatePostResponse>.FailResponse("Route ID and body ID do not match."));

            var result = await mediator.Send(command);
            return Ok(ApiResponse<DeletePostResponse>.SuccessResponse(result));
        }
    }
}
