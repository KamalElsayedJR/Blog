using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Interfaces
{
    public interface IUnitOfWork: IAsyncDisposable
    {
        IGenericRepository<T>  GenericRepository<T>() where T : class;
        public ICategoryRepository CategoryRepository { get; set; }
        Task<int> SaveChangesAsync();
    }
}
