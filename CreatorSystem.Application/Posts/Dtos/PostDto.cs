namespace CreatorSystem.Application.Posts.Dtos
{
    public class PostDto
    {
        public     Guid Id { get; init; }
        public string Title { get; init; }
        public string Content { get; init; }
        public string Platform { get; init; }
        public DateTime ScheduledAt { get; init; }
        public bool IsPublished { get; init; }
    }
    //public record PostDto(
    //Guid Id,
    //string Title,
    //string Content,
    //string Platform,
    //DateTime ScheduledAt,
    //bool IsPublished);
}
