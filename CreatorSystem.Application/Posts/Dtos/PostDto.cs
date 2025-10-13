namespace CreatorSystem.Application.Posts.Dtos
{
    public class PostDto
    {
        public required Guid Id { get; init; }
        public required string Title { get; init; }
        public required string Content { get; init; }
        public required string Platform { get; init; }
        public required DateTime ScheduledAt { get; init; }
        public bool IsPublished { get; init; }
    }
}
