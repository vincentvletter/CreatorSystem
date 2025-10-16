using CreatorSystem.Application.Posts.Commands.CreatePost;
using CreatorSystem.Application.Posts.Commands.UpdatePost;
using CreatorSystem.Application.Posts.Dtos;
using CreatorSystem.Domain.Entities;
using System.Linq.Expressions;

namespace CreatorSystem.Application.Posts.Mappings
{
    public static class PostMapper
    {
        // responses
        public static CreatePostResponse ToCreateResponse(this Post post) => new CreatePostResponse
        {
            PostId = post.Id,
            Title = post.Title
        };

        public static UpdatePostResponse ToUpdateResponse(this Post post) => new UpdatePostResponse
        {
            PostId = post.Id,
            Title = post.Title,
            Content = post.Content,
            Platform = post.Platform,
            ScheduledAt = post.ScheduledAt,
            IsPublished = post.IsPublished
        }; 

        // Dto's
        public static readonly Expression<Func<Post, PostDto>> Projection = post => new PostDto
        {
            Id = post.Id,
            Title = post.Title,
            Content = post.Content,
            Platform = post.Platform,
            ScheduledAt = post.ScheduledAt,
            IsPublished = post.IsPublished
        };

        public static PostDto ToDto(this Post post) => new PostDto
        {
            Id = post.Id,
            Title = post.Title,
            Content = post.Content,
            Platform = post.Platform,
            ScheduledAt = post.ScheduledAt,
            IsPublished = post.IsPublished
        };
    }
}
