using CreatorSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CreatorSystem.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            await context.Database.MigrateAsync();

            if (!await context.Posts.AnyAsync())
            {
                var posts = new List<Post>
                {
                    new("Welcome to CreatorSystem!", "Your first auto-seeded post 🎉"),
                    new("AI Power", "Soon this system will generate captions automatically.")
                };

                await context.Posts.AddRangeAsync(posts);
                await context.SaveChangesAsync();
            }
        }
    }
}
