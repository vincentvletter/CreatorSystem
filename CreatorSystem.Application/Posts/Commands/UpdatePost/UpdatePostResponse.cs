namespace CreatorSystem.Application.Posts.Commands.UpdatePost
{
    public class UpdatePostResponse
    {
        public required Guid PostId { get; init; }
        public required string Title { get; init; }
        public required string Content { get; init; }
        public required string Platform { get; init; }
        public required DateTime ScheduledAt { get; init; }
        public string Message { get; init; } = "Post updated successfully";
        public bool IsPublished { get; init; }
    }
}
