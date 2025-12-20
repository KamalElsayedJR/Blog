using Blog.Domain.Entities;
using Blog.Domain.Interfaces;
using Blog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BlogDbContext _dbContext;

        public CategoryRepository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Category> GetCategoryByName(string categoryName)
        => await _dbContext.Categories.FirstOrDefaultAsync(c => c.Name.ToLower() == categoryName.ToLower());
        
    }
}
