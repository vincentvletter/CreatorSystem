using MediatR;

namespace CreatorSystem.Application.Posts.Commands;

// CQRS Command → dit object bevat de data van een "Create Post" actie
public record CreatePostCommand(string Title, string Content) : IRequest<Guid>;
