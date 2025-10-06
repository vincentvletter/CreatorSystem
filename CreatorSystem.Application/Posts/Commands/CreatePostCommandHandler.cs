using CreatorSystem.Application.Common.Interfaces;
using CreatorSystem.Domain.Entities;
using MediatR;

namespace CreatorSystem.Application.Posts.Commands;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Guid>
{
    private readonly IAppDbContext _context;

    public CreatePostCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = new Post(request.Title, request.Content);

        _context.Posts.Add(post);
        await _context.SaveChangesAsync(cancellationToken);

        return post.Id;
    }
}
