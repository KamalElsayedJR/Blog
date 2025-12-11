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
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly BlogDbContext _dbContext;
        public GenericRepository(BlogDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(T item)
        => await _dbContext.Set<T>().AddAsync(item); 
        public void Delete(T item)
        => _dbContext.Set<T>().Remove(item);
        public async Task<T> GetByIdAsync(string id)
        => await _dbContext.Set<T>().FindAsync(id);   
        public void Update(T item)
        => _dbContext.Set<T>().Update(item);
        public async Task<IReadOnlyList<T>> GetAllAsync()
        => await _dbContext.Set<T>().ToListAsync();

    }
}
