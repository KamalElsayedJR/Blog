using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Persistence
{
    public class AppDbContextDataSeeding
    {
        public static async Task DataSeed(BlogDbContext dbContext)
        {
            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            if (!dbContext.Categories.Any())
            {
                
                var categoriesData = File.ReadAllText("../Blog.Infrastructure/Persistence/DataSeed/Categories.json");
                var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);
                if (categories is not null)
                {
                    foreach (var item in categories)
                    {
                        await dbContext.Set<Category>().AddAsync(item);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }
            if (!dbContext.Posts.Any())
            {
                
                var postsData = File.ReadAllText("../Blog.Infrastructure/Persistence/DataSeed/Posts.json");
                var Posts = JsonSerializer.Deserialize<List<Post>>(postsData);
                if (Posts is not null)
                {
                    foreach (var item in Posts)
                    {
                        await dbContext.Set<Post>().AddAsync(item);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }
            if (!dbContext.Comments.Any())
            {
                
                var commentsData = File.ReadAllText("../Blog.Infrastructure/Persistence/DataSeed/Comments.json");
                var comments = JsonSerializer.Deserialize<List<Comment>>(commentsData);
                if (comments is not null)
                {
                    foreach (var item in comments)
                    {
                        await dbContext.Set<Comment>().AddAsync(item);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
