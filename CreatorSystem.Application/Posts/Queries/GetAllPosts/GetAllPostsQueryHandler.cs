using CreatorSystem.Application.Common.Interfaces;
using CreatorSystem.Application.Posts.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CreatorSystem.Application.Posts.Queries.GetAllPosts
{
    public class GetAllPostsQueryHandler(IAppDbContext context, ICurrentUserService currentUser) : IRequestHandler<GetAllPostsQuery, List<PostDto>>
    {
        public async Task<List<PostDto>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            var userId = currentUser.UserId;
            if (userId == Guid.Empty)
                throw new UnauthorizedAccessException("User not authenticated.");

            return await context.Posts
                .Where(p => p.UserId == userId)
                .Select(p => new PostDto { Id = p.Id, Title = p.Title, Content = p.Content, Platform = p.Platform, ScheduledAt = p.ScheduledAt, IsPublished = p.IsPublished })
                .OrderByDescending(p => p.ScheduledAt)
                .ToListAsync(cancellationToken);
        }
    }
}
