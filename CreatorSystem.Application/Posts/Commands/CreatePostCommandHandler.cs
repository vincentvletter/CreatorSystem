using CreatorSystem.Domain.Entities;
using CreatorSystem.Infrastructure.Data;
using MediatR;
using System;

namespace CreatorSystem.Application.Posts.Commands;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Guid>
{
    private readonly AppDbContext _context;

    public CreatePostCommandHandler(AppDbContext context)
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
