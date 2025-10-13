using CreatorSystem.Application.Posts.Commands.CreatePost;
using CreatorSystem.Application.Posts.Dtos;
using CreatorSystem.Domain.Entities;

namespace CreatorSystem.Application.Posts.Mappings
{
    public static class PostMapper
    {
        public static CreatePostResponse ToCreateResponse(this Post post) => new CreatePostResponse
        {
            PostId = post.Id,
            Title = post.Title
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
