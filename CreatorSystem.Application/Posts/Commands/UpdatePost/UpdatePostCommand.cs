using MediatR;

namespace CreatorSystem.Application.Posts.Commands.UpdatePost
{
    public record UpdatePostCommand(
        Guid Id,
        string Title,
        string Content,
        string Platform,
        DateTime ScheduledAt,
        bool IsPublished) : IRequest<UpdatePostResponse>;   
}
