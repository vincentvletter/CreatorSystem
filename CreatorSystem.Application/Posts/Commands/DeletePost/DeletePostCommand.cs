using MediatR;

namespace CreatorSystem.Application.Posts.Commands.DeletePost
{
    public record DeletePostCommand(
        Guid Id) : IRequest<DeletePostResponse>;   
}
