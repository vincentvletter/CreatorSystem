using CreatorSystem.Application.Common.Interfaces;
using CreatorSystem.Application.Posts.Mappings;
using MediatR;

namespace CreatorSystem.Application.Posts.Commands.UpdatePost
{
    public class UpdatePostCommandHandler(IAppDbContext context, ICurrentUserService currentUser) : IRequestHandler<UpdatePostCommand, UpdatePostResponse>
    {
        public async Task<UpdatePostResponse> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var userId = currentUser.UserId;
            if (userId == Guid.Empty)
                throw new UnauthorizedAccessException("User not authenticated.");

            var post = context.Posts.FirstOrDefault(p => p.Id == request.Id && p.UserId == userId)
                ?? throw new KeyNotFoundException($"Post with id {request.Id} not found.");

            post.Update(request.Title, request.Content, request.ScheduledAt);
            await context.SaveChangesAsync(cancellationToken);

            return post.ToUpdateResponse();
        }
    }
}
