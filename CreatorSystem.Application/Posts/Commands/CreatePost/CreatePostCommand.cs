using MediatR;

namespace CreatorSystem.Application.Posts.Commands.CreatePost
{
    public record CreatePostCommand(string Title, string Content) : IRequest<Guid>;
}