using CreatorSystem.Application.Common.Interfaces;
using MediatR;

namespace CreatorSystem.Application.Posts.Commands.DeletePost
{
    public class DeletePostCommandHandler(IAppDbContext context, ICurrentUserService currentUser) : IRequestHandler<DeletePostCommand, DeletePostResponse>
    {
        public async Task<DeletePostResponse> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var userId = currentUser.UserId;
            if (userId == Guid.Empty)
                throw new UnauthorizedAccessException("User not authenticated.");

            var post = context.Posts.FirstOrDefault(p => p.Id == request.Id && p.UserId == userId)
                ?? throw new KeyNotFoundException($"Post with id {request.Id} not found.");
            
            context.Posts.Remove(post);
            await context.SaveChangesAsync(cancellationToken);

            return new DeletePostResponse { Message = "Post deleted successfully" };
        }
    }
}
