using CreatorSystem.Application.Common.Interfaces;
using CreatorSystem.Domain.Entities;
using MediatR;

namespace CreatorSystem.Application.Posts.Commands.CreatePost
{
    public class CreatePostCommandHandler(IAppDbContext context) : IRequestHandler<CreatePostCommand, Guid>
    {
        public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var post = new Post(request.Title, request.Content);

            context.Posts.Add(post);
            await context.SaveChangesAsync(cancellationToken);

            return post.Id;
        }
    }
}
