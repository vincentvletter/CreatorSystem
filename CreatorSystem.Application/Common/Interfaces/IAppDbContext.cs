using CreatorSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CreatorSystem.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<Post> Posts { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
