using CreatorSystem.Application.Common.Interfaces;
using CreatorSystem.Domain.Entities;
using MediatR;

namespace CreatorSystem.Application.Posts.Commands.CreatePost;

public class CreatePostCommandHandler(IAppDbContext context, ICurrentUserService currentUser) : IRequestHandler<CreatePostCommand, Guid>
{
    public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var userId = currentUser.UserId;
        if (userId == Guid.Empty)
            throw new UnauthorizedAccessException("User not authenticated.");

        var post = new Post(userId, request.Title, request.Content, request.Platform, request.ScheduledAt);

        context.Posts.Add(post);
        await context.SaveChangesAsync(cancellationToken);

        return post.Id;
    }
}