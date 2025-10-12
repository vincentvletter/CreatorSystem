namespace CreatorSystem.Domain.Entities
{
    public class Post
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid UserId { get; private set; }
        public string Title { get; private set; } = default!;
        public string Content { get; private set; } = default!;
        public string Platform { get; private set; } = default!;
        public DateTime ScheduledAt { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public bool IsPublished { get; private set; } = false;

        private Post() { } // EF Core

        public Post(Guid userId, string title, string content, string platform, DateTime scheduledAt)
        {
            UserId = userId;
            Title = title;
            Content = content;
            Platform = platform;
            ScheduledAt = scheduledAt;
        }

        public void Update(string title, string content, DateTime scheduledAt)
        {
            Title = title;
            Content = content;
            ScheduledAt = scheduledAt;
        }

        public void MarkAsPublished() => IsPublished = true;
    }
}
