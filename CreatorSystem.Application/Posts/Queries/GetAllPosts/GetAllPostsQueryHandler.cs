using CreatorSystem.Application.Common.Interfaces;
using CreatorSystem.Application.Posts.Dtos;
using CreatorSystem.Application.Posts.Mappings;
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

            var posts = await context.Posts
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.ScheduledAt)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
           
            return posts.Select(p => p.ToDto()).ToList();
        }
    }
}
