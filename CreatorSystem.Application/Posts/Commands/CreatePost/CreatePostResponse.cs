namespace CreatorSystem.Application.Posts.Commands.CreatePost
{
    public class CreatePostResponse
    {
        public required Guid PostId { get; init; }
        public required string Title { get; init; }
        public string Message { get; init; } = "Post created successfully";
    }
}
