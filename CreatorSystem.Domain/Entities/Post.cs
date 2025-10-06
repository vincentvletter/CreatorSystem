namespace CreatorSystem.Domain.Entities
{
    public class Post
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Title { get; private set; }
        public string Content { get; private set; }
        public bool IsPublished { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? PublishedAt { get; private set; }

        private Post() { } // required for EF

        public Post(string title, string content)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty");

            Title = title;
            Content = content;
        }

        public void UpdateContent(string title, string content)
        {
            Title = title;
            Content = content;
        }

        public void Publish()
        {
            if (IsPublished)
                throw new InvalidOperationException("Post is already published.");

            IsPublished = true;
            PublishedAt = DateTime.UtcNow;
        }

        public void Archive()
        {
            IsPublished = false;
        }
    }
}
