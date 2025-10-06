using MediatR;

namespace CreatorSystem.Application.Posts.Commands;

public record CreatePostCommand(string Title, string Content) : IRequest<Guid>;
