using CreatorSystem.Application.Posts.Dtos;
using MediatR;

namespace CreatorSystem.Application.Posts.Queries.GetAllPosts
{
    public record GetAllPostsQuery() : IRequest<List<PostDto>>;
}

