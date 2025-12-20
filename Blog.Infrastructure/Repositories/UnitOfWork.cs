using Blog.Domain.Interfaces;
using Blog.Infrastructure.Persistence;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogDbContext _dbContext;
        private Hashtable _repository =new Hashtable();

        public ICategoryRepository CategoryRepository { get; set; }

        public UnitOfWork(BlogDbContext dbContext, ICategoryRepository _CategoryRepository)
        {
            _dbContext = dbContext;
            CategoryRepository = _CategoryRepository;
        }
        public async ValueTask DisposeAsync()
        => await _dbContext.DisposeAsync();
        public async Task<int> SaveChangesAsync()
        => await _dbContext.SaveChangesAsync();
        public IGenericRepository<T> GenericRepository<T>() where T : class
        {
            var type = typeof(T).Name;
            if (!_repository.ContainsKey(type))
            {
                var Repo = new GenericRepository<T>(_dbContext);
                _repository.Add(type, Repo);
            }
            return (IGenericRepository<T>)_repository[type];
        }

    }
}
