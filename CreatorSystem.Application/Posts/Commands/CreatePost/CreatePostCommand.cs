using MediatR;

namespace CreatorSystem.Application.Posts.Commands.CreatePost
{
    public record CreatePostCommand(
    string Title,
    string Content,
    string Platform,
    DateTime ScheduledAt) : IRequest<Guid>;
}